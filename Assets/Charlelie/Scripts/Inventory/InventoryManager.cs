using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Slot[] slots;
    public GameObject objPrefab;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void AddObject()
    {
        GameObject obj = Instantiate(objPrefab);
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].objectIn == null)
            {
                obj.transform.parent = slots[i].transform;
                slots[i].SetObjToSlot();
                break;
            }
        }
    }
}
