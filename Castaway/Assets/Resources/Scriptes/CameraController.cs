using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform target2;// 따라다닐 타겟 오브젝트의 Transfor
    private Transform tr;                // 카메라 자신의 Transform
    public bool end = false;

    public enum CamMode { Player, Boat, End, GameOver };
    public CamMode camState;

    private void Awake()
    {
        camState = CamMode.Player;
    }
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void LateUpdate()
    {
        switch(camState)
        {
            case CamMode.Player:
                tr.position = new Vector3(target.position.x - 0.5f, target.position.y + 15f, target.position.z - 15f);
                tr.LookAt(target);
                break;

            case CamMode.Boat:
                tr.position = new Vector3(target2.position.x + 5.0f, target2.position.y + 30f, target2.position.z - 15f);
                tr.LookAt(target2);
                break;

            case CamMode.End:
                if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f)
                {
                    camState = CamMode.GameOver;
                    Invoke("EndCamera", 4f);
                }
                else if (camState != CamMode.GameOver && !this.GetComponent<Animator>().GetBool("End"))
                {
                    this.GetComponent<Animator>().SetBool("End",true);
                }
                break;

            case CamMode.GameOver:

                break;

        }
    }

    void EndCamera()
    {
        this.GetComponent<Animator>().SetBool("End", false);
    }

}
