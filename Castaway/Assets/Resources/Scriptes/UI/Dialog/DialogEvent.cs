using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogEvent : MonoBehaviour
{
    public GameObject player;
    public GameObject boat;
    public GameObject fire;



    void Start()
    {
        StartCoroutine("Destination");
    }


    void Update()
    {
        StartCoroutine("BoatFix");   
    }

    IEnumerator BoatFix()
    {
        yield return new WaitUntil(() =>
        {

            if (boat.GetComponent<Boat>().fix && !boat.GetComponent<Boat>().exit)
            {
                if (dialog.instance.dialog_read(5) && !dialog.instance.running)
                {
                    IEnumerator dialog_co = dialog.instance.dialog_system_start(5);
                    StartCoroutine(dialog_co);

                    if (dialog.instance.dialog_read(5))
                    {
                        return false;
                    }

                }
            }
            else if (boat.GetComponent<Boat>().fix && boat.GetComponent<Boat>().exit)
            {
                if (dialog.instance.dialog_read(6) && !dialog.instance.running)
                {
                    IEnumerator dialog_co = dialog.instance.dialog_system_start(6);
                    StartCoroutine(dialog_co);

                    if (dialog.instance.dialog_read(6))
                    {
                        return false;
                    }

                }
            }

            return false;
        }

        );
    }

   IEnumerator Destination()
    {
        yield return new WaitUntil(() =>
        {
            if (Vector3.Distance(player.transform.position, boat.transform.position)<=10.0f)
            {
                
                if (dialog.instance.dialog_read(0) && !dialog.instance.running)
                {
                    IEnumerator dialog_co = dialog.instance.dialog_system_start(0);
                    StartCoroutine(dialog_co);
                    
                    if (dialog.instance.dialog_read(0))
                    {
                        return false;
                    }

                }

            }
            else if(Vector3.Distance(player.transform.position, boat.transform.position) > 10.0f &&
            Vector3.Distance(player.transform.position, boat.transform.position) < 15.0f)
            {
                if (dialog.instance.dialog_read(1) && !dialog.instance.running)
                {
                    IEnumerator dialog_co = dialog.instance.dialog_system_start(1);
                    StartCoroutine(dialog_co);

                    if (dialog.instance.dialog_read(1))
                    {
                        return false;
                    }

                }
            }
            else if (Vector3.Distance(player.transform.position, boat.transform.position) <= 20.0f)
            {
                if (dialog.instance.dialog_read(2) && !dialog.instance.running)
                {
                    IEnumerator dialog_co = dialog.instance.dialog_system_start(2);
                    StartCoroutine(dialog_co);

                    if (dialog.instance.dialog_read(2))
                    {
                        return false;
                    }

                }
            }
            else if (Vector3.Distance(player.transform.position, fire.transform.position) <= 25.0f &&
            Vector3.Distance(player.transform.position, fire.transform.position) > 20.0f)
            {
                if (dialog.instance.dialog_read(3) && !dialog.instance.running)
                {
                    IEnumerator dialog_co = dialog.instance.dialog_system_start(3);
                    StartCoroutine(dialog_co);

                    if (dialog.instance.dialog_read(3))
                    {
                        return false;
                    }

                }
            }
            else if (Vector3.Distance(player.transform.position, fire.transform.position) <= 15.0f)
            {
                if (dialog.instance.dialog_read(4) && !dialog.instance.running)
                {
                    IEnumerator dialog_co = dialog.instance.dialog_system_start(4);
                    StartCoroutine(dialog_co);

                    if (dialog.instance.dialog_read(4))
                    {
                        return false;
                    }

                }
            }
            return false;
            
        }
        );
    }

}
