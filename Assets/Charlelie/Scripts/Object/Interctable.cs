using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interctable : MonoBehaviour
{
    public ObjectType[] types;
    int nbrObj;
    public UnityEvent consequences;
    List<GameObject> objUsed = new List<GameObject>();
    void Start()
    {
        nbrObj = types.Length;
    }


    void Update()
    {
        
    }

    public void Interact(UIObject obj)
    {
        for (int i = 0; i < types.Length; i++)
        {
            if (types[i] == obj.type)
            {
                obj.DetachFromSlot();
                obj.gameObject.GetComponent<Draggable>().enabled = false;
                obj.transform.SetParent(obj.transform.parent.parent.parent);

                //obj.transform.position = Camera.main.WorldToScreenPoint(transform.position);
                obj.transform.position = Input.mousePosition;

                objUsed.Add(obj.gameObject);
                nbrObj--;
                if (nbrObj <= 0)
                {
                    consequences.Invoke();
                    foreach (GameObject objs in objUsed) Destroy(objs);
                    Destroy(gameObject);
                    return;
                }
                else return;
            }
        }
        
        OnExit();
    }

    #region PC

    private void OnMouseEnter()
    {
        if (GameManager.instance.isShowingDialog) return;
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
