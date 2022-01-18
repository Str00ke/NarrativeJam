using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObject : MonoBehaviour
{
    [HideInInspector]
    public Slot currSlot;
    [HideInInspector]
    public Interctable onInteractable;
    
    public ObjectType type;

    void Start()
    {

    }


    void Update()
    {
        
    }

    public void DetachFromSlot()
    {
        currSlot.objectIn = null;
        currSlot = null;
    }
}
