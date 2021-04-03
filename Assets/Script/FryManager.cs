using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class FryManager : MonoBehaviour
{
    public GameObject Sideimage, Drinkimage, FryCanvas, FryTime, O, X;
    public AudioClip SFX_O, SFX_X, SFX_Fry, SFX_Drink;
    AudioSource SFX_FrySource;

    private int OX = 1;
    public int ConfirmFry = 1;
    private float ftime;
    private float[] ftime1 = { 1.5f, 3.0f, 3.5f, 4.0f, 5.5f, 10.5f };
    string num1, num2;
    int [] num = new int [2];
    Sequence mySequence;

    // Start is called before the first frame update
    void Start()
    {
        SFX_FrySource = GetComponent<AudioSource>();
        SFX_FrySource.clip = SFX_Fry;
        SFX_FrySource.loop = false; 
        SFX_FrySource.mute = false; 

        Sideimage.SetActive(false);
        Drinkimage.SetActive(false);
        FryCanvas.transform.position = new Vector2(-2000, 0);
        FryCanvas.SetActive(false);
        O.SetActive(false);
        X.SetActive(false);
    }
    
    void Update()
    {
        if (Equals(SceneManager.GetActiveScene().name, "GameScene_Daytime") && FryCanvas.activeSelf == true)
        {
            if(OX == 0)
                ftime -= Time.deltaTime;
        }
        FryTime.GetComponent<Text>().text = string.Format("{0:N2}", ftime);
        SFX_FrySource.volume = PlayerPrefs.GetFloat("SFXVolume");
    }

    public void FryActive()
    {
        mySequence = DOTween.Sequence()
        .SetAutoKill(false)
        .OnStart(() =>
        {
            FryCanvas.SetActive(true);
            FryCanvas.transform.localScale = Vector3.zero;
            FryCanvas.transform.localPosition = new Vector2(0, 0);
        })
        .Append(FryCanvas.transform.DOScale(1, 0.5f).SetEase(Ease.Unset));
        FryCanvas.SetActive(true);
        num2 = GameObject.Find("Cashermanager").GetComponent<CasherManager>().OrderFry();
        Debug.Log(num2);
    }

    void OXview(GameObject OX)
    {
        
        mySequence = DOTween.Sequence()
        .SetAutoKill(false)
        .OnStart(() =>
        {
            OX.SetActive(true);
            OX.transform.localScale = Vector3.zero;
            OX.transform.localPosition = new Vector2(0, 0);
        })
        .Append(OX.transform.DOScale(1, 0.5f).SetEase(Ease.Unset))
        .Append(OX.transform.DOScale(0, 0.5f).SetEase(Ease.Unset));
    }

    public void FryInActive()
    {
        mySequence = DOTween.Sequence()
        .SetAutoKill(false)
        .Append(FryCanvas.transform.DOScale(0, 0.5f).SetEase(Ease.Unset));
        Invoke("SettingSetfalse", 0.5f);
        
        num1 = num[0].ToString() + num[1].ToString();
        if (Equals(num1, num2))
        {
            ConfirmFry = 0;
            AudioSource.PlayClipAtPoint(SFX_O, transform.position, PlayerPrefs.GetFloat("SFXVolume"));
            OXview(O);
        }
        else
        {
            ConfirmFry = 1;
            AudioSource.PlayClipAtPoint(SFX_X, transform.position, PlayerPrefs.GetFloat("SFXVolume"));
            OXview(X);
        }
        num1 = "";
        ftime = 0f;
        OX = 1;
        Sideimage.SetActive(false);
        Drinkimage.SetActive(false);
    }

    public int ReturnFry()
    {
        return ConfirmFry;
    }

    public void FryUse()
    {
        SFX_FrySource.Play();
        num[0] = int.Parse(EventSystem.current.currentSelectedGameObject.ToString().Substring(0, 1));
        ftime = ftime1[int.Parse(EventSystem.current.currentSelectedGameObject.ToString().Substring(0, 1))-1] - PlayerPrefs.GetInt("FryUp") * 0.20f;
        OX = 0;
    }

    public void Drink1()
    {
        AudioSource.PlayClipAtPoint(SFX_Drink, transform.position, PlayerPrefs.GetFloat("SFXVolume"));
        num[1] = 1;
        Drinkimage.SetActive(true);
    }

    public void Drink2()
    {
        AudioSource.PlayClipAtPoint(SFX_Drink, transform.position, PlayerPrefs.GetFloat("SFXVolume"));
        num[1] = 2;
        Drinkimage.SetActive(true);
    }

    public void FryOut()
    {
        SFX_FrySource.Stop();
        if (ftime > 0f)
            num[0] = 0;
        else if (ftime > -1.0f)
            Debug.Log("O");
        else
            num[0] = 0;
        ftime = 0f;
        OX = 1;

        Sideimage.SetActive(true);
    }

    void SettingSetfalse()
    {
        FryCanvas.SetActive(false);
        FryCanvas.transform.position = new Vector2(-2000, 0);
    }
}
