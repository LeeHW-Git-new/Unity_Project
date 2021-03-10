using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class dialog_info
{
    public string name;
    [TextArea(3, 5)]
    public string content;
    public bool check_read;
}

[System.Serializable]
public class Dialog_cycle
{
    public string cycle_name;
    public List<dialog_info> info = new List<dialog_info>();
    public int cycle_index;
    public bool check_cycle_read;
}


public class dialog : MonoBehaviour
{
    [SerializeField]
    public static dialog instance = null;
    public List<Dialog_cycle> dialog_cycles = new List<Dialog_cycle>();
    public Queue<string> text_seq = new Queue<string>();               
    public string name_;                                               
    public string text_;                                               

    public Text nameing;                                               
    public Text DialogT;                                               
    public Text Next_T;                                               
    public GameObject dialog_obj;                                      

    IEnumerator seq_;
    IEnumerator skip_seq;

    public float delay;
    public bool running = false;


    void Awake()  
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }

    public void skip(int index)
    {
        StopAllCoroutines();
        dialog_obj.SetActive(false);
        dialog_cycles[index].check_cycle_read = true;
        running = false;
    }
    public IEnumerator dialog_system_start(int index)
    {
        nameing = dialog_obj.GetComponent<parameter>().name_text;  
        DialogT = dialog_obj.GetComponent<parameter>().content;
        Next_T = dialog_obj.GetComponent<parameter>().next_text;

        running = true;
        foreach (dialog_info dialog_temp in dialog_cycles[index].info)  
        {
            text_seq.Enqueue(dialog_temp.content);
        }

        dialog_obj.gameObject.SetActive(true);
        for (int i = 0; i < dialog_cycles[index].info.Count; i++)      
        {

            nameing.text = dialog_cycles[index].info[i].name;

            text_ = text_seq.Dequeue();                                 
            
            seq_ = seq_sentence(index, i);                              
            StartCoroutine(seq_);                                       


            yield return new WaitUntil(() =>
            {
                if (dialog_cycles[index].info[i].check_read)            
                {
                    return true;                                        
                }
                else
                {
                    return false;                                       
                }
            });
        }


                                  

        dialog_cycles[index].check_cycle_read = true;                 
        running = false;
    }

    public void DisplayNext(int index, int number)                    
    {
        Next_T.text = "";
        Next_T.gameObject.SetActive(false);

        if (text_seq.Count == 0)                                      
        {
            dialog_obj.gameObject.SetActive(false);                   
        }
        StopCoroutine(seq_);                                          

        dialog_cycles[index].info[number].check_read = true;          
    }

    public IEnumerator seq_sentence(int index, int number)            
    {
        skip_seq = touch_wait(seq_, index, number);                   
        StartCoroutine(skip_seq);                                     
        DialogT.text = "";                                            
        foreach (char letter in text_.ToCharArray())                  
        {
            DialogT.text += letter;                                   
            yield return new WaitForSeconds(delay);                   
        }
        Next_T.gameObject.SetActive(true);
        Next_T.text = "next";
        StopCoroutine(skip_seq);                                      
        IEnumerator next = next_touch(index, number);                 
        StartCoroutine(next);
    }

    public IEnumerator touch_wait(IEnumerator seq, int index, int number)
    {
        yield return new WaitForSeconds(0.3f);
        yield return new WaitUntil(() => Input.GetMouseButton(0));
        StopCoroutine(seq);                                              
        DialogT.text = text_;                                            
        Next_T.gameObject.SetActive(true);
        Next_T.text = "next";
        IEnumerator next = next_touch(index, number);                    
        StartCoroutine(next);                                                   
    }

    public IEnumerator next_touch(int index, int number)   
    {
        StopCoroutine(seq_);
        StopCoroutine(skip_seq);
        yield return new WaitForSeconds(0.3f);
        yield return new WaitUntil(() => Input.GetMouseButton(0));
        DisplayNext(index, number);
    }

    public bool dialog_read(int check_index)         
    {
        if (!dialog_cycles[check_index].check_cycle_read)
        {
            return true;
        }
        
        return false;
    }
}
