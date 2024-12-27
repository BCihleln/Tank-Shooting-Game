using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Rigidbody))]
public class LifeSystem : MonoBehaviour
{
    //TODO: HP UI Interface
    // UI
    // Color change
    // Material change
    // Die Animation, Effects

    [SerializeField, Min(1)] float maxHP = 10;
    float currentHP ;
    void Start()
    {
        currentHP = maxHP;
    }

    public float GetHP() { return currentHP; }

    public void Hurt(float damage = 1)
    {
        if (damage < 0)
        {
            Debug.Log($"damage Error ! ({damage})");
            return;
        }
        currentHP -= damage;
        if (currentHP <= 0) Die();
    }

    public void Cure(float healPoint)
    {
        if (healPoint < 0)
        {
            Debug.Log($"healPoint Error ! ({healPoint})");
            return;
        }
        currentHP += healPoint;
        if (currentHP > maxHP) currentHP = maxHP;
    }


    void Die()
    {
        //Animation ?

        //Visual Effects?


        Destroy(gameObject);
    }
}
