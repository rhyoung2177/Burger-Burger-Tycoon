using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FrontManager : MonoBehaviour
{
    public GameObject OrderText, Guest;
    public AudioClip SFX_Coin, SFX_Angry;
    int Burger, Fry, Price;
    Sequence mySequence;

    public void ConfirmOrder()
    {
        Burger = GameObject.Find("Grillmanager").GetComponent<GrillManager>().ReturnGrill();
        Fry = GameObject.Find("Frymanager").GetComponent<FryManager>().ReturnFry();
        Price = GameObject.Find("Cashermanager").GetComponent<CasherManager>().ReturnPrice();

        if (PlayerPrefs.GetInt("Guest") == 0)
        {
            if (Burger == 0 && Fry == 0)
            {
                AudioSource.PlayClipAtPoint(SFX_Coin, transform.position, PlayerPrefs.GetFloat("SFXVolume"));
                OrderText.gameObject.GetComponent<TextMesh>().text = "감사합니다!";
                SuccessOrder();
            }
            else
            {
                AudioSource.PlayClipAtPoint(SFX_Angry, transform.position, PlayerPrefs.GetFloat("SFXVolume"));
                OrderText.gameObject.GetComponent<TextMesh>().text = "이게 뭐야...";
                FailOrder();
            }
        }
        Invoke("GuestMove", 1f);
    }

    void GuestMove()
    {
        mySequence = DOTween.Sequence()
        .SetAutoKill(false)
        .Append(Guest.transform.DOMoveY(-12f, 1))
        .Append(Guest.transform.DOMoveX(4f, 1));
        Invoke("GuestActive", 2f);
    }

    void GuestActive()
    {
        int[] temp = new int[3];
        OrderText.gameObject.GetComponent<TextMesh>().text = "안녕하세요!";
        for (int i = 0; i < 3; i++)
            temp[i] = Random.Range(0, 2);
        Guest.GetComponent<SpriteRenderer>().color = new Color(temp[0] * 255, temp[1] * 255, temp[2] * 255, 255);
        mySequence = DOTween.Sequence()
      .SetAutoKill(false) 
      .Append(Guest.transform.DOMoveY(-4f, 1));
    }
    
    void SuccessOrder()
    {
        Debug.Log(PlayerPrefs.GetInt("Money") + Price + PlayerPrefs.GetInt("CashUp") * 300);
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + Price + PlayerPrefs.GetInt("CashUp")*300);
        PlayerPrefs.SetInt("Repute", PlayerPrefs.GetInt("Repute") + 1);
        PlayerPrefs.SetInt("Guest", 1);
        Burger = 1;
        Fry = 1;
        GameObject.Find("Grillmanager").GetComponent<GrillManager>().ConfirmGrill = 1;
        GameObject.Find("Frymanager").GetComponent<FryManager>().ConfirmFry = 1;
    }

    void FailOrder()
    {
        if(PlayerPrefs.GetInt("Repute") >= 1)
            PlayerPrefs.SetInt("Repute", PlayerPrefs.GetInt("Repute") - 1);
        PlayerPrefs.SetInt("Guest", 1);
    }
}
