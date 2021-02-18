using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
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

    bool Fishing = false;

    bool Item = false;

    bool sDown1;
    bool sDown2;
    bool sDown3;

    CharacterController pcController;

    Animator animator;

    public Image hpBar;


    void Wepon_Interation()
    {

    }

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
        //CharacterControl_Slerp();
        animator.SetFloat("Speed", pcController.velocity.magnitude);
        Interation();
        Swap();
        //Fishing_Animation();
        //TryRun();
        StartCoroutine(HPbar());

    }

    private void CharacterControl_Slerp()
    {
        if(!Fishing)
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
    private void Fishing_Animation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Fishing");
        }

        if(Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("Logging");
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fishing"))
        {
            Fishing = true;
        }
        else Fishing = false;
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
            GameManager.Instance.playerHP -= 0.1f*Time.deltaTime;
            hpBar.fillAmount = (GameManager.Instance.playerHP / 100f);
            yield return new WaitForSeconds(5f);
        }
    }
    void GetInput()
    {

        CharacterControl_Slerp();
        TryRun();
        Fishing_Animation();
        Item = Input.GetButtonDown("Interation");

        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        sDown3 = Input.GetButtonDown("Swap3");

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

    void Interation()
    {
        if(Item && NearWeapon != null)
        {
            if(NearWeapon.tag == "Weapon")
            {
                Weapon item = NearWeapon.GetComponent<Weapon>();
                int WeaponIndex = item.value;
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
