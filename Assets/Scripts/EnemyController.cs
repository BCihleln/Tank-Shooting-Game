using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Spawner_FixedPoint spawner;
    Aiming_Auto aiming;
    Rigidbody rb;

    [Header("No Target Behaviour Settings")]
    [Min(0)] public float maxIdleTime = 5;

    void Awake()
    {
        spawner = GetComponent<Spawner_FixedPoint>();
        spawner.enabled = false;

        aiming = GetComponentInChildren<Aiming_Auto>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (aiming.isAiming)
        {
            spawner.enabled = true;
        }
        else
        {
            spawner.enabled = false;
        }
    }
}
