using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenMenu : MonoBehaviour
{
    public GameObject optionMenuUI;
    private bool isOpen = false;

    public void Option()
    {
        isOpen = !isOpen;
        optionMenuUI.SetActive(isOpen);
    }

}
