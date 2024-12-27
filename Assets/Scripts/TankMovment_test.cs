using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO ?:Abstract Tag System
/// <summary>
/// Using Rigidbody to move the tank
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class TankMovment_test : MonoBehaviour
{
    // public GameObject Destination;
    [SerializeField,Min(0.001f)] float speed = 1;
    // [SerializeField, Range(0.1f, 360f)] float DegressPerSecond = 20;
    Rigidbody rb;
    // float losePowerAngle = 20;

    bool isGrounded = true;
    void Awake()
    {
        //Tag every child with the same tag
        Transform[] childs = GetComponentsInChildren<Transform>(includeInactive:false);
        foreach (Transform child in childs) child.tag = gameObject.tag;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var hMotion = Input.GetAxisRaw("Horizontal");
        var vMotion = Input.GetAxis("Vertical");

        // Vector3 direction = Vector3.ProjectOnPlane(transform.forward,Vector3.up);
        Vector3 direction = transform.forward;

        isGrounded = 
        Physics.CheckSphere(transform.position, 1f, ~LayerMask.GetMask("Player"));

        if (isGrounded)
        {
            // Debug.Log("On Ground");

            // FIXME 透過剛體加力的方式在方塊遭遇斜坡等情況時，會被卡住。需要做額外處理，但透過剛體增加力能模擬更真實的載具移動效果，如高臺跳下時原本的衝量仍存在會往前滑行
            rb.velocity = direction * vMotion * speed +
                        new Vector3(0, rb.velocity.y,0);
            // rb.AddForce(transform.forward * vMotion * speed, ForceMode.Force);
            rb.AddTorque(Vector3.up * hMotion, ForceMode.Impulse);
            //  * DegressPerSecond / 360
        }
        // else
        // {
        //     // Debug.Log("Flying");
        //     rb.AddForce(Physics.gravity, ForceMode.Force);
        // }

    }

    // private void OnCollisionStay(Collision other) {
    //     rb.drag = 5;
    // }

    // private void OnCollisionExit(Collision other) {
    //     rb.drag = 0;
    // }
}
