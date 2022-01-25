using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    public Material hl, defaultMat;
    bool isHL = false;
    void Start()
    {
        GetComponent<SpriteRenderer>().material = defaultMat;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) UpdateHL();
    }

    void UpdateHL()
    {
        isHL = !isHL;
        if (isHL) GetComponent<SpriteRenderer>().material = hl;
        else GetComponent<SpriteRenderer>().material = defaultMat;
    }
}
