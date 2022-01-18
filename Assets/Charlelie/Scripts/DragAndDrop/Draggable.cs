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
            transform.position = Input.mousePosition;
    }

    #region PC
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.instance.draggedObject = this.gameObject;
        isDragged = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GameManager.instance.draggedObject = null;
        isDragged = false;
        transform.position = GetComponent<UIObject>().currSlot.gameObject.transform.position;
    }

    #endregion
}
