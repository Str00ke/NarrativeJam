using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interctable : MonoBehaviour
{
    public ObjectType type;
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void Interact(UIObject obj)
    {
        if (type == obj.type)
        {
            Destroy(gameObject);
            obj.DetachFromSlot();
            Destroy(obj.gameObject);
        }
        else
            OnExit();
    }

    #region PC

    private void OnMouseEnter()
    {
        if (FindObjectOfType<GameManager>().draggedObject != null)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
            GameManager.instance.draggedObject.GetComponent<UIObject>().onInteractable = this;
        }
    }

    private void OnMouseExit()
    {
        if (FindObjectOfType<GameManager>().draggedObject != null)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            GameManager.instance.draggedObject.GetComponent<UIObject>().onInteractable = null;
        }
    }

    public void OnExit()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    #endregion
}
