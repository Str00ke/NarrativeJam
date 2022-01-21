using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Text dialogue;
    public string text;
    public float dialogueSpeed;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = gameObject.GetComponentInChildren<Text>();    
    }

    // Update is called once per frame

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                StartCoroutine(StartSpeak());
            }
        }
    }
    
    public IEnumerator StartSpeak ()
    {
        for(int i = 0; i<text.Length-1;i++)
        {
            dialogue.text += text[i];
            yield return new WaitForSeconds(dialogueSpeed);
        }
    }
}
