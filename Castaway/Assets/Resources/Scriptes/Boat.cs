using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Boat : MonoBehaviour
{
    public Transform target;
    public GameObject fakePlayer;
    private NavMeshAgent agent;
    private AudioSource audioCilp;

    [HideInInspector]
    public bool fix = false;

    [HideInInspector]
    public bool exit = false;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        fakePlayer.SetActive(false);
        audioCilp = GetComponent<AudioSource>();
    }

   
    void Update()
    {
       
        FixCheck();


        if (Vector3.Distance(GameObject.Find("Player").transform.position, transform.position) < 5.0f
               && Input.GetKeyDown(KeyCode.E))
        {

            if (fix)
            {
                this.GetComponent<ParticleSystem>().Play();
                GameObject.Find("Player").GetComponent<Animator>().SetTrigger("BoatFix");
                Invoke("BoatFix", 5.0f);
            }
        }



        if (Vector3.Distance(this.transform.position, target.transform.position)<= 120.0f
            && GameObject.Find("Main Camera").GetComponent<CameraController>().camState != CameraController.CamMode.GameOver)
        {

            GameObject.Find("Main Camera").GetComponent<CameraController>().camState = CameraController.CamMode.End;
        }

    }


    void BoatFix()
    {
        this.GetComponent<ParticleSystem>().Stop();
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
        GameObject.Find("Player").transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Find("Main Camera").GetComponent<CameraController>().camState = CameraController.CamMode.Boat;
        agent.SetDestination(target.position);
        fakePlayer.SetActive(true);
        exit = true;
        audioCilp.Play();
    }


    void FixCheck()
    {
        int slotCnt = Inventory.Instance.AllSlot.Count;
        int itemCnt = 0;
        for (int i = 0; i < slotCnt; i++)
        {
            Slot slot = Inventory.Instance.AllSlot[i].GetComponent<Slot>();

            if (!slot.isSlots())
                continue;


            if (slot.slot.Peek().no == 11)
            {
                itemCnt += slot.slot.Count;
            }

        }


        if(itemCnt >=10)
        {
            fix = true;
        }


    }

}
