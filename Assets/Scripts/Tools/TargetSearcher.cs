using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// FIXME: Make State changing more centralized
// currently the state changing behaviour is seperated around Update, ChooseTarget and SearchTarget
// TODO Consider use action or delegate to send message to caller?
// To support caller OnSearcherFound() function
public class TargetSearcher : MonoBehaviour
{

    // FIXME state changing not in time if a target is destory by others
    // this will cause some "Missing GameObject reference call" Error

    public enum State { Searching, Found /*, TargetLost*/ };
    public State currState { get; private set; }
    public GameObject target;

    [Header("Search Info")]
    public LayerMask targetLayer;
    [Min(0)] public float searchStart;
    [Min(.1f)] public float searchRadius = .1f;

    [Header("Search Multiple Times")]
    [Min(-1), Tooltip("-1: Search Constantly")] public int searchTimes = 1;
    [Min(.1f)] public float searchEvery = .1f;

    void Awake()
    {
        if (searchTimes == 0) this.enabled = false;
        timer = searchStart;
    }
    void OnDrawGizmosSelected()
    {
        if(this.enabled == false) return;

        switch (currState)
        {
            case State.Searching:
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, searchRadius);
                break;
            case State.Found:
                Debug.DrawLine(transform.position, target.transform.position, Color.red);
                break;
                // case State.TargetLost:
                //     break;
        }
    }


    void SearchTarget()
    {
        if (targetLayer == 0) Debug.LogWarning($"No TargetLayer Given !({name})");

        // FIXME Consider using OverlapSphereNonAlloc ? to avoid memory garbage
        var colliders = Physics.OverlapSphere(transform.position, searchRadius, targetLayer);
        if (target) //Set target before and still exist in game world
        {
            //Check Target in search range or not
            foreach (var collider in colliders)
                if (collider.gameObject == target && TargetNotBlocked(collider)) return;
        }

        //Previous Target Not found : 
        //target == null (target is destoryed or lost)
        //Set new target in Range
        if (ChooseTarget(colliders)) return;

        // No target in range
        target = null;
        currState = State.Searching;


    }

    bool TargetNotBlocked(Collider target)
    {
        // target block check
        // Physics.Raycast(
        //         transform.position,
        //         target.transform.position - transform.position,
        //         out RaycastHit hit,
        //         searchRadius);
        Physics.Linecast(
                transform.position,
                target.transform.position,
                out RaycastHit hit);
        // Debug.Log($"{name} -> { hit.collider}");
        return (hit.collider!=null && hit.collider.gameObject == target.gameObject);
    }

    bool ChooseTarget(Collider[] colliders)
    {
        //No Avalible targets in range
        if (colliders.Length == 0) return false;

        // TODO Consider not using list to prevent garbage
        List<Collider> candidates = new List<Collider>();
        foreach(var collider in colliders)
        {
            if(TargetNotBlocked(collider)) candidates.Add(collider);
        }

        // TODO: target priority and re-aim highest priority target
        // Priority offer by outside components
        if (candidates.Count > 0)
        {
            var targetIndex = Random.Range(0, candidates.Count);
            target = candidates[targetIndex].gameObject;

            // var targetIndex = Random.Range(0, colliders.Length);
            // target = colliders[targetIndex].gameObject;


            currState = State.Found;
            return true;
        }
        else return false;
    }

    int searchCounts = 1;
    float timer;
    private void Update()
    {
        if (timer <= 0)
        {
            if (searchCounts > searchTimes && searchTimes > 0) return;
            SearchTarget();
            if (searchTimes > 0) searchCounts++;
            
            if (searchCounts < searchTimes) timer = searchEvery;
            // FIXME: Maybe can destory self, need to broadcast the death to anyone who use this
        }
        else timer -= Time.deltaTime;
    }
}
