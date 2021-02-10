using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TestSceneChange : MonoBehaviour
{
    public Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.ChangeBGM("MenuBGM");
    }

    // Update is called once per frame
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
