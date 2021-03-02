using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingBubber : MonoBehaviour
{
    public Item fish;
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "Water")
        {
            Debug.Log("Get Fish");
            Inventory.Instance.AddItem(fish);
        }

    }
}
