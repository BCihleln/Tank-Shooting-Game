using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_RandomPoint : Spawner
{
    [SerializeField] Vector2 xRange;
    [SerializeField] Vector2 yRange;
    [SerializeField] Vector2 zRange;
    protected override void Awake()
    {
        InvokeRepeating("RandomSpawnPoint", base.startTime, base.spawnEvery);
        base.Awake();
    }

    void RandomSpawnPoint()
    {
        spawnPoint.position = new Vector3(
            Random.Range(xRange.x, xRange.y),
            Random.Range(yRange.x, yRange.y),
            Random.Range(zRange.x, zRange.y)
            );
    }
}
