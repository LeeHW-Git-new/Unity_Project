using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    private Rigidbody characterRigidbody;

    public static float MoveSpeed = 6.0f;
    public static float RunSpeed =10f;
    public float ApplySpeed = MoveSpeed;
    public float rotSpeed = 360f;


    CharacterController pcController;
    Vector3 direction;

    Animator animator;

    public Image hpBar;


    void Start()
    {
        GameManager.Instance.playerHP = 100f;
        characterRigidbody = GetComponent<Rigidbody>();
        pcController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {   
        CharacterControl_Slerp();
        animator.SetFloat("Speed", pcController.velocity.magnitude);
        Input_Animation();
        TryRun();
        //Debug.Log(pcController.velocity.magnitude);
        StartCoroutine(HPbar());
    }
    private void CharacterControl_Slerp()
    {
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"),
                0,
                Input.GetAxis("Vertical"));

            if (direction.sqrMagnitude > 0.01f)
            {
                Vector3 forward = Vector3.Slerp(transform.forward,
                    direction,
                    rotSpeed * Time.deltaTime /
                    Vector3.Angle(transform.forward, direction));
                transform.LookAt(transform.position + forward);
            }
            //direction.y -= jumpSpeed * Time.deltaTime;
            pcController.Move(direction * ApplySpeed * Time.deltaTime + Physics.gravity);
        }
    private void Input_Animation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Fishing");
        }
    }

    private void TryRun()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Run();
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunCancle();
        }
    }

    private void Run()
    {
        animator.SetBool("Runing", true);
        ApplySpeed = RunSpeed;
    }

    private void RunCancle()
    {
        animator.SetBool("Runing", false);
        ApplySpeed = MoveSpeed;
    }


    IEnumerator HPbar()
    {
        while (GameManager.Instance.playerHP >= 0)
        {
            GameManager.Instance.playerHP -= 1f*Time.deltaTime;
            hpBar.fillAmount = (GameManager.Instance.playerHP / 100f);
            yield return new WaitForSeconds(5f);
        }
    }
}
