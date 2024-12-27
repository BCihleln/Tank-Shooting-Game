using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetSearcher))]
public class Aiming_Auto : Aiming
{
    public TargetSearcher searcher;
    public GameObject target { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        if (searcher == null) Debug.LogWarning($"{name} aiming can't get target from null");
    }

    protected override void SetTarget()
    {
        if (searcher?.currState == TargetSearcher.State.Found)
        {
            target = searcher.target;
            if (target)
            {
                targetPosition = target.transform.position;
                isAiming = true;
                return;
            }

        }
        isAiming = false;
    }
}
