using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    [Header("Explosive Info")]
    public bool isTriggered = false;
    public LayerMask effectLayer;
    [Min(0)] public float explosionForce = 125f;
    [Min(0)] public float explosionRadius = 5;
    // public AudioSource sound;
    public AudioSource sound;
    public ParticleSystem visualEffect;
    const float destoryDelay = 1f;

    void Awake()
    {
        sound = GetComponent<AudioSource>();
        if (sound == null) sound = gameObject.GetComponentInChildren<AudioSource>();


        visualEffect = GetComponent<ParticleSystem>();
        if (visualEffect == null) visualEffect = gameObject.GetComponentInChildren<ParticleSystem>();

        if(effectLayer==0) Debug.Log($"{name} does not effect anything !");
    }

    public void TriggerBomb(float triggerDelay = 0f)
    {
        if(isTriggered) return;
        StartCoroutine(Explosion(triggerDelay));
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    /// <summary>
    /// tear apart other and Destory them after delay seconds
    /// </summary>
    /// <param name="delay"></param>
    private IEnumerator Explosion(float delay)
    {
        isTriggered = true;

        yield return new WaitForSeconds(delay);
        
        // SendMessage(nameof(TriggerBomb),0f, SendMessageOptions.DontRequireReceiver);

        var colliders = Physics.OverlapSphere(transform.position, explosionRadius, effectLayer);
        // FIXME Ignore objects behind walls ?

        List<GameObject> bodys = new List<GameObject>();
        foreach (var collider in colliders)
        {
            // Trigger nearby bomb
            var nearbyExplosive = collider.gameObject.GetComponentInChildren<Explosive>();
            if( nearbyExplosive && 
                nearbyExplosive!=this && 
                nearbyExplosive.isTriggered == false) 
            {
                nearbyExplosive.TriggerBomb(.25f);
                continue;
            }

            //FIXME: Consider the function of body tear out
            //// Get bodys that not belongs to bomb
            // var partMeshs = collider.gameObject.GetComponentsInChildren<MeshRenderer>();
            // foreach (var mesh in partMeshs)
            // {
            //     Debug.Log(mesh.gameObject.transform.parent.parent.name);
            //     bodys.Add(mesh.gameObject);
            // }
        }

        // // Let body spread out
        // foreach (var part in bodys)
        // {
        //     part.transform.SetParent(null);
        //     var rb = part.gameObject.GetComponent<Rigidbody>();
        //     if (rb == null) rb = part.gameObject.AddComponent<Rigidbody>();
        //     var collier = part.gameObject.AddComponent<BoxCollider>();


        //     float tmpMultipler = Random.Range(1, 10);
        //     rb.AddExplosionForce(explosionForce * tmpMultipler, transform.position, explosionRadius, explosionForce);

        //     Destroy(part.gameObject, destoryDelay * tmpMultipler);
        // }

        visualEffect.Play();
        sound.Play();
        yield return new WaitForSeconds(sound.clip.length);

        Destroy(gameObject);
    }
}
