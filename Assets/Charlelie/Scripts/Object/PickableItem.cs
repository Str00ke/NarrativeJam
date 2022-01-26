using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickableItem : MonoBehaviour, IPointerDownHandler
{
    public float speed;
    public AnimationCurve animEase;
    bool test = false;

    public GameObject uiObj;
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
        if (GameManager.instance.isShowingDialog) return;
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
        Vector2 scaleE = new Vector2(0.3f, 0.3f); //TODO: Get good scale
        while(t < 1)
        {
            transform.position = Vector2.Lerp(s, e, t);
            transform.localScale = Vector2.Lerp(scaleS, scaleE, t);
            t += Time.deltaTime * speed * animEase.Evaluate(t);
            yield return null;
        }             
        InventoryManager.instance.AddObjectWithObj(uiObj);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        if (InventoryManager.instance.showIn && test)
            InventoryManager.instance.UpdateInventory();
        
        test = false;
        Destroy(gameObject);
    }
}
