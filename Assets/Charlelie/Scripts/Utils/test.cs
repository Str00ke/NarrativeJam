using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Component c in GetComponents<MonoBehaviour>())
        {
            Debug.Log(c.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
