using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Aiming : MonoBehaviour
{
    // Summary
    // Aiming the GameObject to Target
    [Header("Aiming Basic Info")]
    public bool isAiming = false;
    [HideInInspector] public Vector3 targetPosition;
    [SerializeField] protected Transform aimBody;

/// <summary>
/// Set aimbody as self transform if it's not given in the inspector.
/// </summary>
    protected virtual void Awake() {
        if(aimBody == null) aimBody = transform;
    }

/// <summary>
/// Automatically Look at Target
/// </summary>
   virtual protected void Update()
    {
        SetTarget();
        if (isAiming && aimBody) aimBody.LookAt(targetPosition);

        // Debug.DrawRay(transform.position, targetPosition - transform.position, Color.red);
    }

    /// <summary>
    /// Need to Set isAiming and targetPosition
    /// </summary>
   abstract protected void SetTarget();
}
