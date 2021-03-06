using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool isDragged = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (isDragged)
        {
            transform.position = Input.mousePosition;
        }
    }

    #region PC
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.instance.draggedObject = gameObject;
        isDragged = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GameManager.instance.draggedObject = null;
        isDragged = false;
        if (GetComponent<UIObject>().onInteractable)
        {
            GetComponent<UIObject>().onInteractable.Interact(GetComponent<UIObject>());
        }
        if (GetComponent<UIObject>().currSlot != null)
            transform.position = GetComponent<UIObject>().currSlot.gameObject.transform.position;
    }

    #endregion
}
