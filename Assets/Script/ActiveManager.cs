using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveManager : MonoBehaviour
{
    public GameObject[] GrillIcon, FryIcon, Icon;
    public GameObject PlayerIcon;
    public Sprite[] IconResource, PlayerIconSprite;
    
    void Start()
    {
        int n = PlayerPrefs.GetInt("Day");
        if (n < 7)
            for (int i = 0; i < 3; i++)
                GrillIcon[i].SetActive(false);
        PlayerIcon.GetComponent<SpriteRenderer>().sprite = PlayerIconSprite[PlayerPrefs.GetInt("SetCloth")];

        for (int i = 0; i < 4; i++)
           FryIcon[i].SetActive(false);
        if (n >= 5)
            FryIcon[0].SetActive(true);
        if (n >= 10)
        {
            FryIcon[1].SetActive(true);
            FryIcon[2].SetActive(true);
        }
        if (n >= 17)
            FryIcon[3].SetActive(true);

        for (int i = 0; i < Icon.Length; i++)
            Icon[i].gameObject.GetComponent<SpriteRenderer>().sprite = IconResource[5*(PlayerPrefs.GetInt("decostate")-1) + i];
    }
}
