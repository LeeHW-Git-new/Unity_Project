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
            GameObject.Find("Player").transform.GetChild(1).gameObject.SetActive(false);
            GameObject.Find("Player").GetComponent<Player>().CamMode = false;
           agent.SetDestination(target.position);
           fakePlayer.SetActive(true);
       }
    }
}
