using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{

    #region instance
    private static InventoryManager _instance;

    public static InventoryManager instance
    {
        get { return GetInstance(); }
    }

    static InventoryManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new InventoryManager();
        }
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }

    #endregion


    public delegate void OnUp();
    public OnUp _delegate;

    public static Slot[] sSlots = new Slot[5];
    public Slot[] slots;
    public GameObject objPrefab;
    [HideInInspector]
    public bool showIn = false;
    public GameObject bagBtn;
    public GameObject invGo;

    Vector2 invDown;
    [HideInInspector]
    public Vector2 invUp;

    bool isTravellingDone = true;
    public float speed;

    public AnimationCurve animEase;
    public GameObject beer, dyna, shovel, lighter;
    GameObject[] goList = new GameObject[5];

    private void Start()
    {
        if (inventoryData.beer)
        {
            AddObjectWithObj(beer);
        }
        if (inventoryData.dynas)
        {
            AddObjectWithObj(dyna);
            AddObjectWithObj(dyna);
        }
        if (inventoryData.shovel)
        {
            AddObjectWithObj(shovel);
        }
        if (inventoryData.lighter)
        {
            AddObjectWithObj(lighter);
        }
        for (int i = 0; i < 5; i++)
        {
            if (InventoryManager.instance.goList[i] != null)
                Debug.Log(InventoryManager.instance.goList[i].name);
        }
        
        

        invUp = Vector2.right; //(0, 1)
        invDown = Vector2.one; //(1, 1)
        invGo.GetComponent<RectTransform>().pivot = invDown;
        bagBtn.GetComponent<RectTransform>().pivot = invDown;
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

    public void AddObjectWithObj(GameObject objGo)
    {
        GameObject obj = Instantiate(objGo);
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].objectIn == null)
            {
                if (objGo.GetComponent<UIObject>().type == ObjectType.BEER)
                    inventoryData.beer = true;

                else if (objGo.GetComponent<UIObject>().type == ObjectType.DYNAMITE)
                    inventoryData.dynas = true;

                else if(objGo.GetComponent<UIObject>().type == ObjectType.SHOVEL)
                    inventoryData.shovel = true;

                else if(objGo.GetComponent<UIObject>().type == ObjectType.ZIPPER)
                    inventoryData.lighter = true;

                obj.transform.parent = slots[i].transform;
                slots[i].SetObjToSlot();
                break;
            }
        }
    }

    public Slot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].objectIn == null)
            {
                return slots[i];
            }
        }
        return null;
    }

    public void test(System.Action<bool> done)
    {
        //if (UpdateInventory()) done(true);
        //done(UpdateInventory());
    }

    public void UpdateInventory()
    {
        if (!isTravellingDone) return;
        showIn = !showIn;
        if (showIn)
            StartCoroutine(Move(invDown, invUp));
        else
            StartCoroutine(Move(invUp, invDown));
    }

    IEnumerator Move(Vector2 istart, Vector2 iend)
    {
        isTravellingDone = false;
        float t = 0;

        RectTransform invRect = invGo.GetComponent<RectTransform>();
        RectTransform bagRect = bagBtn.GetComponent<RectTransform>();
        while (t < 1)
        {
            invRect.pivot = Vector3.Lerp(istart, iend, t);
            bagRect.pivot = Vector3.Lerp(istart, iend, t); 
            t += Time.deltaTime * speed * (1 - animEase.Evaluate(t));
            yield return null;
        }
        invRect.pivot = iend;
        bagRect.pivot = iend;
        isTravellingDone = true;
        if (istart == invDown && _delegate != null)
            _delegate();
        yield return null;
    }
}
