using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_MultipleTarget : Spawner_RandomPoint
{
    [SerializeField] GameObject[] targetList;
    override protected void Awake()
    {
        InvokeRepeating("RandomSelectTarget", base.startTime, base.spawnEvery);
        base.Awake();
    }

    void RandomSelectTarget()
    {
        if (targetList.Length > 0)
        {
            int index = Random.Range(0, targetList.Length);
            base.target = targetList[index];
        }
    }
}
