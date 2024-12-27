using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Chaser))]
public class ActiveAttackDrone : DroneController
{
    enum State { Attack, Return };
    State currState = State.Attack;
    Chaser chaser;
    override protected void Awake() {
        chaser = GetComponent<Chaser>();
        chaser.speed = droneSpeed;

        follower = gameObject.AddComponent<Follower_TargetFixed>();
        follower.speed = droneSpeed;

        base.Awake();
    }

    override protected void Update() {
        base.Update();
        switch (currState)
        {
            case State.Attack: AttackMode();
                weapon.enabled = true;
                break;
            case State.Return: GoBackMode();
                weapon.enabled = false;
                break;
        }
    }

    private void GoBackMode()
    {
        chaser.enabled = false;
        follower.enabled = true;
        if(chaser.searcher.currState == TargetSearcher.State.Found)
        {
            currState = State.Attack;
        }
        else follower.targetDesire = homePosition;
    }

    private void AttackMode()
    {
        chaser.enabled = true;
        follower.enabled = false;
        if (chaser.searcher.currState == TargetSearcher.State.Searching)
        {
            currState = State.Return;
        }
    }
}