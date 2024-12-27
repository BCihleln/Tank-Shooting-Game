using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Customize the Inspector GUI, hide some veriable through specific variable state
public abstract class Follower : MonoBehaviour
{
    public enum State { NoTarget, Following, Reached, Fleeting };
    public State currState { get; protected set; }

    public Vector3 targetPosition{ get; protected set; }

    [Header("Follower Property")]
    [Min(0)] public float speed = 1f;
    [SerializeField, Min(0f)] float offsetDistance = 0f;
    // [Min(1f)]public float followRange = 20f;
    Rigidbody rb;

    [Header("Distance Maintenance Info")]
    [Tooltip("Whether the follower will maintain distance to target when too close to target.")]
    public bool maintainDistance = false;
    [SerializeField, Min(0.1f),Tooltip("Should be less than offsetDistance and more than 0.")] 
    protected float fleetDistance = .5f;

    /// <summary>
    /// Set Target and Toggle Follower State. <br/>
    /// Called constanctly no matter Follower is enabled or disabled
    /// </summary>
    protected abstract void UpdateState();

    
    // TODO Should be a delegate,Event or Action ?
    /// <summary>
    /// Do something until refind target
    /// </summary>
    /// <param name="tmpDestination"></param>
    // public abstract void TragetLostBehaviour(Vector3 tmpDestination);


    // InvokeRepeating UpdateState.
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if(offsetDistance <= fleetDistance) fleetDistance = offsetDistance / 2;
        if(Vector3.zero == targetPosition) currState = State.NoTarget;
        else currState = State.Following;

        InvokeRepeating(nameof(UpdateState), 0, .1f);
        // InvokeRepeating(nameof(UpdateState), 0, Time.deltaTime);
    }

    Vector3 distanceVector;
    float distance;
    protected virtual void Update() {
        if (currState == State.NoTarget) return;

        // Calculate Distance with Target
        distanceVector = (targetPosition - transform.position);
        distance = distanceVector.magnitude;
        // Chase the target until close enough
        if (distance - offsetDistance > 0.1f) currState = State.Following;
        else //Stop at position
        {
            if (maintainDistance && distance < offsetDistance - fleetDistance) currState = State.Fleeting;
            else currState = State.Reached;
        }
    }

    protected virtual void FixedUpdate()
    {
        switch (currState)
        {
            case State.NoTarget:
                // TragetLostBehaviour(tmpDestination: value);
                break;
            case State.Following:
                MoveControll(distanceVector.normalized);
                break;
            case State.Reached:
                // if (rb != null) rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime);
                // else transform.Translate(transform.position);
                if (rb != null) rb.velocity = Vector3.zero;
                else transform.Translate(transform.position);
                break;
            case State.Fleeting:
                MoveControll(-distanceVector.normalized);
                break;
        }
    }

    //TODO Pathfinding Algorithum
    protected virtual void MoveControll(Vector3 direction)
    {
        //Rigidbody sometimes conflicts transform.Translate
        if (rb != null) rb.velocity = direction * speed;
        else transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        if (currState != State.NoTarget) Debug.DrawLine(transform.position, targetPosition, Color.yellow);
    }
}
