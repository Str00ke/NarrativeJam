using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Location : MonoBehaviour, IPointerDownHandler
{
    public string locName;
    public void OnPointerDown(PointerEventData data)
    {
        MapManager.instance.AskConfirm(locName);
    }

}
