using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{

    #region instance
    private static MapManager _instance;

    public static MapManager instance
    {
        get { return GetInstance(); }
    }

    static MapManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new MapManager();
        }
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }

    #endregion


    string currLocSelected;
    public GameObject panel;
    string txt = "Do you want to go to "; 

    public void AskConfirm(string loc)
    {
        currLocSelected = loc;
        panel.SetActive(true);
        panel.transform.GetChild(0).GetComponent<Text>().text = txt + currLocSelected + " ?";
    }

    public void GotToLoc()
    {
        //GameManager.instance.LoadLevel(currLocSelected);
        //GameManager.instance.LoadLevelAsync(currLocSelected, UnityEngine.SceneManagement.LoadSceneMode.Single);
        FindObjectOfType<tmp>().LoadLevel(currLocSelected);
    }

    public void Cancel()
    {
        panel.SetActive(false);
        currLocSelected = "";
    }
}
