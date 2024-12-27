using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enum DamageType{

// }

//TODO： Damage Class, damage number, damage type, damage duration, damage effcts ...
public class Attacker : MonoBehaviour
{
    [SerializeField] float damage = 1f;

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag != gameObject.tag)
            other.gameObject.GetComponent<LifeSystem>()?.Hurt(damage);
    }

    public void SetDamage(float amount) {damage = amount;}
    public float GetDamage() { return damage; }
}
