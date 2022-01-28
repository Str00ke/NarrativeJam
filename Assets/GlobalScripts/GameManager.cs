using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

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
    public GameObject dialogBg;
    public GameObject charaUI;
    public bool isShowingDialog;
    public float dialogAnimSpeed;
    public AnimationCurve dialogAnim;
    public List<CharaID> ids = new List<CharaID>();
    public BarMan charaSpeaking;
    public bool isSpeaking = false;
    private void Start()
    {
        dialogBg.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1.5f);
        charaUI.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 2);

        //LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[i];
    }



    public void DialogAdvance()
    {
        charaSpeaking.perso.Advance();
    }

    public void Choix1()
    {
        charaSpeaking.Choix1();
    }

    public void Choix2()
    {
        charaSpeaking.Choix2();
    }

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
}
