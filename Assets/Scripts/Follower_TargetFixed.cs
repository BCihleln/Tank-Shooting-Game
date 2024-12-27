using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower_TargetFixed : Follower
{
    public Vector3 targetDesire;
    protected override void UpdateState()
    {
        if(Vector3.Distance(transform.position,targetDesire) < 0.1f)
        {
            currState = State.Reached;
        }
        else {
            
            targetPosition = targetDesire;
            currState = State.Following;
        }
    }
}
