using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class VillageSpecs : MonoBehaviour
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

    public LocalizedString[] dialogueC;
    public LocalizedString[] dialogueChoice1C;
    public LocalizedString[] dialogueChoice2C;
    public questionStruct[] questionC;
    public bool[] uSpeakC;


    public BarMan b;

    void Start()
    {
        if (CharaFlags.indianChiefConfiance)
        {
            b.dialogue = dialogueC;
            b.dialogueChoice1 = dialogueChoice1C;
            b.dialogueChoice2 = dialogueChoice2C;
            b.question = questionC;
            b.uSpeak = uSpeakC;
        }
        else
        {
            b.dialogue = dialogue;
            b.dialogueChoice1 = dialogueChoice1;
            b.dialogueChoice2 = dialogueChoice2;
            b.question = question;
            b.uSpeak = uSpeak;
        }
    }

    public void ResetStats()
    {
        if(CharaFlags.indianChiefConfiance)
        {
            b.dialogue = dialogueC;
            b.dialogueChoice1 = dialogueChoice1C;
            b.dialogueChoice2 = dialogueChoice2C;
            b.question = questionC;
            b.uSpeak = uSpeakC;
        }
        else
        {
            b.dialogue = dialogue;
            b.dialogueChoice1 = dialogueChoice1;
            b.dialogueChoice2 = dialogueChoice2;
            b.question = question;
            b.uSpeak = uSpeak;
        }
    }
}
