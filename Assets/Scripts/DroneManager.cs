using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class DroneManager : MonoBehaviour
{
    [SerializeField] GameObject DronePrefab;
    [SerializeField, Range(0, 20)] int dronesAmount = 3;
    [SerializeField] float DistributeRadius = 5f;
    // [SerializeField] float DroneMovementSpeed = 5f;
    [SerializeField, Range(0.5f, 2f)] float DroneHeight = 1f;

    //TODO: Using Object Pool to improve efficiency ?
    // List<GameObject> drones = new List<GameObject>();
    // List<Rigidbody> drones = new List<Rigidbody>();
    List<DroneController> drones = new List<DroneController>();

    List<Vector3> RelativePosition = new List<Vector3>();

    //TODO: Handle Logic while drone has been destroy by Enemy bullet
    //FIXME: Using Delgate / Action / Event to handle Drone add or delete logic
    void Awake()
    {
        //Add or Delete Drone to the given Amount, All Judgements are done in the funtions
        InvokeRepeating(nameof(AddDrone), 0, 0.5f);
        InvokeRepeating(nameof(DelDrone_test), 0, 0.5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Tell every drone controller thier position
        for (int i = 0; i < drones.Count; i++)
        {
            drones[i].homePosition = transform.position + RelativePosition[i];
        }
    }

    void AddDrone()
    {
        if (dronesAmount > drones.Count)
        {   //In order to calculate all drones Relative Position
            for (int i = drones.Count; i < dronesAmount; ++i)
            {
                var droneObject = Instantiate(DronePrefab,transform.position + Vector3.up,Quaternion.identity);
                droneObject.GetComponent<DroneController>().SetHost(this);
                
                drones.Add(droneObject.GetComponent<DroneController>());
                RelativePosition.Add(Vector3.zero);
            }
            CalculateRelativePos();
        }
    }

    void DelDrone_test()
    {
        if (dronesAmount < drones.Count)
        {
            for (int i = drones.Count; i > dronesAmount; --i)
            {
                int randomIndex = Random.Range(0, drones.Count - 1);
                Destroy(drones[randomIndex].gameObject);
                drones.RemoveAt(randomIndex);
                RelativePosition.RemoveAt(randomIndex);
            }
            CalculateRelativePos();
        }
    }

    public void DroneDestoryed(DroneController target)
    {
        // FIXME: May cause memory leak on "x => x == target.gameObject"?

        // Delete target from DroneList
        var targetInList = drones.FindIndex(x => x == target);
        if (targetInList >= 0)
        {
            drones.RemoveAt(targetInList);
            RelativePosition.RemoveAt(targetInList);
            dronesAmount--;
            CalculateRelativePos();
        }
        else Debug.LogWarning("The Drone has been destory some where else !");
    }

    void CalculateRelativePos()
    {
        // TODO Abstract this for supporting multiple style of follow point sets
        if(drones.Count <=0 ) return;

        float averageAngle = 360f / (float)drones.Count;
        Vector3 distributeStartPoint = 
            (transform.right * DistributeRadius);
        distributeStartPoint.Set(
            distributeStartPoint.x,
            distributeStartPoint.y + DroneHeight,
            distributeStartPoint.z
        );
        for (int i = 0; i < drones.Count; i++)
        {
            RelativePosition[i] = Quaternion.Euler(0, (float)i * averageAngle, 0) * distributeStartPoint;
        }
    }
}
