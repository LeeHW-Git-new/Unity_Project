using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    public float movementSpeed = 3;
    public float runSpeed;
    private float applySpeed;

    public int HP = 3;
    public float jumpForce = 300;

    private Vector3 direction;

    private bool isDead;
    private bool isAction;
    private bool isWalking;

    [SerializeField] private float WalkTime;
    [SerializeField] private float WaitTime;
    private float CurrentTime;

    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private BoxCollider BoxCol;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        CurrentTime = WaitTime;
        isAction = true;
  
    }

    void Update()
    {
        if ((transform.position - GameObject.Find("Player").transform.position).magnitude<5f)
        {
            Vector3 direction = (transform.position - GameObject.Find("Player").transform.position).normalized;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 2.5f);
            anim.SetBool("Walk", true);
            transform.position += direction * 3f * Time.deltaTime;
        }
        else
        {
            anim.SetBool("Walk", false);
        }


        if (!isDead)
        {
            Move();
            ElapseTime();
        }
    }


    private void Move()
    {
       if(isWalking)
        {
            rb.MovePosition(transform.position + transform.forward * applySpeed * Time.deltaTime);
        }
    }
    private void Rotation()
    {
        if(isWalking)
        {
            Vector3 _rotation = Vector3.Lerp(transform.eulerAngles, direction, 0.01f);
            rb.MoveRotation(Quaternion.Euler(_rotation));
        }
    }
    private void ElapseTime()
    {
        if(isAction)
        {
            CurrentTime -= Time.deltaTime;
            //Debug.Log(CurrentTime);
            if (CurrentTime <= 0)
                ReSet();
        }
    }
    private void ReSet()
    {
        isAction = true;
        isWalking = false;
        anim.SetBool("Walk", isWalking);

        applySpeed = movementSpeed;
        //direction.Set(0f, -90f, 0f);

        RandomAction();
    }

    private void RandomAction()
    {
        int _random = Random.Range(0, 3);
        Debug.Log(_random);
        if (_random == 0)
            Wait();
        else if (_random == 1)
            Jump();
        else if (_random == 2)
            TryWalk();
    }

    public void Damage(int _dmg , Vector3 _targetPos)
    {
        if(!isDead)
        {
            HP -= _dmg;
            if(HP <=0)
            {
                Dead();
                return;
            }
        }
    }
    private void Dead()
    {
        isWalking = false;
        isDead = true;
    }

    private void Wait()
    {
        CurrentTime = WaitTime;
    }

    private void Jump()
    {
        CurrentTime = WaitTime;
        rb.AddForce(0, jumpForce, 0);
        anim.SetTrigger("jump");
    }
    private void TryWalk()
    {
        CurrentTime = WalkTime;
        isWalking = true;
        anim.SetBool("Walk", isWalking);
    }
    //void ControllPlayer()
    //{
    //    float moveHorizontal = Input.GetAxisRaw("Horizontal");
    //    float moveVertical = Input.GetAxisRaw("Vertical");

    //    Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

    //    if (movement != Vector3.zero)
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
    //        anim.SetInteger("Walk", 1);
    //    }
    //    else {
    //        anim.SetInteger("Walk", 0);
    //    }

    //    transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);

    //    if (Input.GetButtonDown("Jump") && Time.time > canJump)
    //    {
    //            rb.AddForce(0, jumpForce, 0);
    //            canJump = Time.time + timeBeforeNextJump;
    //            anim.SetTrigger("jump");
    //    }
    //}
}