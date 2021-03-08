using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Boat : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

   
    void Update()
    {
        if (Vector3.Distance(GameObject.Find("Player").transform.position, transform.position) < 10.0f)
        {
            agent.SetDestination(target.position);
        }
    }
}
