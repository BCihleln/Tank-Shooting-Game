using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBehaviour : MonoBehaviour
{
    public GameObject reward;
    // const float rotateSpeed = 1f;
    // private void Update() {
    //     transform.Rotate(0, rotateSpeed, 0);
    // }

    private void OnCollisionEnter(Collision other) {

        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // TODO Give player Rewards
            // reward.transform.SetParent(other.transform);

            Destroy(gameObject);
        }
    }
}
