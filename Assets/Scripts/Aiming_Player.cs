using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming_Player : Aiming
{

    [Header("Player Aiming Info")]
    [SerializeField] Camera UsingCamera = null;

    override protected void Awake()
    {
        base.Awake();
        if (UsingCamera == null)
            UsingCamera = Camera.main;
        this.isAiming = true;
    }

    protected override void SetTarget()
    {
        // 将鼠标屏幕位置转换为世界坐标系中的射线
        Ray mouseRay = UsingCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(
            ray: mouseRay,
            hitInfo: out RaycastHit hit,
            maxDistance: 1000,
            layerMask: ~LayerMask.GetMask("Ground"))
            && hit.transform.tag != gameObject.tag
            )
            // 进行射线投射，检测是否与物体相交，並获取鼠标指向的点
            targetPosition = hit.point;
        else targetPosition = mouseRay.direction + transform.position;
        // Debug.DrawLine(transform.position, targetPosition, Color.red);
    }
}
