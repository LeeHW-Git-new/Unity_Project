using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneChange : MonoBehaviour
{
    public Slider volumeSlider;
    public InputField inputField;

    void Start()
    {
        SoundManager.Instance.ChangeBGM("MenuBGM");
    }

 
    void Update()
    {
        BgmVolume();
    }


    void BgmVolume()
    {
        SoundManager.Instance.changeBGMVolume(volumeSlider.value);
    }


    void ChangeScene()
    {
        GameManager.Instance.playerName = inputField.text;
        LoadingManager.LoadScene("Forest");
    }

    void Quit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


}
