using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MorningManager : MonoBehaviour
{
    public GameObject CWind1, CWind2, CText1, CText2, Character, Character1, Character3, LoadingCanvas;
    public GameObject [] Illust;
    public TextAsset txt1, txt2;
    public Text Script, Char1, Char2;
    private TextAsset txt;
    public Sprite [] SCloth;

    string[,] Sentence;
    int lineSize, rowSize;
    int i = 0;

    void Start()
    {
        LoadingCanvas.SetActive(false);
        switch (PlayerPrefs.GetInt("Day"))
        {
            case 1:
                txt = txt1;
                break;
            case 2:
                txt = txt2;
                break;
            default:
                LoadingCanvas.SetActive(true);
                SceneManager.LoadScene("GameScene_Daytime");
                break;
        }

        string currentText = txt.text.Substring(0, txt.text.Length - 1);
        string[] line = currentText.Split('\n');
        lineSize = line.Length;
        rowSize = line[0].Split('\t').Length;
        Sentence = new string[lineSize, rowSize];
        
        for (int i = 0; i < lineSize; i++)
        {
            string[] row = line[i].Split('\t');
            for (int j = 0; j < rowSize; j++)
            {
                Sentence[i, j] = row[j];
            }
        }
        for (int i = 0; i < Illust.Length; i++)
            Illust[i].SetActive(false);
    }

    void Update()
    {
        if (Equals(Sentence[i, 0], "주인공"))
        {
            for (int i = 0; i < Illust.Length; i++)
                Illust[i].SetActive(false);
            CWind1.SetActive(true);
            CWind2.SetActive(false);
            CText1.SetActive(true);
            CText2.SetActive(false);
            Char1.GetComponent<Text>().text = Sentence[i, 0];
            Character1.SetActive(true);
            Character3.SetActive(false);
        }
        else
        {
            CWind1.SetActive(false);
            CWind2.SetActive(true);
            CText1.SetActive(false);
            CText2.SetActive(true);
            Char2.GetComponent<Text>().text = "점장";
            if (Equals(Sentence[i, 0], "점장"))
            {
                for (int i = 0; i < Illust.Length; i++)
                    Illust[i].SetActive(false);
                Character1.SetActive(false);
                Character3.SetActive(true);
            }
            else
            {
                Character1.SetActive(false);
                Character3.SetActive(false);
                for (int i = 0; i < Illust.Length; i++)
                    Illust[i].SetActive(false);
                Illust[Convert.ToInt32(Sentence[i, 0])-1].SetActive(true);
            }
        }
        Script.GetComponent<Text>().text = Sentence[i, 1];
        if (Input.GetMouseButtonDown(0))
            i++;
        if (i == lineSize)
        {
            SceneManager.LoadScene("GameScene_Daytime");
            i = 0;
        }

        Character.gameObject.GetComponent<Image>().sprite = SCloth[PlayerPrefs.GetInt("SetCloth")];
    }
}