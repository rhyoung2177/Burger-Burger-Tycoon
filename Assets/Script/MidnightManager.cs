using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MidnightManager : MonoBehaviour
{
    public GameObject ClothCanvas, UpgradeCanvas, LackmoneyCanvas, DecoCanvas, TimerUp, FryUp, CashUp, Character, Content, UpgradeText;
    public GameObject[] GCloth, CUpgrade, GDeco, TimerUpImage, FryUpImage, CashUpImage;
    public Sprite [] SCloth;
    public AudioClip SFX_Coin, SFX_X, SFX_BT;
    public Text Cloth1;
    Sequence mySequence;

    int[] UpgradePrice = {30000,50000,75000};
    int SettingCloth, UpgradeBT;
    string GetCloth, ClothPrice;
    
    void Start()
    {
        DecoUpdate();
        ClothUpdate();
        UpgradeUpdate();
    }

    //================================================================================== 윈도우 활성화/비활성화
    void WindowActive(GameObject WindowCanvas)
    {
        mySequence = DOTween.Sequence()
        .SetAutoKill(false) //추가
        .OnStart(() =>
        {
            WindowCanvas.SetActive(true);
            WindowCanvas.transform.localScale = Vector3.zero;
            WindowCanvas.transform.localPosition = new Vector2(0, 0);
            Content.transform.localPosition = new Vector2(0, 0);
        })
        .Append(WindowCanvas.transform.DOScale(1, 0.5f).SetEase(Ease.Unset));
    }
    public void UpgradeActive()
    {
        WindowActive(UpgradeCanvas);
    }
    public void ClothActive()
    {
        WindowActive(ClothCanvas);
    }
    public void DecoActive()
    {
        WindowActive(DecoCanvas);
    }
    void LackActive()
    {
        AudioSource.PlayClipAtPoint(SFX_X, transform.position, PlayerPrefs.GetFloat("SFXVolume"));
        WindowActive(LackmoneyCanvas);
    }

    void WindowInActive(GameObject WindowCanvas)
    {
        mySequence = DOTween.Sequence()
        .SetAutoKill(false) //추가
        .Append(WindowCanvas.transform.DOScale(0, 0.5f).SetEase(Ease.Unset));
        Invoke("LackmoneySetfalse", 0.5f);
    }
    public void LackInActive()
    {
        WindowInActive(LackmoneyCanvas);
        Invoke("LackmoneySetfalse", 0.5f);
    }
    public void ClothInActive()
    {
        WindowInActive(ClothCanvas);
        Invoke("LackmoneySetfase", 0.5f);
    }
    public void UpgradeInActive()
    {
        WindowInActive(UpgradeCanvas);
        Invoke("SettingSetfalse", 0.5f);
    }
    public void DecoInActive()
    {
        WindowInActive(DecoCanvas);
        Invoke("SettingSetfalse", 0.5f);
    }

    void SettingSetfalse()
    {
        UpgradeCanvas.SetActive(false);
        ClothCanvas.SetActive(false);
        DecoCanvas.SetActive(false);
    }

    void LackmoneySetfalse()
    {
        LackmoneyCanvas.SetActive(false);
    }

    public void LoadDaytime()
    {
        PlayerPrefs.SetInt("Day", PlayerPrefs.GetInt("Day") + 1);
        SceneManager.LoadScene("GameScene_Morning");
    }

    public void SettingFirst()
    {
        PlayerPrefs.SetInt("cloth1", 0);
        PlayerPrefs.SetInt("Day", 1);
    }

    //================================================================================== 업그레이드 윈도우
    void UpgradeUpdate()
    {
        UpgradeBT = 0;
        UpgradeSetFalse();
        UpgradeSetTrue("Fry", FryUpImage);
        UpgradeSetTrue("Cash", CashUpImage);
        UpgradeSetTrue("Timer", TimerUpImage);
        TimerUp.SetActive(true);
    }

    public void UpgradeTouch()
    {
        switch (UpgradeBT)
        {
            case 0:
                if (PlayerPrefs.GetInt("TimerUp") < 3)
                    UpgradeTouchChild("Timer", TimerUpImage);
                break;
            case 1:
                if (PlayerPrefs.GetInt("FryUp") < 3)
                    UpgradeTouchChild("Fry", FryUpImage);
                break;
            case 2:
                if (PlayerPrefs.GetInt("CashUp") < 3)
                    UpgradeTouchChild("Cash", CashUpImage);
                break;
            default:
                break;
        }
    }

    public void UpgradeTouchL()
    {
        UpgradeSetFalse();
        switch (UpgradeBT)
        {
            case 0:
                UpgradeBT = 2;  //2
                CashUp.SetActive(true);
                break;
            case 1:
                UpgradeBT--;    //0
                TimerUp.SetActive(true);
                break;
            case 2:
                UpgradeBT--;    //1
                FryUp.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void UpgradeTouchR()
    {
        UpgradeSetFalse();
        switch (UpgradeBT)
        {
            case 0:
                UpgradeBT++;  //1
                FryUp.SetActive(true);
                break;
            case 1:
                UpgradeBT++;    //2
                CashUp.SetActive(true);
                break;
            case 2:
                UpgradeBT = 0;    //0
                TimerUp.SetActive(true);
                break;
            default:
                break;
        }
    }

    void UpgradeSetFalse()
    {
        TimerUp.SetActive(false);
        FryUp.SetActive(false);
        CashUp.SetActive(false);
    }

    void UpgradeSetTrue(string strUp, GameObject [] imgUp)
    {
        string str = strUp + "Up";
        for (int i = 0; i < 9; i++)
            imgUp[i].SetActive(false);
        imgUp[PlayerPrefs.GetInt(str)].SetActive(true);
        imgUp[PlayerPrefs.GetInt(str) + 5].SetActive(true);
    }

    void UpgradeTouchChild(string strUp, GameObject[] imgUp)
    {
        string str = strUp + "Up";
        if (PlayerPrefs.GetInt("Money") >= UpgradePrice[PlayerPrefs.GetInt(str)])
        {
            AudioSource.PlayClipAtPoint(SFX_Coin, transform.position, PlayerPrefs.GetFloat("SFXVolume"));
            for (int i = 0; i < 9; i++)
                imgUp[i].SetActive(false);
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - UpgradePrice[PlayerPrefs.GetInt(str)]);
            PlayerPrefs.SetInt(str, PlayerPrefs.GetInt(str) + 1);
            imgUp[PlayerPrefs.GetInt(str)].SetActive(true);
            imgUp[PlayerPrefs.GetInt(str) + 5].SetActive(true);
        }
        else LackActive();
    }

    //================================================================================== 의상 교환 윈도우
    void ClothUpdate()
    {
        for (int i = 0; i < 7; i++)
        {
            GetCloth = "Cloth" + i.ToString();
            if (PlayerPrefs.GetInt(GetCloth) == 1)
                GCloth[i].GetComponent<Text>().text = "보유중";
        }
        Character.gameObject.GetComponent<Image>().sprite = SCloth[PlayerPrefs.GetInt("SetCloth")];
    }

    public void ClothPurchase()
    {
        GetCloth = "Cloth" + EventSystem.current.currentSelectedGameObject.ToString().Substring(0, 1);
        ClothPrice = "ClothPrice" + EventSystem.current.currentSelectedGameObject.ToString().Substring(0, 1);
        if (PlayerPrefs.GetInt(GetCloth) == 0) // 안 산 상태
        {
            if (PlayerPrefs.GetInt("Money") >= PlayerPrefs.GetInt(ClothPrice))
            {
                AudioSource.PlayClipAtPoint(SFX_Coin, transform.position, PlayerPrefs.GetFloat("SFXVolume"));
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - PlayerPrefs.GetInt(ClothPrice));
                PlayerPrefs.SetInt(GetCloth, 1);
                PlayerPrefs.SetInt("SetCloth", int.Parse(EventSystem.current.currentSelectedGameObject.ToString().Substring(0, 1)));
                ClothUpdate();
            }
            else LackActive();
        }
        else   // 산 상태 
        {
            AudioSource.PlayClipAtPoint(SFX_BT, transform.position, PlayerPrefs.GetFloat("SFXVolume"));
            PlayerPrefs.SetInt("SetCloth", int.Parse(EventSystem.current.currentSelectedGameObject.ToString().Substring(0, 1)));
            ClothUpdate();
        }
    }

    //================================================================================== 가게꾸미기 윈도우
    void DecoUpdate()
    {
        for (int i = 0; i < 6; i++)
        {
            GetCloth = "purchase" + (i + 1).ToString();
            if (PlayerPrefs.GetInt(GetCloth) == 1)
                GDeco[i].GetComponent<Text>().text = "보유중";
        }
    }

    public void TouchDeco()
    {
        string decotemp = "purchase" + EventSystem.current.currentSelectedGameObject.name;

        if (PlayerPrefs.GetInt(decotemp) == 1)       //산 경우
        {
            AudioSource.PlayClipAtPoint(SFX_BT, transform.position, PlayerPrefs.GetFloat("SFXVolume"));
            PlayerPrefs.SetInt("decostate", Convert.ToInt32(EventSystem.current.currentSelectedGameObject.name));
        }
        else
        {
            if (PlayerPrefs.GetInt("Money") >= 100000)
            {
                AudioSource.PlayClipAtPoint(SFX_Coin, transform.position, PlayerPrefs.GetFloat("SFXVolume"));
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 100000);
                PlayerPrefs.SetInt(decotemp, 1);
                PlayerPrefs.SetInt("decostate", Convert.ToInt32(EventSystem.current.currentSelectedGameObject.name));
            }
            else
                LackActive();
        }
        DecoUpdate();
    }
}
