using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersoManager : MonoBehaviour
{
    public DialogeManager perso;
    [Header("dialogue")]
    public string[] dialogue;
    public bool[] question;
    public int step = 0;
    [Header("phase")]
    public int[] startPhase;
    public int phase = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
