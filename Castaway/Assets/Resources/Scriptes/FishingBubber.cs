using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FishingBubber : MonoBehaviour
{
    public Item fish;

    private NavMeshHit hit;
    private void Update()
    {
        if(NavMesh.SamplePosition(transform.position, out hit, 0.05f, NavMesh.AllAreas))
        {
            if(hit.mask == 8)
            {
                Debug.Log("Get Fish");
                Inventory.Instance.AddItem(fish);
                GameObject.Find("Player").GetComponent<Player>().action = false;
            }
            else
            {
                GameObject.Find("Player").GetComponent<Player>().action = false;
            }
        }
    }

}
