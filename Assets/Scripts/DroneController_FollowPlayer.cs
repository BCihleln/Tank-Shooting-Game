using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Follower))]
[RequireComponent(typeof(Aiming))]
public class DroneController : MonoBehaviour
{
    DroneManager host;

    [Header("Basic Drone Info")]
    public Vector3 homePosition;
    [SerializeField, Min(0)]protected float droneSpeed = 10f;
    protected Follower_TargetFixed follower;
    protected Aiming aiming;
    protected Spawner_FixedPoint weapon;

    protected virtual void Awake() {
        follower = gameObject.GetComponent<Follower_TargetFixed>();
        follower.speed = droneSpeed;

        aiming = gameObject.GetComponent<Aiming>();
        weapon = gameObject.GetComponent<Spawner_FixedPoint>();
    }

    protected virtual void Update() {
        // Move drone to the position
        follower.targetDesire = homePosition;
        if(aiming.isAiming) weapon.enabled = true;
        else weapon.enabled = false;
    }

    public void SetHost(DroneManager target) { host = target; }
    protected virtual void OnDestroy() {
        host?.DroneDestoryed(this);
    }
}
