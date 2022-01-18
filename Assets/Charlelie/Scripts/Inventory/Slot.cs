using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public GameObject objectIn;
    void Start()
    {
        Debug.Log(transform.childCount > 0);
        if (transform.childCount > 0)
        {
            objectIn = transform.GetChild(0).gameObject;
            objectIn.GetComponent<UIObject>().currSlot = this;
        }
    }


    void Update()
    {
        
    }
}
