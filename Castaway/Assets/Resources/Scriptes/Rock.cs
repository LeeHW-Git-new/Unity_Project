using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rock : MonoBehaviour
{
    public GameObject DropItem;
    public int RockHP = 3;

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
        if (GameObject.Find("Player").GetComponent<Player>().action == true)
        {
            if (collision.gameObject.layer == 12)
            {
                {
                    Invoke("AttackRock", 3);
                    this.GetComponent<ParticleSystem>().Play();
                    SoundManager.Instance.PlaySFX("StoneImpact");

                    if (RockHP <= 0)
                    {
                        //Invoke("Destruction", 3);
                        this.GetComponent<ParticleSystem>().Play();
                        SoundManager.Instance.PlaySFX("StoneDestroy");
                        Destruction();
                    }
                    Debug.Log(RockHP);
                    transform.DOShakePosition(2, new Vector3(0.1f, 0, 0.1f));
                }
            }
            GameObject.Find("Player").GetComponent<Player>().action = false;
        }
    }
    void AttackRock()
    {
        RockHP--;
    }

    void Destruction()
    {
        Drop();
        this.gameObject.SetActive(false);
        DropItem.SetActive(true);
    }
    void Drop()
    {
        Instantiate(DropItem, gameObject.transform.position, Quaternion.identity);
    }
}
