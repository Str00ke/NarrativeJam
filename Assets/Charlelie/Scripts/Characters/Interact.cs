using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Interact : MonoBehaviour, IPointerDownHandler
{
    public CharaEnum charaId;

    bool isDialog = false;
    bool isShowing = false;
    Vector2 pivotDown = new Vector2(0.5f, 1.5f);
    Vector2 pivotUp = new Vector2(0.5f, 0);
    GameObject dialogBg;
    RectTransform dialogRect;
    float speed;
    AnimationCurve anim;
    
    RectTransform charaUIRect;
    Vector2 charaUIDown = new Vector2(0.5f, 2);
    Vector2 charaUIUp = new Vector2(0.5f, 0.5f);
    public bool dofDone = true;
    public bool isOnInteract = false;
    public Vector2 uiSize;


    private void Start()
    {
        dialogBg = GameManager.instance.dialogBg;
        dialogRect = dialogBg.GetComponent<RectTransform>();
        speed = GameManager.instance.dialogAnimSpeed;
        anim = GameManager.instance.dialogAnim;
        charaUIRect = GameManager.instance.charaUI.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameManager.instance.isSpeaking) return;
        UpdateDialogRender();
    }

    private void OnMouseDown()
    {
        if (GameManager.instance.isSpeaking) return;
        UpdateDialogRender();
    }


    public void UpdateDialogRender()
    {
        if (isShowing || !dofDone) return;
        isDialog = !isDialog;
        if (isDialog)
        {
            GameManager.instance.isSpeaking = true;
            Sprite spr = null;
            foreach (CharaID id in GameManager.instance.ids)
            {
                if (id.id == charaId)
                {
                    spr = id.img;
                    break;
                } 
            }
            GameManager.instance.charaUI.GetComponent<Image>().sprite = spr;
            GameManager.instance.charaUI.GetComponent<RectTransform>().sizeDelta = uiSize;
            InventoryManager.instance.bagBtn.SetActive(false);
            if (InventoryManager.instance.showIn) InventoryManager.instance.UpdateInventory();
            FindObjectOfType<DoF>().UpdateBlur();
            FindObjectOfType<ColorEffect>().UpdateColor();
            StartCoroutine(ShowDialog(pivotDown, pivotUp, charaUIDown, charaUIUp));
        }
        else 
        {
            InventoryManager.instance.bagBtn.SetActive(true);
            FindObjectOfType<DoF>().UpdateBlur();
            FindObjectOfType<ColorEffect>().UpdateColor();
            StartCoroutine(ShowDialog(pivotUp, pivotDown, charaUIUp, charaUIDown));
        } 
    }

    IEnumerator ShowDialog(Vector2 s, Vector2 e, Vector2 cs, Vector2 ce)
    {
        if (isDialog) GameManager.instance.isShowingDialog = true;
        
        
        isShowing = true;
        float t = 0;
        while(t < 1)
        {
            dialogRect.pivot = Vector2.Lerp(s, e, t);
            charaUIRect.pivot = Vector2.Lerp(cs, ce, t);
            t += Time.deltaTime * speed * anim.Evaluate(t);
            yield return null;
        }
        isShowing = false;
        if (!isDialog) 
        {
            GameManager.instance.isShowingDialog = false;
            GameManager.instance.isSpeaking = false;
            GameManager.instance.charaSpeaking = null;
        }
        if (isDialog) 
        {
            GetComponent<DialogeManager>().spawnDialogue();
            GameManager.instance.charaSpeaking = GetComponent<BarMan>();
        } 
        yield return null;
    }
}
