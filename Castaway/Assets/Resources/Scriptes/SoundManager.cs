using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager sInstance;

    public static SoundManager Instance
    {
        get
        {
            if(sInstance == null)
            {
                Debug.Log("instance is null");
            }
            return sInstance;
        }
    }

    private void Awake()
    {
        sInstance = this;
    }

    public int audioSourceCount = 1;

    [SerializeField]
    [Header("Audio Clips")]
    public AudioClip[] BGM = new AudioClip[1];
    public AudioClip[] SFX = new AudioClip[1];

    private AudioSource BGMsource;
    private AudioSource[] SFXsource;

    public delegate void CallBack();
    CallBack BgmEndCallBack;

    private void OnEnable()
    {
        float volume = PlayerPrefs.GetFloat("volumeBGM", 1);

        BGMsource = gameObject.AddComponent<AudioSource>();
        BGMsource.volume = volume;
        BGMsource.playOnAwake = false;
        BGMsource.loop = true;

        SFXsource = new AudioSource[audioSourceCount];

        volume = PlayerPrefs.GetFloat("volumeSFX", 1);

        for(int i = 0; i<SFXsource.Length; i++)
        {
            SFXsource[i] = gameObject.AddComponent<AudioSource>();
            SFXsource[i].playOnAwake = false;
            SFXsource[i].volume = volume;
        }

        ChangeBGM("Game", false);

    }

    // ==== SFX ====
    public void PlaySFX(string name, bool loop = false, float pitch = 1)
    {
        for(int i = 0; i<SFX.Length; i++)
        {
            if(SFX[i].name == name)
            {
                AudioSource a = GetEmptySource();
                a.loop = loop;
                a.pitch = pitch;
                a.clip = SFX[i];
                a.Play();
                return;
            }
        }
    }

    public void StopSFXByName(string name)
    {
        for(int i = 0; i<SFXsource.Length; i++)
        {
            if(SFXsource[i].clip.name == name)
            {
                SFXsource[i].Stop();
            }
        }
    }

    private AudioSource GetEmptySource()
    {
        int lageindex = 0;
        float lageProgress = 0;
        for(int i = 0; i<SFXsource.Length; i++)
        {
            if(!SFXsource[i].isPlaying)
            {
                return SFXsource[i];
            }

            float progress = SFXsource[i].time / SFXsource[i].clip.length;
            if(progress > lageProgress && !SFXsource[i].loop)
            {
                lageindex = i;
                lageProgress = progress;
            }
        }
        return SFXsource[lageindex];
    }

    // =========

    // ==== BGM ====
    private AudioClip changeClip;
    private bool isChanging = false;
    private float startTime;

    [SerializeField]
    [Header("Changing speed")]
    public float ChangingSpeed;

    public void ChangeBGM(string name, bool isSmooth = false, CallBack callback = null)
    {
        BgmEndCallBack = callback;

        changeClip = null;

        for(int i = 0; i<BGM.Length; i++)
        {
            if(BGM[i].name == name)
            {
                changeClip = BGM[i];
            }
        }

        if (changeClip == null)
            return;

        if(!isSmooth)
        {
            BGMsource.clip = changeClip;
            BGMsource.Play();
        }
        else
        {
            startTime = Time.time;
            isChanging = true;
        }
    }

    public string GetRandomBgmName()
    {
        return BGM[Random.Range(0, BGM.Length)].name;
    }

    private void Update()
    {
        if (!isChanging) return;

        float progress = (Time.time - startTime) * ChangingSpeed;
        BGMsource.volume = Mathf.Lerp(PlayerPrefs.GetFloat("volumeBGM", 1), 0, progress);

        if(progress > 1)
        {
            isChanging = false;
            BGMsource.volume = PlayerPrefs.GetFloat("volumeBGM", 1);
            BGMsource.clip = changeClip;
            BGMsource.Play();
        }
    }



    public void StopBGM()
    {
        BGMsource.Stop();
    }

    public void SetPitch(float pitch)
    {
        BGMsource.pitch = pitch;
    }
    


    public void changeBGMVolume(float volume)
    {
        PlayerPrefs.SetFloat("volumeBGM", volume);
        BGMsource.volume = volume;
    }

    public void changeSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("volumeSFX", volume);
        for(int i = 0; i<SFXsource.Length; i++)
        {
            SFXsource[i].volume = volume;
        }
    }

    // ==========
}
