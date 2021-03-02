using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingBubber : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "Water")
        {
            Debug.Log("Get Fish");
        }

    }
}
