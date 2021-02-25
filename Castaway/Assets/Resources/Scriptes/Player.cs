using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    float TickTime;
    float DestroyTime = 1.5f;
    private Rigidbody characterRigidbody;

    public GameObject[] Weapons;
    public bool[] hasWeapons;
    GameObject NearWeapon;
    GameObject EquipWeapon;
    int EquipWeaponIndex = -1;

    public static float MoveSpeed = 6.0f;
    public static float RunSpeed =10f;
    public float ApplySpeed = MoveSpeed;
    public float rotSpeed = 360f;

    public Image hpBar;
    //bool Fishing = false;
    bool Logging = false;

    public bool Axeing = false;

    bool Item = false;

    bool sDown1;
    bool sDown2;
    bool sDown3;

    private CharacterController pcController;
    private Animator animator;

    public GameObject playerHand;

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
        Interation();
        StartCoroutine(HPbar());
    }

    public void EquipSwap(int selectNO)
    {

         playerHand.transform.GetChild(selectNO).gameObject.SetActive(true);


    }

    void GetInput()
    {
        CharacterControl_Slerp();
        Run();
        AnimationState();
        Swap();
        Item = Input.GetButtonDown("Interation");

        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        sDown3 = Input.GetButtonDown("Swap3");

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
    private void AnimationState()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch(EquipWeaponIndex)
            {
                case -1:
                    return;

                case 0:
                    animator.SetTrigger("Logging");
                    break;

                case 1:
                    animator.SetTrigger("Attack");
                    break;

                case 2:
                    animator.SetTrigger("Fishing");
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

    private void Run()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("Runing", true);
            ApplySpeed = RunSpeed;
        }
        
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("Runing", false);
            ApplySpeed = MoveSpeed;
        }
    }


    void Swap()
    {
        if (sDown1 && (!hasWeapons[0] || EquipWeaponIndex == 0))
            return;
        if (sDown2 && (!hasWeapons[1] || EquipWeaponIndex == 1))
            return;
        if (sDown3 && (!hasWeapons[2] || EquipWeaponIndex == 2))
            return;

        int WeaponIndex = -1;
        if (sDown1) WeaponIndex = 0;
        if (sDown2) WeaponIndex = 1;
        if (sDown3) WeaponIndex = 2;
        if (sDown1 || sDown2 || sDown3)
        {
            if(EquipWeapon != null)
            {
                EquipWeapon.SetActive(false);
            }
            EquipWeaponIndex = WeaponIndex;
            EquipWeapon = Weapons[WeaponIndex];
            EquipWeapon.SetActive(true);
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



    void Interation()
    {
        if(Item && NearWeapon != null)
        {
            if(NearWeapon.tag == "Weapon")
            {
                Item item = NearWeapon.GetComponent<Item>();
                int WeaponIndex = item.no-20;
                hasWeapons[WeaponIndex] = true;


                Destroy(NearWeapon);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Weapon")
        {
            NearWeapon = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
        {
            NearWeapon = null;
        }
    }
}
