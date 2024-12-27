using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected GameObject target;
    [SerializeField, Min(0)] protected float startTime = 0;
    [SerializeField, Min(minRespawnTime)] protected float spawnEvery = 1;
    const float minRespawnTime = 0.001f;
    protected Transform spawnPoint = null;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        if (spawnPoint == null) spawnPoint = transform;
        timer = startTime;
        // InvokeRepeating(nameof(Spawn), startTime, spawnEvery);
    }

    float timer;
    protected virtual void Update()
    {
        if (timer <= 0)
        {
            timer = spawnEvery;
            Spawn();
        }
        else timer -= Time.deltaTime;
    }

    protected virtual void Spawn()
    {
        if (target != null) Instantiate(target, spawnPoint.position, spawnPoint.rotation);
        else Debug.LogError($"{name} does not have spawn target !");
    }

    public void SetTarget(GameObject replacedTarget)
    {
        target = replacedTarget;
    }

    public void SetRespwanTime(float time)
    {
        if (time > minRespawnTime) spawnEvery = time;
        else Debug.LogError("Respwan Time to small");
    }
}
