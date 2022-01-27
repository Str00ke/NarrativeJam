using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefIndien : PersoManager
{
    public bool confiance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (confiance)
        {
            phase = 1;
        }
        else
            phase = 0;

    }
}
