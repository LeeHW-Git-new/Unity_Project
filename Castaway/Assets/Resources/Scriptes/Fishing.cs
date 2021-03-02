using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    public GameObject bubber;

    private void Update()
    {
        CastingLineState();
    }

    private void CastingLineState()
    {
        if (GameObject.Find("Player").GetComponent<Player>().action == true)
        {
            bubber.SetActive(true);
        }
        else
        {
            bubber.SetActive(false);
        }
    }

}
