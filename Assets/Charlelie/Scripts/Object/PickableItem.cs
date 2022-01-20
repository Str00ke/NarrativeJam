using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickableItem : MonoBehaviour, IPointerDownHandler
{
    public float speed;
    public AnimationCurve animEase;
    bool test = false;
    void Start()
    {
        
    }



    private void OnMouseDown()
    {
        Picked();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Picked();
    }

    public void Picked()
    {        
        Slot slot = InventoryManager.instance.GetEmptySlot();
        if (!InventoryManager.instance.showIn) 
        {
            test = true;
            InventoryManager.instance.UpdateInventory();
            InventoryManager.instance._delegate += OpenInventory;
        } 
        else
            if (slot != null)
                StartCoroutine(GoToSlot(slot));
    }


    void OpenInventory()
    {
        Slot slot = InventoryManager.instance.GetEmptySlot();
        InventoryManager.instance._delegate -= OpenInventory;
        if (slot != null)
            StartCoroutine(GoToSlot(slot));
    }

    IEnumerator GoToSlot(Slot slot)
    {
        float t = 0;
        Vector2 s = transform.position;
        Vector2 e = Camera.main.ScreenToWorldPoint(slot.transform.position);

        Vector2 scaleS = transform.localScale;
        Vector2 scaleE = new Vector2(5, 5); //TODO: Get good scale
        while(t < 1)
        {
            transform.position = Vector2.Lerp(s, e, t);
            transform.localScale = Vector2.Lerp(scaleS, scaleE, t);
            t += Time.deltaTime * speed * animEase.Evaluate(t);
            yield return null;
        }
        if (InventoryManager.instance.showIn && test)
            InventoryManager.instance.UpdateInventory();
        test = false;
        yield return null;
    }
}
