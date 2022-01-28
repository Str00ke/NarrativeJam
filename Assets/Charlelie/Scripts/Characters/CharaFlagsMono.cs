using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class CharaFlags
{
    public static bool minerGotBeer = false;
    public static bool indianChiefConfiance = false;
    public static bool wentToMine = false;
    public static bool wentToTrapperHouse = false;

    public static void Reset()
    {
        minerGotBeer = false;
        indianChiefConfiance = false;
        wentToMine = false;
        wentToTrapperHouse = false;
    }
}

[System.Serializable]
public enum flags
{
    MINER_BEER,
    INDIAN_CONF,
    WENT_MINE
}

public class CharaFlagsMono : MonoBehaviour
{
    
    public void SetFlag(int flag)
    {
        switch (flag)
        {
            case 1:
                CharaFlags.minerGotBeer = true;
                break;

            case 2:
                CharaFlags.indianChiefConfiance = true;
                break;

            case 3:
                CharaFlags.wentToMine = true;
                break;
        }
    }

    public void AddObject(GameObject obj)
    {
        InventoryManager.instance.AddObjectWithObj(obj);
    }
}
