using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Boat : MonoBehaviour
{
    public Transform target;
    public GameObject fakePlayer;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        fakePlayer.SetActive(false);
    }

   
    void Update()
    {

        if (Vector3.Distance(GameObject.Find("Player").transform.position, transform.position) < 10.0f
               && Input.GetKeyDown(KeyCode.E))
        {
            this.GetComponent<ParticleSystem>().Play();
            GameObject.Find("Player").GetComponent<Animator>().SetTrigger("BoatFix");
            Invoke("BoatFix", 5.0f);
        }



        if (Vector3.Distance(this.transform.position, target.transform.position)<= 100.0f
            && GameObject.Find("Main Camera").GetComponent<CameraController>().camState != CameraController.CamMode.GameOver)
        {
            GameObject.Find("Main Camera").GetComponent<CameraController>().camState = CameraController.CamMode.End;
        }

    }


    void BoatFix()
    {
        this.GetComponent<ParticleSystem>().Stop();
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
        GameObject.Find("Player").transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Find("Main Camera").GetComponent<CameraController>().camState = CameraController.CamMode.Boat;
        agent.SetDestination(target.position);
        fakePlayer.SetActive(true);
    }


}
