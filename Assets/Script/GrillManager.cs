using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class GrillManager : MonoBehaviour
{
    public GameObject[] stack;
    public Sprite[] stacksprite;
    public GameObject GrillCanvas, O, X;
    public AudioClip SFX_O, SFX_X, SFX_BT;
    public Text Temp;
    string num, num2;
    Sequence mySequence;

    int i = 0;
    public int ConfirmGrill = 1;

    void Start()
    {
        GrillCanvas.transform.position = new Vector2(2000, 0);
        GrillCanvas.SetActive(false);
        O.SetActive(false);
        X.SetActive(false);

        for (int i = 0; i < 9; i++)
        {
            stack[i].GetComponent<Image>().color = new Color32(255, 255, 255, 0);
            stack[i].gameObject.GetComponent<Image>().sprite = stacksprite[9];
        }
        
    }

    public void GrillActive()
    {
        mySequence = DOTween.Sequence()
        .SetAutoKill(false) //추가
        .OnStart(() =>
        {
            GrillCanvas.SetActive(true);
            GrillCanvas.transform.localScale = Vector3.zero;
            GrillCanvas.transform.localPosition = new Vector2(0, 0);
        })
        .Append(GrillCanvas.transform.DOScale(1, 0.5f).SetEase(Ease.Unset));
        num2 = GameObject.Find("Cashermanager").GetComponent<CasherManager>().OrderBurger();
    }

    public void GrillInactive()
    {
        mySequence = DOTween.Sequence()
        .SetAutoKill(false) //추가
        .Append(GrillCanvas.transform.DOScale(0, 0.5f).SetEase(Ease.Unset));
        Invoke("SettingSetfalse", 0.5f);
        
        Temp.GetComponent<Text>().text = "";

        for (int i = 0; i < 9; i++)
        {
            if (stack[i] != null)
            {
                stack[i].GetComponent<Image>().color = new Color32(255, 255, 255, 0);
                stack[i].gameObject.GetComponent<Image>().sprite = stacksprite[9];
            }
        }
        
        if (Equals(num, num2))
        {
            ConfirmGrill = 0;
            AudioSource.PlayClipAtPoint(SFX_O, transform.position, PlayerPrefs.GetFloat("SFXVolume"));
            OXview(O);
        }
        else
        {
            ConfirmGrill = 1;
            AudioSource.PlayClipAtPoint(SFX_X, transform.position, PlayerPrefs.GetFloat("SFXVolume"));
            OXview(X);
        }
        i = 0;
        num = "";
    }

    void OXview(GameObject OX)
    {
        mySequence = DOTween.Sequence()
        .SetAutoKill(false) //추가
        .OnStart(() =>
        {
            OX.SetActive(true);
            OX.transform.localScale = Vector3.zero;
            OX.transform.localPosition = new Vector2(0, 0);
        })
        .Append(OX.transform.DOScale(1, 0.5f).SetEase(Ease.Unset))
        .Append(OX.transform.DOScale(0, 0.5f).SetEase(Ease.Unset));
    }

    public int ReturnGrill()
    {
        return ConfirmGrill;
    }

    public void UseBT()
    {
        AudioSource.PlayClipAtPoint(SFX_BT, transform.position, PlayerPrefs.GetFloat("SFXVolume"));
        string stackchar = EventSystem.current.currentSelectedGameObject.ToString().Substring(2, 1);
        if (i <= 8)
        {
            i++;
            num = num + stackchar;
            stack[i - 1].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            if (stackchar == "1" && i != 1)
            {
                stack[i - 1].gameObject.GetComponent<Image>().sprite = stacksprite[8];
                return; 
            }
            stack[i - 1].gameObject.GetComponent<Image>().sprite = stacksprite[Convert.ToInt32(stackchar) - 1];
        }
        if (i >= 9)
            Temp.GetComponent<Text>().text = "더 쌓을 수 없습니다.";
    }
    void SettingSetfalse()
    {
        GrillCanvas.SetActive(false);
        GrillCanvas.transform.position = new Vector2(2000, 0);
    }
}
