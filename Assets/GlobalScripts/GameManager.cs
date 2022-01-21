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

    private void Awake()
    {
        _instance = this;
    }

    #endregion

    public GameObject draggedObject;


    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevel(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void LoadLevelAsync(string sceneName, UnityEngine.SceneManagement.LoadSceneMode type)
    {
        StartCoroutine(LoadAsync(sceneName, type));
    }

    IEnumerator LoadAsync(string sceneName, UnityEngine.SceneManagement.LoadSceneMode type)
    {
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, type);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }

}
