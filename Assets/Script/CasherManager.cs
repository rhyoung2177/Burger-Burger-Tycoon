using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;

public class CasherManager : MonoBehaviour
{
    public GameObject OrderText, Guest;
    string burger, burgerText, fryText, drinkText;
    int n, r1, r2, r3, price;
    Sequence mySequence;
    
    public string OrderBurger()
    {
        return burger;
    }

    public string OrderFry()
    {
        return r3.ToString();
    }

    public int ReturnPrice()
    {
        return price;
    }

    public void TouchGuest()
    {
        n = PlayerPrefs.GetInt("Day");
        if (n < 3)                              //1-3일차
            r1 = Random.Range(1, 3);
        else if (n < 5)                         //3-5일차
            r1 = Random.Range(1, 4);
        else if (n < 7)                         //5-7일차
            r1 = Random.Range(1, 5);
        else if (n < 10)                         //7-10일차
            r1 = Random.Range(1, 6);
        else if (n < 15)                         //10-15일차
            r1 = Random.Range(1, 7);
        else                                     //15일차 이후
            r1 = Random.Range(1, 8);


        if (n < 5)                               //1-5일차
            r2 = Random.Range(1, 3);
        else if (n < 5)                     //5-10일차 
            r2 = Random.Range(1, 4);
        else if (n < 10)                     
            r2 = Random.Range(1, 5);
        else if (n < 17)                     //10-17일차
            r2 = Random.Range(1, 7);
        else
            r2 = Random.Range(1, 8);              //17일차 이후

        switch (r2)
        {
            case 1:
                fryText = "감자튀김";
                price = 1500;
                r3 = 2;
                break;
            case 2:
                fryText = "치즈스틱";
                price = 2000;
                r3 = 1;
                break;
            case 3:
                fryText = "치킨너겟";
                price = 1000;
                r3 = 2;
                break;
            case 4:
                fryText = "쉑쉑치킨";
                price = 2000;
                r3 = 3;
                break;
            case 5:
                fryText = "화이어윙";
                price = 2500;
                r3 = 5;
                break;
            case 6:
                fryText = "치킨휠레";
                price = 2500;
                r3 = 4;
                break;
            case 7:
                fryText = "치킨";
                price = 5000;
                r3 = 6;
                break;
        }

        r3 = r3*10 + Random.Range(1, 3);

        switch (r3 % 10)
        {
            case 1:
                drinkText = "콜라";
                break;
            case 2:
                drinkText = "사이다";
                break;
        }

        switch (r1)
        {
            case 1:
                burger = "1231";                 //미트버거
                burgerText = "미트버거";
                price += 2000;
                break;
            case 2:
                burger = "1251";                 //치즈버거
                burgerText = "치즈버거";
                price += 2000;
                break;
            case 3:
                burger = "1241";                 //비프버거
                burgerText = "비프버거";
                price += 2500;
                break;
            case 4:
                burger = "12351";                //버거버거
                burgerText = "버거버거";
                price += 3000;
                break;
            case 5:
                burger = "128671";                //야채버거
                burgerText = "야채버거";
                price += 3500;
                break;
            case 6:
                burger = "1823241";                //더블버거
                burgerText = "더블버거";
                price += 4000;
                break;
            case 7:
                burger = "123456781";                //올인원버거
                burgerText = "올인원버거";
                price += 5000;
                break;
        }
        
        OrderText.gameObject.GetComponent<TextMesh>().text = "" + burgerText + "하나랑 \n" + fryText + " 부탁드리구요,\n음료는 " + drinkText + "로 주세요!";
        PlayerPrefs.SetInt("Guest", 0);
        Invoke("GuestMove", 1f);
    }

    void GuestMove()
    {
        mySequence = DOTween.Sequence()
      .SetAutoKill(false) //추가
      .Append(Guest.transform.DOMoveX(-2.5f, 1));
    }

    public void TouchGuest1()
    {
        if (PlayerPrefs.GetInt("Repute") >= 1 && PlayerPrefs.GetInt("Guest") == 0)
            PlayerPrefs.SetInt("Repute", PlayerPrefs.GetInt("Repute") - 1);
    }
}
