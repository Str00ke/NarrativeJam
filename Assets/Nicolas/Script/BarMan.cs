using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarMan : PersoManager
{

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
        perso.loca.SetReference(perso.loca.TableReference, "Dialogue Barman Réponse 1");
        perso.StartCoroutine(perso.StartSpeak());

        perso.reponse[0].SetActive(false);
        perso.reponse[1].SetActive(false);
        //step = startPhase[phase];
        question[5] = false;
        phase = 2;
    }

    public void Choix2()
    {
        perso.loca.SetReference(perso.loca.TableReference, "Dialogue Barman Réponse 2");
        perso.StartCoroutine(perso.StartSpeak());

        perso.reponse[0].SetActive(false);
        perso.reponse[1].SetActive(false);
        //step = startPhase[phase];
        question[5] = false;
        phase = 2;
    }


}
