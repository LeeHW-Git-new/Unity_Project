using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MecanimControl : MonoBehaviour
{
    public float runSpeed = 6.0f;
    public float rotSpeed = 360f;
    public bool bHandUp = false;
    CharacterController pcController;
    Vector3 direction;

    Animator animator;
    NavMeshAgent agent;
   Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        pcController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
       // Input_Animation2();
        animator.SetFloat("Speed", pcController.velocity.magnitude);
        CharacterControl_Slerp();
    }

    private void CharacterControl_Slerp()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));


        if (direction.sqrMagnitude > 0.01f)
        {  
            Vector3 forward = Vector3.Slerp(transform.forward, direction, rotSpeed * Time.deltaTime / Vector3.Angle(transform.forward, direction));
            transform.LookAt(transform.position + forward);
           
        }
        else
        {
            

        }


        if(Input.GetKey(KeyCode.J))
        {
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
        }


        pcController.Move(direction * runSpeed * Time.deltaTime);

    }

    private void Input_Animation()
    {
        animator.SetFloat("Speed", pcController.velocity.magnitude);
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("HandUp");
        }


    }


    private void Input_Animation2()
    {
        if (target != null)
            agent.destination = target;

        animator.SetFloat("Speed",agent.velocity.magnitude);
        if (Input.GetMouseButtonDown(0)&&!bHandUp)
        {
            bHandUp = true;
            animator.SetBool("bHanUp", bHandUp);
            StartCoroutine("HandUp_Routine");
        }



        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                target = hit.point;
                
            }
        }



    }

    IEnumerator HandUp_Routine()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.0f);

            if(bHandUp &&
                animator.GetCurrentAnimatorStateInfo(1).IsName("UpperBody.Attack"))
            {
                if(animator.GetCurrentAnimatorStateInfo(1).normalizedTime>=1.0f)
                {
                    bHandUp = false;
                    animator.SetBool("bHandUp", bHandUp);
                    break;
                }
            }
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.name == "Coin3D")
    //    {
    //        Debug.Log("Get");   
    //        other.gameObject.SendMessage("Get");
    //        GameManager.Instance.Score += 100;
    //    }

    //    if(other.gameObject.name == "Enemy")
    //    {
    //        Debug.Log("Hit");
    //        GameManager.Instance.life -= 10;
    //    }

    //}

}


