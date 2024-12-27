using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Rigidbody))]
public class TankMovment : MonoBehaviour
{
    // public GameObject Destination;
    [SerializeField,Min(0.001f)] float speed = 1;
    [SerializeField, Range(0.1f, 360f)] float DegressPerSecond = 20;
    // float losePowerAngle = 20;

    bool isGrounded = true;

    float hMotion = 0;
    float vMotion = 0;
    float last_vMotion = 0; // Use to simulate inertia(惯性，[E'ner shEa])
    void Awake()
    {
        //Tag every child with the same tag
        Transform[] childs = GetComponentsInChildren<Transform>(includeInactive:false);
        foreach (Transform child in childs) child.tag = gameObject.tag;
    }

    // Update is called once per frame
    
    private void Update() {
        hMotion = Input.GetAxisRaw("Horizontal");
        vMotion = Input.GetAxis("Vertical");
        isGrounded = 
        Physics.CheckSphere(transform.position, 1f, ~LayerMask.GetMask("Player"));
    }
    
    void FixedUpdate()
    {
        if(isGrounded)
        {
            last_vMotion = vMotion;
            // Debug.Log("On Ground");
            transform.Translate(Vector3.forward * vMotion * speed * Time.deltaTime);
            transform.Rotate(Vector3.up * hMotion * DegressPerSecond * Time.deltaTime);
            // Debug.Log($"Last vMotion = {last_vMotion}");
        }
        else
        {
            transform.Translate(Vector3.forward * last_vMotion * speed * Time.deltaTime);
        }
    }
}
