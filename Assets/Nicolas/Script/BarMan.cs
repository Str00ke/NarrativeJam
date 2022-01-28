using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.UI;

[System.Serializable]
public struct questionStruct
{
    public bool questionB;
    public LocalizedString choice1;
    public LocalizedString choice2;
    public LocalizedString answer1;
    public LocalizedString answer2;
}
public class BarMan : MonoBehaviour
{
    public DialogeManager perso;
    [Header("dialogue")]
    public LocalizedString[] dialogue;
    public LocalizedString[] dialogueChoice1;
    public LocalizedString[] dialogueChoice2;
    public questionStruct[] question;
    public bool[] uSpeak; //c'est caca de faire ca
    public UnityEvent[] eventOn; //c'est caca de faire ca
    public int step = 0;
    [Header("phase")]
    public int[] startPhase;
    public int phase = 0;
    public int choice; 
    
    public string doneSpeakingTxt = "...";
    // Start is called before the first frame update

    private void Update()
    {
        if (perso.doneSpeaking) return;
        if(question[step].questionB && perso.spawnBooton)
        {
            perso.reponse[0].SetActive(true);
            perso.reponse[1].SetActive(true);
            perso.reponse[0].GetComponentInChildren<Text>().text = question[step].choice1.GetLocalizedString();
            perso.reponse[1].GetComponentInChildren<Text>().text = question[step].choice2.GetLocalizedString();
            perso.askingQuestion = true;
        }
        if (!OnSpeak.instance.onSpeak && !perso.doneSpeaking)
        {
            //step = startPhase[phase];
            //question[step].questionB = true;
            if (phase == 2)
                phase--;
        }

    }

    public void Choix1()
    {
        perso.askingQuestion = false;
        choice = 1;
        perso.loca.SetReference(perso.loca.TableReference, question[step].answer1.TableEntryReference);
        perso.StartCoroutine(perso.StartSpeak());
        perso.reponse[0].SetActive(false);
        perso.reponse[1].SetActive(false);
        //step = startPhase[phase];
        question[step].questionB = false;
        phase = 2;
    }

    public void Choix2()
    {
        perso.askingQuestion = false;
        choice = 2;
        perso.loca.SetReference(perso.loca.TableReference, question[step].answer2.TableEntryReference);
        perso.StartCoroutine(perso.StartSpeak());

        perso.reponse[0].SetActive(false);
        perso.reponse[1].SetActive(false);
        //step = startPhase[phase];
        question[step].questionB = false;
        phase = 2;
    }


}
