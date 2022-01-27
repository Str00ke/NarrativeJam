using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;

public class DialogeManager : MonoBehaviour
{
    private Text dialogue;
    public float dialogueSpeed;
    public GameObject[] reponse = new GameObject [2] ;
    public LocalizedString loca;
    public BarMan barman;
    public bool whait = false;
    public bool spawnBooton = false;

    private void Awake()
    {
        
        //loca = gameObject.GetComponentInChildren<L>();
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }
    public string GetText()
    {
        return dialogue.text;
    }
    // Update is called once per frame

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began && !whait)
            {
                

                //dialogue.text = loca.GetLocalizedString();
                if ( OnSpeak.instance.onSpeak && !barman.question[barman.step])
                {
                    //loca.SetReference(loca.TableReference, "intro Barman 2");
                    //textN++;
                    //dialogue.text = "";
                    barman.step++;
                    if (barman.dialogue[barman.step] != "stop")
                    {

                        loca.SetReference(loca.TableReference, barman.dialogue[barman.step]);
                        StartCoroutine(StartSpeak());

                    }
                    else
                    {
                        barman.step = barman.startPhase[barman.phase];
                        loca.SetReference(loca.TableReference, barman.dialogue[barman.step]);
                        OnSpeak.instance.onSpeak = false;
                        gameObject.SetActive(false);
                        //last = false;
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
        whait = true;
        dialogue.text = "";
        bool stop = false;
        bool speakOnGate = false;
        string speak = loca.GetLocalizedString();//phases[phaseN]._text[textN].dialogue;
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
        whait = false;

        spawnBooton = true;
        yield return new WaitForSeconds(0.1f);
        spawnBooton = false;

    }

    public void spawnDialogue()
    {
        if((!OnSpeak.instance.onSpeak && OnSpeak.instance.idSpeaker != gameObject.GetInstanceID()))
        {
            OnSpeak.instance.onSpeak = true;
            OnSpeak.instance.idSpeaker = gameObject.GetInstanceID();
            
            gameObject.SetActive(true);
            dialogue = gameObject.GetComponentInChildren<Text>();
            loca.SetReference(loca.TableReference, barman.dialogue[barman.step]);
            StartCoroutine(StartSpeak());
        }
    }
    /*
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
    }*/
}

