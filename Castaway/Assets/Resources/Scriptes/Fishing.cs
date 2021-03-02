using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    public GameObject bubber;
    private void CastingRoad()
    {
        bubber.SetActive(true);  
    }
   
    private void PullRoad()
    {
        bubber.SetActive(false);
    }

}
