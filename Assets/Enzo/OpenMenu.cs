using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using UnityEngine.UI;
public class OpenMenu : MonoBehaviour
{
    public GameObject optionMenuUI;
    private bool isOpen = false;

    public void SetSelectedLocale(Locale locale)
    {
        LocalizationSettings.SelectedLocale = locale;
    }

    public void Option()
    {
        isOpen = !isOpen;
        optionMenuUI.SetActive(isOpen);
    }

    public void En()
    {
        SetSelectedLocale(LocalizationSettings.AvailableLocales.Locales[0]);
    }

    public void Fr()
    {
        SetSelectedLocale(LocalizationSettings.AvailableLocales.Locales[1]);
    }

}
