using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    private float TickTime;
    private float DestroyTime = 1.5f;
    private Rigidbody characterRigidbody;
    private CharacterController pcController;
    private Animator animator;

    public static float MoveSpeed = 6.0f;
    public static float RunSpeed =10f;
    public float ApplySpeed = MoveSpeed;
    public float rotSpeed = 360f;
    public GameObject playerHand;
    public Image hpBar;
    
    //bool Fishing = false;
    bool Logging = false;
    public bool Axeing = false;

    void Start()
    {
        GameManager.Instance.playerHP = 100f;
        characterRigidbody = GetComponent<Rigidbody>();
        pcController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        GetInput();
        animator.SetFloat("Speed", pcController.velocity.magnitude);
        StartCoroutine(HPbar());
    }


    void GetInput()
    {
        CharacterControl_Slerp();
        Run();
    }


    private void CharacterControl_Slerp()
    {
        if (!Logging)
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
            pcController.Move(direction * ApplySpeed * Time.deltaTime + Physics.gravity);
        }
    }

    private void Run()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("Runing", true);
            ApplySpeed = RunSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("Runing", false);
            ApplySpeed = MoveSpeed;
        }
    }



    public void EquipSwap(int selectNO)
    {
        Transform hand = playerHand.transform;

        for (int i = 0; i < hand.childCount; i++)
        {
            if (i == selectNO)
            {
                hand.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                hand.GetChild(i).gameObject.SetActive(false);
            }
        }

    }

    private void AnimationState(int selectNO)
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch(selectNO)
            {
                case -1:
                    return;

                case 0:
                    animator.SetTrigger("Fishing");             
                    break;

                case 1:
                    animator.SetTrigger("Logging");         
                    break;

                case 2:
                    
                    break;


                case 3:
                    animator.SetTrigger("Attack");
                    break;
            }
        }


        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fishing"))
        {
            //Fishing = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Axe") &&
                   animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f && 
                   animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        {
            Axeing = true;
        }
        else
        {
            //Fishing = false;
            Logging = false;
        }
    }



    IEnumerator HPbar()
    {
        while (GameManager.Instance.playerHP >= 0)
        {
            GameManager.Instance.playerHP -= 0.1f * Time.deltaTime;
            hpBar.fillAmount = (GameManager.Instance.playerHP / 100f);
            yield return new WaitForSeconds(5f);
        }
    }

}
