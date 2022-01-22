using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogeManager : MonoBehaviour
{
    private Text dialogue;
    private int textN = 0;
    public List<Phase> phases;
    public float dialogueSpeed;
    public int phaseN;
    public GameObject[] reponse = new GameObject [2] ;
    private bool last;

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
                if (!phases[phaseN]._text[textN].reponse && OnSpeak.instance.onSpeak)
                {
                    textN++;
                    //dialogue.text = "";
                    if (textN < phases[phaseN]._text.Count && !last)
                        StartCoroutine(StartSpeak());
                    else
                    {
                        OnSpeak.instance.onSpeak = false;
                        gameObject.SetActive(false);
                        last = false;
                    }
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
        dialogue.text = "";
        bool stop = false;
        bool speakOnGate = false;
        string speak = phases[phaseN]._text[textN].dialogue;
        string gateOpen = "";
        string gateClose = "";

        for (int i = 0; i < speak.Length; i++)
        {
            if (speak[i] == '<')
                stop = true;



            if (!stop)
            {
                if (!speakOnGate)
                    dialogue.text += speak[i];
                else
                    dialogue.text += gateOpen + speak[i] + gateClose;
                yield return new WaitForSeconds(dialogueSpeed);
            }
            else
            {
                gateOpen += speak[i];
                gateClose += speak[i];
                if (gateClose == "<")
                    gateClose += '/';
            }

            if (speak[i] == '>' && stop)
            {
                stop = false;
                speakOnGate = true;
            }
            if (gateOpen == gateClose)
            {
                stop = false;
                speakOnGate = false;
            }
        }
        dialogue.text = speak;
        if (phases[phaseN]._text[textN].reponse)
        {
            reponse[0].SetActive(true);
            reponse[1].SetActive(true);
            reponse[0].GetComponentInChildren<Text>().text = phases[phaseN]._text[textN].reponseYes;
            reponse[1].GetComponentInChildren<Text>().text = phases[phaseN]._text[textN].reponseNo;
        }

    }

    public void spawnDialogue()
    {
        if((!OnSpeak.instance.onSpeak && OnSpeak.instance.idSpeaker != gameObject.GetInstanceID()) && phaseN < phases.Count)
        {
            OnSpeak.instance.onSpeak = true;
            OnSpeak.instance.idSpeaker = gameObject.GetInstanceID();
            textN = 0;
            gameObject.SetActive(true);
            dialogue = gameObject.GetComponentInChildren<Text>();
            StartCoroutine(StartSpeak());
        }
    }

    public void ReponseRight()
    {
        textN += 2;
        reponse[0].SetActive(false);
        reponse[1].SetActive(false);
        StartCoroutine(StartSpeak());
        last = true;
    }

    public void ReponseLeft()
    {
        textN++;
        reponse[0].SetActive(false);
        reponse[1].SetActive(false);
        StartCoroutine(StartSpeak());
        last = true;
    }

    [System.Serializable]
    public struct Phase
    {
        public List<Speak> _text;
    }

    [System.Serializable]
    public struct Speak
    {
        public string dialogue;
        public bool reponse;
        public string reponseNo;
        public string reponseYes;
    }
}

