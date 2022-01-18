using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneClick : MonoBehaviour
{
    public GameObject targetTopLeft;
    public GameObject targetDownRight;
    public DialogeManager dialogue;
    public GameObject bulle;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            //Debug.Log("touch");
            if (touch.phase == TouchPhase.Began)
            {
                if ((touch.position.x <= targetDownRight.transform.position.x && touch.position.x >= targetTopLeft.transform.position.x) && (touch.position.y >= targetDownRight.transform.position.y && touch.position.y <= targetTopLeft.transform.position.y))
                {
                    Debug.Log("touch in zone");
                    bulle.SetActive(true);
                    dialogue.StartDialogue();
                }
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("touch");
    }
}
