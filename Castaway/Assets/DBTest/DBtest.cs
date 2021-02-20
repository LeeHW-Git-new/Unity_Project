using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine.Networking;


public class DBtest : MonoBehaviour
{
    public Text text1;
    public Text text2;
    private void Awake()
    {
        StartCoroutine(DBCreate());
    }

    private void Start()
    {
        DatabaseInsert("Insert into test(USERNUMBER, ID, NICKNAME) VALUES(4, \"파블로\", \"에스코와르\")");
        DataBaseRead("Select * From test");
    }

    IEnumerator DBCreate()
    {
        string filePath = string.Empty;
        filePath = Application.dataPath + "/DBTest/Test.db";
        if(!File.Exists(filePath))
        {
            File.Copy(Application.streamingAssetsPath + "/DBTest/Test.db", filePath);
        }
        yield return text1.text = "DB생성완료"; ;
    }

    public string GetDBFilePath()
    {
        string str = "";
        str = "URI=file:" + Application.dataPath + "/DBTest/Test.db";
        return str;
    }

    public void DBConnectionCheck()
    {
        try
        {
            IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
            dbConnection.Open();

            if(dbConnection.State == ConnectionState.Open)
            {
                text2.text = "DB연결 성공";
            }
            else
            {
                text2.text = "연결실패(에러)";
            }

        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
    }

    public void DataBaseRead(string query)
    {
        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = query;
        IDataReader dataReader = dbCommand.ExecuteReader();

        while(dataReader.Read())
        {
            Debug.Log(dataReader.GetInt32(0) + ", " + dataReader.GetString(1) + ", " + dataReader.GetString(2));
        }

        dataReader.Dispose();
        dataReader = null;
        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;
    }

    public void DatabaseInsert(string query)
    {
        IDbConnection dbConnection = new SqliteConnection(GetDBFilePath());
        dbConnection.Open();
        IDbCommand dbCommand = dbConnection.CreateCommand();

        dbCommand.CommandText = query;
        dbCommand.ExecuteNonQuery();

        dbCommand.Dispose();
        dbCommand = null;
        dbConnection.Close();
        dbConnection = null;

    }
}
