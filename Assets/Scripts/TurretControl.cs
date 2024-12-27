using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FIXME: Spawner won't stop spwaning when disabled
public class TurretControl : MonoBehaviour
{
    Spawner_FixedPoint spawner;
    Aiming_Auto aiming;
    Transform body;
    void Awake()
    {
        spawner = GetComponent<Spawner_FixedPoint>();
        spawner.enabled = false;

        aiming = GetComponentInChildren<Aiming_Auto>();
        body = transform.Find("Body");
    }

    // Update is called once per frame
    void Update()
    {
        // TODO Limit the rotation angle of the turret
        if(aiming.isAiming)
        {
            spawner.enabled = true;
        }
        else
        {
            spawner.enabled = false;
            
            // body.localEulerAngles = new Vector3(0,body.localEulerAngles.y,0);
            body.Rotate(Vector3.up, 20 * Time.deltaTime);
        }
    }
}
