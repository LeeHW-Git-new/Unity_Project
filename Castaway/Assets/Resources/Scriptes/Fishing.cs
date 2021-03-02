using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    public GameObject bobber;
    public Transform parent;
    private GameObject bubberChild;
    private void CastingRoad()
    {
        Debug.Log("Cast");

        bubberChild = Instantiate(bobber) as GameObject;
        bubberChild.transform.SetParent(parent);
        bubberChild.transform.rotation = parent.rotation;
        bubberChild.transform.position = new Vector3(parent.transform.position.x, parent.transform.position.y, parent.transform.position.z);

    }
   
    private void PullRoad()
    {
        Destroy(bubberChild);
    }

}
