using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogeManager : MonoBehaviour
{
    private Text dialogue;
    private int textN = 0;
    public List<string> text;
    public float dialogueSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                textN++;
                dialogue.text = "";
                if (textN < text.Count)
                    StartCoroutine(StartSpeak());
                else
                {
                    OnSpeak.instance.onSpeak = false;
                    gameObject.SetActive(false);
                }


            }
        }
    }

    public void StartDialogue()
    {
        StartCoroutine(StartSpeak());
    }

    public IEnumerator StartSpeak()
    {
        string speak = text[textN];
        for (int i = 0; i < text[textN].Length; i++)
        {
            dialogue.text += speak[i];
            yield return new WaitForSeconds(dialogueSpeed);
        }
    }

    public void spawnDialogue()
    {
        if(!OnSpeak.instance.onSpeak && OnSpeak.instance.idSpeaker != gameObject.GetInstanceID())
        {
            OnSpeak.instance.onSpeak = true;
            OnSpeak.instance.idSpeaker = gameObject.GetInstanceID();
            textN = 0;
            gameObject.SetActive(true);
            dialogue = gameObject.GetComponentInChildren<Text>();
            StartCoroutine(StartSpeak());
        }

    }
}
