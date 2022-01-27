using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarMan : MonoBehaviour
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

    private void Update()
    {
        if(question[step] && perso.spawnBooton)
        {
            perso.reponse[0].SetActive(true);
            perso.reponse[1].SetActive(true);
        }
        if (!OnSpeak.instance.onSpeak)
        {
            step = startPhase[phase];
            question[5] = true;
            if (phase == 2)
                phase--;
        }

    }

    public void Choix1()
    {
        perso.loca.SetReference(perso.loca.TableReference, "Dialogue Barman R�ponse 1");
        perso.StartCoroutine(perso.StartSpeak());

        perso.reponse[0].SetActive(false);
        perso.reponse[1].SetActive(false);
        //step = startPhase[phase];
        question[5] = false;
        phase = 2;
    }

    public void Choix2()
    {
        perso.loca.SetReference(perso.loca.TableReference, "Dialogue Barman R�ponse 2");
        perso.StartCoroutine(perso.StartSpeak());

        perso.reponse[0].SetActive(false);
        perso.reponse[1].SetActive(false);
        //step = startPhase[phase];
        question[5] = false;
        phase = 2;
    }


}
