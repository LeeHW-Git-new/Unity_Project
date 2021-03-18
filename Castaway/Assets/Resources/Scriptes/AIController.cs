using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public GameObject DropItem;

    public float movementSpeed = 3;
    public float runSpeed;
    private float applySpeed;

    public int HP = 3;
    public float jumpForce = 300;

    protected Vector3 destination;

    private bool isDead;
    private bool isAction;
    private bool isWalking;

    [SerializeField]
    private float WalkTime;
    [SerializeField]
    private float WaitTime;
    
    private float CurrentTime;

    [SerializeField] 
    private Animator anim;
    [SerializeField] 
    private NavMeshAgent Nav;
    [SerializeField] 
    private BoxCollider BoxCol;

    private AudioSource audioCilp;

    void Start()
    {
        anim = GetComponent<Animator>();
        Nav = GetComponent<NavMeshAgent>();
        CurrentTime = WaitTime;
        isAction = true;
        audioCilp = GetComponent<AudioSource>();
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
            Nav.SetDestination(transform.position + destination * 5f);
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
        destination.Set(Random.Range(-0.2f, 0.2f), 0f, Random.Range(0.5f, 1f));
        RandomAction();
    }

    private void RandomAction()
    {
        int _random = Random.Range(0, 3);
        //Debug.Log(_random);
        if (_random == 0)
            Wait();
        else if (_random == 1)
            Jump();
        else if (_random == 2)
            TryWalk();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameObject.Find("Player").GetComponent<Player>().action == true)
        {
            if (collision.gameObject.name == "Sword")
            {
                HP--;
                audioCilp.Play();
                Jump();
                if (HP <= 0)
                {
                    Dead();
                    audioCilp.Play();
                    return;
                }
                Debug.Log(HP);
            }
        }
    }

    private void Dead()
    {
        isWalking = false;
        isDead = true;
        Destruction();
    }

    private void Wait()
    {
        CurrentTime = WaitTime;
    }

    private void Jump()
    {
        CurrentTime = WaitTime;
        //rb.AddForce(0, jumpForce, 0);
        anim.SetTrigger("jump");
    }
    private void TryWalk()
    {
        CurrentTime = WalkTime;
        isWalking = true;
        anim.SetBool("Walk", isWalking);
    }

    void Destruction()
    {
        Drop();
        this.gameObject.SetActive(false);
        DropItem.SetActive(true);
    }

    private void Drop()
    {
        Instantiate(DropItem, gameObject.transform.position, Quaternion.identity);
    }

}