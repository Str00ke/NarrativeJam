using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlock : MonoBehaviour
{
    public bool everyCompsLocked = true;

    public List<Component> compsToLock = new List<Component>();
    void Start()
    {
        if (compsToLock.Count > 0)
            foreach (MonoBehaviour c in compsToLock)
                c.enabled = false;

        if (everyCompsLocked)
        {
            foreach (MonoBehaviour c in GetComponents(typeof(MonoBehaviour)))
            {
                c.enabled = false;
            }
        } 
    }

    void Update()
    {

    }

    public void UnlockComponent(MonoBehaviour comp)
    {
        comp.enabled = true;
    }

    public void UnlockAll()
    {
        foreach (MonoBehaviour c in compsToLock)
            c.enabled = true;
    }

}
