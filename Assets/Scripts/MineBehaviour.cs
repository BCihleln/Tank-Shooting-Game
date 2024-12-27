using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// When it detects Target, come out or visible and explode
/// </summary>
[RequireComponent(typeof(Explosive))]
public class MineBehaviour : MonoBehaviour
{
    TargetSearcher detector;
    Animator anim;
    [Header("Mine Info")]
    public bool isTriggered = false;
    [Min(0)] public float explodeDelayTime = 1f;

    Explosive bomb;
    // [Header("Explosive Info")]
    // public LayerMask effectLayer;
    // [Min(0)]public float explosionForce = 500f;
    // [Min(0)] public float explosionRadius = 5;
    // // public AudioSource sound;
    // public AudioSource sound;
    // public ParticleSystem visualEffect;
    // const float destoryDelay = .5f;

    void Awake()
    {
        detector = GetComponent<TargetSearcher>();
        anim = GetComponent<Animator>();

        bomb = GetComponentInChildren<Explosive>();
        if (bomb == null) bomb = gameObject.AddComponent<Explosive>();

        // sound = GetComponent<AudioSource>();
        // if(sound == null) sound = gameObject.GetComponentInChildren<AudioSource>();


        // visualEffect = GetComponent<ParticleSystem>();
        // if (visualEffect == null) visualEffect = gameObject.GetComponentInChildren<ParticleSystem>();
    }

    // TODO Using OnDetectorFound() to replace this function
    private void Update() {
        if(!isTriggered && detector.currState == TargetSearcher.State.Found)
        {
            isTriggered = true;
            anim.SetTrigger("BombEnable");
            bomb.TriggerBomb(explodeDelayTime);
        }
    }

    // public void TriggerBomb(float triggerDelay = 0f)
    // {
    //     StartCoroutine(Explosion(triggerDelay));
    //     isTriggered = true;
    // }

    // /// <summary>
    // /// tear apart other and Destory them after delay seconds
    // /// </summary>
    // /// <param name="delay"></param>
    // private IEnumerator Explosion(float delay)
    // {

    //     yield return new WaitForSeconds(delay);

    //     var colliders = Physics.OverlapSphere(transform.position, explosionRadius, effectLayer);

    //     List<GameObject> bodys = new List<GameObject>();
    //     foreach(var collider in colliders)
    //     {
    //         var partMeshs = collider.gameObject.GetComponentsInChildren<MeshRenderer>();
    //         foreach(var mesh in partMeshs) bodys.Add(mesh.gameObject);
    //     }

    //     //Destroy root rigidbody
    //     // Destroy(other.GetComponent<Rigidbody>());

    //     foreach (var part in bodys)
    //     {
    //         part.transform.SetParent(null);
    //         var rb = part.gameObject.GetComponent<Rigidbody>();
    //         if (rb == null) rb = part.gameObject.AddComponent<Rigidbody>();
    //         var collier = part.gameObject.AddComponent<BoxCollider>();
            
            
    //         float tmpMultipler = Random.Range(1, 10);
    //         rb.AddExplosionForce(explosionForce * tmpMultipler, transform.position, explosionRadius, explosionForce);

    //         Destroy(part.gameObject, destoryDelay * tmpMultipler);
    //     }
    //     foreach(var collider in colliders) Destroy(collider.gameObject);

    //     sound.Play();
    //     visualEffect.Play();
    //     yield return new WaitForSeconds(sound.clip.length);

    //     Destroy(gameObject);
    // }
}
