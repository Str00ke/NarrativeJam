using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaloonStart : MonoBehaviour
{
    public GameObject billyJoe;
    public GameObject bodySnatcher;
    void Start()
    {
        if (CharaFlags.wentToMine) billyJoe.SetActive(true);
        else billyJoe.SetActive(false);
        if (CharaFlags.wentToTrapperHouse) bodySnatcher.SetActive(true);
        else bodySnatcher.SetActive(false);
    }
}
