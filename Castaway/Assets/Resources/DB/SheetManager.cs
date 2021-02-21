using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class SheetManager : MonoBehaviour
{
    // const string URL = "https://docs.google.com/spreadsheets/d/1m93a_VuqFslKLSSAqJwx3-osGaGom0MgeCNRWND0njc/export?format=tsv&gid=1352990389";

    const string URL = "https://script.google.com/macros/s/AKfycbxwMcVcLzvhOffsgtVlUkWjhlRL_hiNBaHhQEajg2sThVP0W9pt9OdqDw/exec";
    IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        print(data);
    }


}
