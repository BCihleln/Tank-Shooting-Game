using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_FixedPoint : Spawner
{
    public Transform[] spawnAt;

    protected override void Spawn()
    {
        foreach(Transform givenpoint in spawnAt)
        {
            spawnPoint = givenpoint;
            base.Spawn();
        }
    }
}
