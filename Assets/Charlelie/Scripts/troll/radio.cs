using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radio : MonoBehaviour
{
    public GameObject sound;
    void Start()
    {
        if (GetComponent<PickableItem>())
            GetComponent<PickableItem>()._pickedDelegate += Sound;
        //GetComponent<PickableItem>()._invDelegate += Sound;
        if (GetComponent<Draggable>())
            GetComponent<Draggable>()._thrashedDelegate += Thrashed;
    }

    void Sound()
    {
        Instantiate(sound);
    }

    void Thrashed()
    {
        Destroy(GameObject.Find("SoundTroll(Clone)"));
        Destroy(gameObject);
    }
}
