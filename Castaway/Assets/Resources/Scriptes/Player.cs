using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    private Rigidbody characterRigidbody;
    private CharacterController pcController;
    private Animator animator;

    public static float MoveSpeed = 6.0f;
    public static float RunSpeed =10f;
    public float ApplySpeed = MoveSpeed;
    public float rotSpeed = 360f;
    public int selectNo = -1;
    public bool action = false;
    public GameObject playerHand;
    public Image hpBar;

    private void Start()
    {
        GameManager.Instance.playerHP = 100f;
        characterRigidbody = GetComponent<Rigidbody>();
        pcController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CharacterControl_Slerp();
        Run();
        animator.SetFloat("Speed", pcController.velocity.magnitude);
        StartCoroutine(HPbar());
        AnimationState();
        EquipSwap();
    }

    private void CharacterControl_Slerp()
    {
        if (!action)
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



    public void EquipSwap()
    {
        Transform hand = playerHand.transform;

        for (int i = 0; i < hand.childCount; i++)
        {
            if (i == selectNo)
            {
                hand.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                hand.GetChild(i).gameObject.SetActive(false);
            }
        }

    }

    private void AnimationState()
    {   
        if (Input.GetMouseButtonDown(0))
        {
            switch (selectNo)
            {
                case -1:
                    return;

                case 0:
                    animator.SetTrigger("Fishing");
                    break;

                case 1:
                    animator.SetTrigger("Axeing");
                    break;

                case 2:

                    break;


                case 3:
                    animator.SetTrigger("Attack");
                    break;
            }

        }
    }

    public void AnimationCheck()
    {
        Debug.Log(action);
        action = !action;
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
