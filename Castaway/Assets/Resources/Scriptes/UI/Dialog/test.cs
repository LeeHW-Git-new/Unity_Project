using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    //public float time =100f;
    //public float target;
    //public float target_2;

    //public bool test_1;
    public int index;

    public GameObject player;
    public GameObject target;


    void Start()
    {
        StartCoroutine("timer");
        //if (test_1)
        //{
        //    StartCoroutine("timer");
        //}
        //else
        //{
        //    StartCoroutine("timer_action");
        //}

    }
    
    public void skip()
    {
        dialog.instance.skip(index);
    }

    IEnumerator timer()
    {
        yield return new WaitUntil(() => {
            if (false)
            {
                return true;
            }
            else
            {

                if (Vector3.Distance(player.transform.position, target.transform.position)<=5.0f)
                {
                    if (dialog.instance.dialog_read(0) && !dialog.instance.running)
                    {
                        index = 0;
                        IEnumerator dialog_co = dialog.instance.dialog_system_start(0);
                        StartCoroutine(dialog_co);
                        
                        if (dialog.instance.dialog_read(0))
                        {
                            return false;
                        }

                    }
                    else if (!dialog.instance.dialog_read(0) && !dialog.instance.running)
                    {
                       // time -= Time.deltaTime;
                        
                    }
                }
                else
                {
                    //time -= Time.deltaTime;
                   
                }
                
                return false;
            }
        });
    }

    //IEnumerator timer_action()
    //{
    //    yield return new WaitUntil(() => {
    //        if (time <= 0)
    //        {
    //            return true;
    //        }
    //        else
    //        {

    //            if (time <= target && time>=target_2)
    //            {
    //                if (dialog.instance.dialog_read(0) && !dialog.instance.running)
    //                {
    //                    index = 0;
    //                    IEnumerator dialog_co = dialog.instance.dialog_system_start(0);
    //                    StartCoroutine(dialog_co);

    //                    if (dialog.instance.dialog_read(0))
    //                    {
    //                        return false;
    //                    }

    //                }
    //                else if (!dialog.instance.dialog_read(0) && !dialog.instance.running)
    //                {
    //                    time -= Time.deltaTime;
                        
    //                }

    //            }
    //            else if(time <= target_2)
    //            {
    //                if (dialog.instance.dialog_read(1) && !dialog.instance.running)
    //                {
    //                    index = 1;
    //                    IEnumerator dialog_co = dialog.instance.dialog_system_start(1);
    //                    StartCoroutine(dialog_co);

    //                    if (dialog.instance.dialog_read(1))
    //                    {
    //                        return false;
    //                    }

    //                }
    //                else if (!dialog.instance.dialog_read(1) && !dialog.instance.running)
    //                {
    //                    time -= Time.deltaTime;
                       
    //                }
    //            }
    //            else
    //            {
    //                time -= Time.deltaTime;
                   
    //            }

    //            return false;
    //        }
    //    });
    //}
}
