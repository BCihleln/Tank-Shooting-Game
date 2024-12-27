using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Aiming_Auto))]
[RequireComponent(typeof(TargetSearcher))]
public class Chaser : Follower
{
    [Header("Auto TargetFinding Info")]
    public TargetSearcher searcher;

    protected override void UpdateState()
    {
        if(searcher?.target != null) 
        {   targetPosition = searcher.target.transform.position;
            currState = State.Following;
        }
        else currState = State.NoTarget;
    }
}
