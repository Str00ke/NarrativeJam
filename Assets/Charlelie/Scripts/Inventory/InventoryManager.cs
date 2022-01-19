using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Slot[] slots;
    public GameObject objPrefab;
    bool showIn = false;
    public GameObject bagBtn;
    public GameObject invGo;

    public float bagYMin, bagYMax;
    public float invYMin, invYMax;
    bool isTravellingDone = true;
    public float speed;

    private void Start()
    {
        bagBtn.transform.localPosition = new Vector3(914, bagYMin, 0);
        invGo.transform.localPosition = new Vector3(455, invYMin, 0);
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

    public void UpdateInventory()
    {
        if (!isTravellingDone) return;
        showIn = !showIn;
        if (showIn)
            StartCoroutine(Move(bagYMin, bagYMax, invYMin, invYMax));
        else
            StartCoroutine(Move(bagYMax, bagYMin, invYMax, invYMin));
    }

    IEnumerator Move(float start, float end, float istart, float iend)
    {
        isTravellingDone = false;
        float t = 0;

        Vector3 s = new Vector3(914, start, 0);
        Vector3 e = new Vector3(914, end, 0);

        Vector3 iS = new Vector3(455, istart, 0);
        Vector3 iE = new Vector3(455, iend, 0);
        while (t < 1)
        {
            bagBtn.transform.localPosition = Vector3.Lerp(s, e, t); 
            invGo.transform.localPosition = Vector3.Lerp(iS, iE, t); 
            t += Time.deltaTime * speed;
            yield return null;
        }
        isTravellingDone = true;
        yield return null;
    }
}
