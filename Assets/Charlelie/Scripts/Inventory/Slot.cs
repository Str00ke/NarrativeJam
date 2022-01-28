using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slot : MonoBehaviour
{
    public GameObject objectIn;
    void Start()
    {
        if (transform.childCount > 0)
        {
            objectIn = transform.GetChild(0).gameObject;
            objectIn.GetComponent<UIObject>().currSlot = this;
        }
    }


    void Update()
    {
        
    }

    public void SetObjToSlot()
    {
        objectIn = transform.GetChild(0).gameObject;
        objectIn.transform.position = transform.position;
        objectIn.GetComponent<UIObject>().currSlot = this;
    }
}
