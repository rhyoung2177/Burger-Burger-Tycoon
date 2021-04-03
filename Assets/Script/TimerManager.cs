using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public GameObject Timer, Money, Day, Repute, SettingCanvas;
    private float time;

    private void Awake()
    {
        time = 90f + PlayerPrefs.GetInt("TimerUp")*15f;
        if(Equals(SceneManager.GetActiveScene().name, "GameScene_Daytime"))
            StartCoroutine("SettingTimer");
    }

    private void Update()
    {
        Money.GetComponent<Text>().text = PlayerPrefs.GetInt("Money").ToString();
        Day.GetComponent<Text>().text = PlayerPrefs.GetInt("Day").ToString();
        Repute.GetComponent<Text>().text = PlayerPrefs.GetInt("Repute").ToString();
    }

    public void MoneySet()
    {
        PlayerPrefs.SetInt("Money", 0);
    }

    public void MoneyManager(int change)
    {
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + change);
    }

    IEnumerator SettingTimer()
    {
        yield return new WaitForSeconds(1f);
        if ( SettingCanvas.activeSelf == false)
            time--;
        StartCoroutine("SettingTimer");
        Timer.GetComponent<Text>().text = Mathf.Ceil(time).ToString();
        if (time <= 0)
            SceneManager.LoadScene("GameScene_Midnight");
        
    }
}
