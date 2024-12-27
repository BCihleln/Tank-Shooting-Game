using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Rocket : Bullet
{
    [Header("Rocket Info")]
    [SerializeField, Min(0)] float rocketTime=5f;
    [SerializeField, Min(0)] float rocketForce = 10f;
    [SerializeField, Min(0)] float rigidbodyDrag = 2.5f;

    protected override void Awake()
    {
        base.Awake();
        rb.useGravity = false;
        rb.drag = rigidbodyDrag;
    }

    protected override void MoveControll()
    {
        if (rocketTime > 0)
        {
            this.rb.AddForce(transform.forward * rocketForce * Time.deltaTime, ForceMode.Force);
            rocketTime -= Time.deltaTime;
        }
        else rb.useGravity = true;
    }
}
