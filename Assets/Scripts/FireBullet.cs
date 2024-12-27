using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Fire bullet with MouseKey
public class FireBullet : MonoBehaviour
{
    [SerializeField] KeyCode fireKey = KeyCode.Mouse0;
    //Bullet Attribute
    [SerializeField] GameObject bullet = null;

    //Fire Attribute
    [SerializeField, Min(0.01f)] float ReloadTime = 1f;
    public AudioSource fireSound;
    [SerializeField] Transform[] fireFrom;
    

    bool Ready = true;
    float timer = 0;
    void Update()
    {
        bool pressFireKey = false;
        if(!EventSystem.current.IsPointerOverGameObject())
        {   // Avoid firing when pointing to UI
            pressFireKey = Input.GetKey(fireKey);
        }


        if (pressFireKey && Ready)
        {
            foreach (var firePoint in fireFrom)
            {
                if(firePoint == null) continue;
                GameObject clone = Instantiate(bullet, firePoint.position, firePoint.rotation);
                //生成子彈時傳入其生成者Tag，避免自損
                clone.tag = gameObject.tag;
            }
            if(fireSound) fireSound.Play();
            Ready = false;
            timer = ReloadTime;
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = 0;
                Ready = true;
            }
        }
    }
}
