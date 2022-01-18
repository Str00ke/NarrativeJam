using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region instance
    private static GameManager _instance;

    public static GameManager instance
    {
        get { return GetInstance(); }
    }

    static GameManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new GameManager();
        }
        return _instance;
    }

    #endregion

    public GameObject draggedObject;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

}
