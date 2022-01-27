using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : PersoManager
{
    // Update is called once per frame
    void Update()
    {
        if (question[step] && perso.spawnBooton)
        {
            perso.reponse[0].SetActive(true);
            perso.reponse[1].SetActive(true);
        }
        if (!OnSpeak.instance.onSpeak)
        {
            step = startPhase[phase];
            question[0] = true;
        }
    }

    public void Choix1()
    {

        perso.reponse[0].SetActive(false);
        perso.reponse[1].SetActive(false);
        //step++;
        question[0] = false;
        perso.spawnBooton = false;
        perso.spawnBooton = false;
        OnSpeak.instance.onSpeak = false;
        perso.gameObject.SetActive(false);
    }

    public void Choix2()
    {
        if(InventoryManager.instance.objPrefab.name == "beer")
            phase = 1;
        question[0] = false;
        perso.reponse[0].SetActive(false);
        perso.reponse[1].SetActive(false);
        //step++;
        perso.spawnBooton = false;
        OnSpeak.instance.onSpeak = false;
        perso.gameObject.SetActive(false);
    }
}
