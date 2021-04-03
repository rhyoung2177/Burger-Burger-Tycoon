using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;

public class ExpandAnimation : MonoBehaviour
{
    public GameObject DO1, DO2, DO3, DO4, DO5, Text1, Text2, Text3;
    Sequence mySequence;

    void Start()
    {
        mySequence = DOTween.Sequence()
       .SetAutoKill(false) //추가
       .OnStart(() =>
       {
           DO1.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
           DO2.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
           DO3.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
           Text1.GetComponent<Text>().color = new Color32(255, 255, 255, 0); 
           Text2.GetComponent<Text>().color = new Color32(255, 255, 255, 0);
           Text3.GetComponent<Text>().color = new Color32(255, 255, 255, 0);
           DO5.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
           DO4.transform.localScale = Vector3.zero;
           DO4.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
           
       })
       .Append(DO4.transform.DOScale(1, 1).SetEase(Ease.OutBounce))
       .Join(DO4.GetComponent<Image>().DOFade(1, 1))
       .Append(DO1.GetComponent<Image>().DOFade(1, 1))
       .Join(Text1.GetComponent<Text>().DOFade(1, 1))
       .Append(DO2.GetComponent<Image>().DOFade(1, 1))
       .Join(Text2.GetComponent<Text>().DOFade(1, 1))
       .Append(DO3.GetComponent<Image>().DOFade(1, 1))
       .Join(Text3.GetComponent<Text>().DOFade(1, 1))
       .Append(DO5.transform.DOMoveY(150,1).SetEase(Ease.OutBounce))
       .Join(DO5.GetComponent<Image>().DOFade(1, 1));
    }

    //void OnEnable()
    //{
    //    mySequence.Restart();
    //}
}
