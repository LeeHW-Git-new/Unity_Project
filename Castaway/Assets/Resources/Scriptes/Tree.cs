using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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
        if(GameObject.Find("Player").GetComponent<Player>().action == true)
        {
            if (collision.gameObject.layer == 9)
            {
                {

                    Invoke("AttackTree", 3);
                    this.GetComponent<ParticleSystem>().Play();
                    SoundManager.Instance.PlaySFX("WoodImpact");
            
                    //TreeHP--;
                    if (TreeHP <= 0)
                    {
                        this.GetComponent<ParticleSystem>().Play();
                        SoundManager.Instance.PlaySFX("TreeFalling");
                        //Invoke("Destruction", 3);
                        Destruction();
                    }
                    
                    Debug.Log(TreeHP);
                    transform.DOShakePosition(2, new Vector3(0.1f, 0, 0.1f));
                }

            }
            GameObject.Find("Player").GetComponent<Player>().action = false;
        }
    }

    void AttackTree()
    {
        TreeHP--;
    }

    void Destruction()
    {
        Drop();
        this.gameObject.SetActive(false);
        DropItem.SetActive(true);
    }

    void Drop()
    {
        Instantiate(DropItem, gameObject.transform.position,Quaternion.identity);
    }
}
