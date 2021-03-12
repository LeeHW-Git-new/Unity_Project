using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject DropItem;
    public GameObject DropItem2;
    public GameObject DropItem3;
    public int BoxHP = 3; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameObject.Find("Player").GetComponent<Player>().action == true)
        {
            if (collision.gameObject.layer == 9)
            {
                {

                    Invoke("AttackTree", 3);
                    SoundManager.Instance.PlaySFX("WoodImpact");
                    if (BoxHP <= 0)
                    {
                        SoundManager.Instance.PlaySFX("TreeFalling");
                        Destruction();
                    }
                    Debug.Log(BoxHP);
                }

            }
            GameObject.Find("Player").GetComponent<Player>().action = false;
        }
    }

    void AttackTree()
    {
        BoxHP--;
    }
    void Destruction()
    {
        this.transform.GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(2).gameObject.SetActive(true);
        Drop();
        DropItem.SetActive(true);
    }
    void Drop()
    {
        Instantiate(DropItem, gameObject.transform.position, Quaternion.identity);
    }
}
