using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject GrillCanvas, FryCanvas, SettingCanvas;
    
    void Start()
    {
        PlayerPrefs.SetInt("Guest", 1);
    }
    
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (Input.GetMouseButtonDown(0) && hit.collider != null && GrillCanvas.activeSelf == false && FryCanvas.activeSelf == false && SettingCanvas.activeSelf == false)
        {
            RaycastPlay(hit);
        }
    }

    void RaycastPlay(RaycastHit2D hit)
    {
        this.transform.position = hit.transform.position;
        if (hit.collider.tag == "Grill")
            GameObject.Find("Grillmanager").GetComponent<GrillManager>().GrillActive();
        else if (hit.collider.tag == "Fry")
            GameObject.Find("Frymanager").GetComponent<FryManager>().FryActive();
        else if (hit.collider.tag == "Casher")
        {
            GameObject.Find("Cashermanager").GetComponent<CasherManager>().TouchGuest1();
            GameObject.Find("Cashermanager").GetComponent<CasherManager>().TouchGuest();
        }
        else if (hit.collider.tag == "Front")
            GameObject.Find("Frontmanager").GetComponent<FrontManager>().ConfirmOrder();
        else return;
    }
}
