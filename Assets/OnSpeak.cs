using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSpeak : MonoBehaviour
{
    public bool onSpeak = false;
    public int idSpeaker = 0;

    static public OnSpeak instance;
    public void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began && !onSpeak )
            {
                idSpeaker = 0;
            }
        }
    }

}
