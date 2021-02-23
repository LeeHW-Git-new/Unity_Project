using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    private float distance;
    public Player playerPosition;
    public GameObject cookUI;
    public GameObject inventoryUI;

    private bool bCookUI = false;
    private void Update()
    {
        if (Vector3.Distance(playerPosition.transform.position, this.transform.position)<3.5f)
        {
            Debug.Log("test");
            if (Input.GetButtonDown("Interation"))
            {
                bCookUI = !bCookUI;
                cookUI.SetActive(bCookUI);
                inventoryUI.SetActive(bCookUI);
            }
        }
        
    }

}
