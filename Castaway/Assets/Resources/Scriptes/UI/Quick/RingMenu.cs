using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingMenu : MonoBehaviour
{
    public Ring Data;
    public RingCakePiece RingCakePiecePrefab;
    public float GapWidthDegree = 1f;
    public Action<string> callback;
    protected RingCakePiece[] Pieces;
    protected RingMenu Parent;

    [HideInInspector]
    public string Path;
    // Start is called before the first frame update
    void Start()
    {
        float stepLength = 360f / Data.Elements.Length;
        float iconDist = Vector3.Distance(RingCakePiecePrefab.Icon.transform.position, RingCakePiecePrefab.CakePiece.transform.position);

        Pieces = new RingCakePiece[Data.Elements.Length];

        for(int i = 0; i<Data.Elements.Length; i++)
        {
            Pieces[i] = Instantiate(RingCakePiecePrefab, transform);

            Pieces[i].transform.localPosition = Vector3.zero;
            Pieces[i].transform.localRotation = Quaternion.identity;


            Pieces[i].CakePiece.fillAmount = 1f / Data.Elements.Length - GapWidthDegree / 360f;
            Pieces[i].CakePiece.transform.localPosition = Vector3.zero;
            Pieces[i].CakePiece.transform.localRotation = Quaternion.Euler(0, 0, -stepLength / 2f + GapWidthDegree / 2f + i * stepLength);
            Pieces[i].CakePiece.color = new Color(1f, 1f, 1f, 0.5f);

            Pieces[i].Icon.transform.localPosition = Pieces[i].CakePiece.transform.localPosition + Quaternion.AngleAxis(i * stepLength, Vector3.forward)*Vector3.up * iconDist;
            Pieces[i].Icon.sprite = Data.Elements[i].Icon;

        }

    }

    // Update is called once per frame
    void Update()
    {
        float stepLength = 360f / Data.Elements.Length;
        float mouseAngle = NormlizeAngle(Vector3.SignedAngle(Vector3.up, Input.mousePosition - new Vector3(Screen.width/2, Screen.height/2), Vector3.forward) + stepLength / 2f);
        int activeElement = (int)(mouseAngle / stepLength);

        for (int i = 0; i < Data.Elements.Length; i++)
        {
            if (i == activeElement)
            {
                Pieces[i].CakePiece.color = new Color(1f, 1f, 1f, 0.75f);
            }
            else
            {
                Pieces[i].CakePiece.color = new Color(1f, 1f, 1f, 0.5f);
            }
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            gameObject.SetActive(false);
            GameObject.Find("Player").GetComponent<Player>().EquipSwap(activeElement);
        }


    }

    private float NormlizeAngle(float a) => (a + 90f) % 360f;


}
