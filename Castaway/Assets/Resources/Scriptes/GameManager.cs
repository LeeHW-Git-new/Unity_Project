using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager sInstance;

    public static GameManager Instance
    {
        get
        {
            if (sInstance == null)
            {
                GameObject newGameObject = new GameObject("_GameManager");
                sInstance = newGameObject.AddComponent<GameManager>();
            }
            return sInstance;
        }
    }

    private void Awake()
    {
        Screen.SetResolution(1920, 1080, true);
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    public float playerHP;

}
