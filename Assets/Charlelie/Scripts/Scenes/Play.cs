using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class Play : MonoBehaviour
{
    public void BtnPlay()
    {
        CharaFlags.Reset();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Saloon");
    }

    public void ChangeLangages(int l)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[l];
    }
}
