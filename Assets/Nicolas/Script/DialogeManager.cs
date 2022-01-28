using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;


public class DialogeManager : MonoBehaviour
{
    public bool flagChoice;
    public Text dialogue;
    public Text speakerName;
    public string charaName;
    public float dialogueSpeed;
    public GameObject[] reponse = new GameObject [2] ;
    public LocalizedString loca;
    public BarMan barman;
    public bool whait = false;
    public bool spawnBooton = false;
    bool ok;
    bool dialogDone = true;
    public bool doneSpeaking = false;
    public bool askingQuestion = false;
    

    private void Awake()
    {
        
        //loca = gameObject.GetComponentInChildren<L>();
    }

    void Start()
    {
       
    }
    public string GetText()
    {
        return dialogue.text;
    }


    void Update()
    {
        //Debug.Log(ok + "   " + dialogDone + "  " + askingQuestion);
        if (ok && dialogDone)
        {
            ok = false;
            if (doneSpeaking)
            {
                OnSpeak.instance.onSpeak = false;
                GameManager.instance.charaSpeaking.gameObject.GetComponent<Interact>().UpdateDialogRender();
                dialogue.enabled = false;
                speakerName.enabled = false;
            }
                
            //dialogue.text = loca.GetLocalizedString();
            if (OnSpeak.instance.onSpeak && !barman.question[barman.step].questionB)
            {
                //loca.SetReference(loca.TableReference, "intro Barman 2");
                //textN++;
                //dialogue.text = "";
                barman.step++;
                barman.eventOn[barman.step].Invoke();
                if (barman.uSpeak[barman.step] && !doneSpeaking)
                {
                    speakerName.text = "Bobby";
                    GameManager.instance.charaUI.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
                }
                else 
                {
                    speakerName.text = charaName;
                    GameManager.instance.charaUI.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                } 
                if (!flagChoice || (flagChoice && barman.choice == 0))
                {
                    if (barman.dialogue[barman.step].GetLocalizedString() == "stop") 
                    {
                        no();
                        return;
                    } 
                    else loca.SetReference(loca.TableReference, barman.dialogue[barman.step].TableEntryReference);
                    StartCoroutine(StartSpeak());
                } else if (flagChoice && barman.choice != 0)
                {
                    if (barman.choice == 1) 
                    {
                        if (barman.dialogueChoice1[barman.step].GetLocalizedString() == "stop")
                        {
                            no();
                            return;
                        }
                        else loca.SetReference(loca.TableReference, barman.dialogueChoice1[barman.step].TableEntryReference);
                    } 
                    else if (barman.choice == 2)
                    {
                        if (barman.dialogueChoice2[barman.step].GetLocalizedString() == "stop")
                        {
                            no();
                            return;
                        }
                        else loca.SetReference(loca.TableReference, barman.dialogueChoice2[barman.step].TableEntryReference);
                    }
                    StartCoroutine(StartSpeak());
                }

            }
        }
        /*foreach (Touch touch in Input.touches)
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
        }*/
    }

    void no()
    {
        barman.phase++;
        if (barman.phase < barman.startPhase.Length)
        {
            barman.step = barman.startPhase[barman.phase];
            if (flagChoice && barman.choice != 0)
            {
                if (barman.choice == 1) loca.SetReference(loca.TableReference, barman.dialogueChoice1[barman.step].TableEntryReference);
                else if (barman.choice == 2) loca.SetReference(loca.TableReference, barman.dialogueChoice2[barman.step].TableEntryReference);
            }
            else
                loca.SetReference(loca.TableReference, barman.dialogue[barman.step].TableEntryReference);
        }
        else doneSpeaking = true;
        OnSpeak.instance.onSpeak = false;
        GameManager.instance.charaSpeaking.gameObject.GetComponent<Interact>().UpdateDialogRender();
        dialogue.enabled = false;
        speakerName.enabled = false;
        //gameObject.SetActive(false);
        //last = false;
    }
    public void ResetDoneTalk()
    {
        doneSpeaking = false;
        barman.step = 0;
        barman.phase = 0;
        barman.choice = 0;
    }

    public void Advance()
    {
        //Debug.Log("___ADVANCE___");
        if (doneSpeaking)
        {
            OnSpeak.instance.onSpeak = false;
            GameManager.instance.charaSpeaking.gameObject.GetComponent<Interact>().UpdateDialogRender();
            dialogue.enabled = false;
            speakerName.enabled = false;
        }

        if (!askingQuestion)
            ok = true;
    }

    public void StartDialogue()
    {
        StartCoroutine(StartSpeak());
    }

    public IEnumerator StartSpeak()
    {
        whait = true;
        dialogDone = false;
        dialogue.enabled = true;
        dialogue.text = "";
        bool stop = false;
        bool speakOnGate = false;
        string speak = "";
        if (doneSpeaking) speak = barman.doneSpeakingTxt;
        else speak = loca.GetLocalizedString();//phases[phaseN]._text[textN].dialogue;
        string gateOpen = "";
        string gateClose = "";


        for (int i = 0; i < speak.Length; i++)
        {
            if (speak[i] == '<')
                stop = true;

            if (ok)
            {
                ok = false;
                dialogue.text = speak;
                break;
            }


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
        dialogDone = true;
        dialogue.text = speak;
        whait = false;

        spawnBooton = true;
        yield return new WaitForSeconds(0.1f);
        spawnBooton = false;

    }

    public void spawnDialogue()
    {
        if(!OnSpeak.instance.onSpeak /*&& OnSpeak.instance.idSpeaker != gameObject.GetInstanceID())*/)
        {
            if (barman.uSpeak[barman.step] && !doneSpeaking)
            {
                speakerName.text = "Bobby";
                GameManager.instance.charaUI.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
            else
            {
                speakerName.text = charaName;
                GameManager.instance.charaUI.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            speakerName.enabled = true;
            if (barman.uSpeak[barman.step] && !doneSpeaking) speakerName.text = "Bobby";
            else speakerName.text = charaName;
            OnSpeak.instance.onSpeak = true;
            OnSpeak.instance.idSpeaker = gameObject.GetInstanceID();

            //gameObject.SetActive(true);
            //dialogue = gameObject.GetComponentInChildren<Text>();
            if (!doneSpeaking)
                loca.SetReference(loca.TableReference, barman.dialogue[barman.step].TableEntryReference);
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

