using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public AudioClip BGM;
    public AudioSource BGM1;
    public Slider BGMVolume, SFXVolume;
    
    void Start()
    {
        BGM1 = GetComponent<AudioSource>();
        BGM1.clip = BGM;
        if (SceneManager.GetActiveScene().name == "GameScene_Daytime" || SceneManager.GetActiveScene().name == "MainScene")
        {
            BGMVolume.value = PlayerPrefs.GetFloat("BGMVolume");
            SFXVolume.value = PlayerPrefs.GetFloat("SFXVolume");
        }
    }

    public void SliderUpdate()
    {
        BGM1.volume = PlayerPrefs.GetFloat("BGMVolume");
        PlayerPrefs.SetFloat("BGMVolume", BGMVolume.value);
        PlayerPrefs.SetFloat("SFXVolume", SFXVolume.value);
    }
}
