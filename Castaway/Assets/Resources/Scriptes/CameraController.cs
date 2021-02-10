using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;        // 따라다닐 타겟 오브젝트의 Transfor
    private Transform tr;                // 카메라 자신의 Transform
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void LateUpdate()
    {
        tr.position = new Vector3(target.position.x - 0.5f, target.position.y+15f, target.position.z -15f);
        tr.LookAt(target);
    }
}
