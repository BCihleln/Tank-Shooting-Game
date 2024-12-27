using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Attacker))]
public abstract class Bullet : MonoBehaviour
{
    [Header("Basic Bullet Info")]
    [SerializeField, Min(0)] protected float initialSpeed = 1;
    [SerializeField, Min(0)] protected float surviveTime = 5;

    /// <summary>
    /// Use for avoid self desturction when spawn by others
    /// </summary>
    [SerializeField, Min(0.01f),
    Tooltip("Use for avoid self desturction when spawn by others")] protected float invincibleTime = .05f;
    float invincibleTimer;

    // protected Attacker attacker;
    protected Rigidbody rb;

    protected virtual void Awake()
    {
        // attacker = GetComponent<Attacker>();
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        // rb.useGravity = true;

        invincibleTimer = invincibleTime;

        Destroy(gameObject, surviveTime);
    }

    /// <summary>
    /// Add Initial force to initialSpeed
    /// </summary>
    void Start()
    {
        rb.AddForce(transform.forward * initialSpeed * Time.deltaTime, ForceMode.Impulse);
    }

    /// <summary>
    /// use MoveControll()
    /// </summary>
    protected virtual void Update()
    {
        // Implement the common update logic for all bullet types
        // This can include movement, collision checks, etc.
        // You can use the initialSpeed and surviveTime variables here
        if(invincibleTimer >0) invincibleTimer -= Time.deltaTime;
        MoveControll();
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        // Debug.Log($"{gameObject.tag} Collides {other.gameObject.tag} and Destroy");
        if (invincibleTimer <= 0) Destroy(gameObject);
    }

    protected abstract void MoveControll();

    protected virtual void OnDestroy()
    {
        //TODO: Particle Effects, Play Sounds etc.
    }
}
