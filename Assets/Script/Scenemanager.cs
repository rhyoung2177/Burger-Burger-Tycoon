using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Scenemanager : MonoBehaviour
{
    public GameObject SettingCanvas;
    public AudioClip SFX_BT;
    float BGMinfo, SFXinfo;
    Sequence mySequence;

    void Start()
    {
        SettingCanvas.SetActive(false);
        SettingCanvas.transform.position = new Vector2(0, -1000);
    }
    
    public void NewStart()
    {
        BGMinfo = PlayerPrefs.GetFloat("BGMVolume");
        SFXinfo = PlayerPrefs.GetFloat("SFXVolume");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("cloth1", 0);
        PlayerPrefs.SetInt("Day", 1);
        PlayerPrefs.SetInt("TimerUp", 0);
        PlayerPrefs.SetInt("FryUp", 0);
        PlayerPrefs.SetInt("CashUp", 0);
        PlayerPrefs.SetInt("purchase1", 1);
        PlayerPrefs.SetInt("decostate", 1);
        SceneManager.LoadScene("GameScene_Morning");
        PlayerPrefs.SetFloat("BGMVolume", BGMinfo);
        PlayerPrefs.SetFloat("SFXVolume", SFXinfo);
        PriceInit();
    }

    public void LoadStart()
    {
        if (PlayerPrefs.GetInt("Day") >= 1)
            SceneManager.LoadScene("GameScene_Morning");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SFXBTPlay()
    {
        AudioSource.PlayClipAtPoint(SFX_BT, transform.position, PlayerPrefs.GetFloat("SFXVolume"));
    }

    public void SettingCanvasActive()
    {
        mySequence = DOTween.Sequence()
        .SetAutoKill(false) //추가
        .OnStart(() =>
        {
            SettingCanvas.SetActive(true);
            SettingCanvas.transform.localScale = Vector3.zero;
            SettingCanvas.transform.localPosition = new Vector2(0, 0);
        })
        .Append(SettingCanvas.transform.DOScale(1, 0.5f).SetEase(Ease.Unset));
    }

    public void SettingCanvasInActive()
    {
        mySequence = DOTween.Sequence()
        .SetAutoKill(false) //추가
        .Append(SettingCanvas.transform.DOScale(0, 0.5f).SetEase(Ease.Unset));
        Invoke("SettingSetfalse", 0.5f);
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainScene");
    }

    void SettingSetfalse()
    {
        SettingCanvas.SetActive(false);
        SettingCanvas.transform.position = new Vector2(0, -1000);
    }

    void PriceInit()
    {
        PlayerPrefs.SetInt("ClothPrice1", 10000);
        PlayerPrefs.SetInt("ClothPrice2", 30000);
        PlayerPrefs.SetInt("ClothPrice3", 50000);
        PlayerPrefs.SetInt("ClothPrice4", 150000);
        PlayerPrefs.SetInt("ClothPrice5", 300000);
        PlayerPrefs.SetInt("ClothPrice6", 500000);
    }
}
