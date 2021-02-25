using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float movementSpeed = 3;
    public int HP;
    public float jumpForce = 300;
    //
    public float timeBeforeNextJump = 1.2f;
    private float canJump = 0f;
    //

    private Vector3 direction;

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
        Move();
        Rotation();
        ElapseTime();
    }
    private void Move()
    {
       if(isWalking)
        {
            rb.MovePosition(transform.position + transform.forward * movementSpeed * Time.deltaTime);
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
        isWalking = false;
        isAction = true;
        anim.SetBool("Walk", isWalking);

        direction.Set(0f, Random.Range(0f, 360f), 0f);

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

    private void Wait()
    {
        CurrentTime = WaitTime;
    }

    private void Jump()
    {
        CurrentTime = WaitTime;
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