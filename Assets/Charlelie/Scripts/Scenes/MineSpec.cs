using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class MineSpec : MonoBehaviour
{

    public LocalizedString[] dialogue;
    public LocalizedString[] dialogueChoice1;
    public LocalizedString[] dialogueChoice2;
    public questionStruct[] question;
    public bool[] uSpeak;

    [Space]
    [Space]
    [Space]
    [Space]

    public LocalizedString[] dialogueB;
    public LocalizedString[] dialogueChoice1B;
    public LocalizedString[] dialogueChoice2B;
    public questionStruct[] questionB;
    public bool[] uSpeakB;


    BarMan b;
    
    void Start()
    {
        b = FindObjectOfType<BarMan>();
        if (CharaFlags.minerGotBeer)
        {
            b.dialogue = dialogueB;
            b.dialogueChoice1 = dialogueChoice1B;
            b.dialogueChoice2 = dialogueChoice2B;
            b.question = questionB;
            b.uSpeak = uSpeakB;
        }
        else
        {
            b.dialogue = dialogue;
            b.dialogueChoice1 = dialogueChoice1;
            b.dialogueChoice2 = dialogueChoice2;
            b.question = question;
            b.uSpeak = uSpeak;
        }
        CharaFlags.wentToMine = true;
    }


    public void ResetStats()
    {
        b = FindObjectOfType<BarMan>();
        if (CharaFlags.minerGotBeer)
        {
            b.dialogue = dialogueB;
            b.dialogueChoice1 = dialogueChoice1B;
            b.dialogueChoice2 = dialogueChoice2B;
            b.question = questionB;
            b.uSpeak = uSpeakB;
            FindObjectOfType<BarMan>().dialogue[0] = dialogueB[0];
        }
        else
        {
            b.dialogue = dialogue;
            b.dialogueChoice1 = dialogueChoice1;
            b.dialogueChoice2 = dialogueChoice2;
            b.question = question;
            b.uSpeak = uSpeak;
        }
        CharaFlags.wentToMine = true;
    }
}
