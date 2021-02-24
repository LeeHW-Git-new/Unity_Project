using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject DropItem;
    public int TreeHP = 3;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        while(GameObject.Find("Player").GetComponent<Player>().Axeing == true)
        {
            if (collision.gameObject.layer == 9)
            {
                {
                    TreeHP--;
                    if(TreeHP <= 0)
                    {
                       
                        Destruction();
                    }
                    
                    Debug.Log(TreeHP);
                }

            }
            GameObject.Find("Player").GetComponent<Player>().Axeing = false;
        }
    }

    void Destruction()
    {
        this.gameObject.SetActive(false);
        DropItem.SetActive(true);
    }
}
