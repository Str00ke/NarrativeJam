using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duel : MonoBehaviour
{
    public GameObject gun, enemy;
    float currRot;
    public float rotMin, rotMax;
    public float speed;
    int way = 1;
    Quaternion rot;
    void Start()
    {
        rot = gun.transform.rotation;
    }


    void Update()
    {
        currRot += Time.deltaTime * way * speed;
        gun.transform.eulerAngles = new Vector3(0, 0, currRot);
        if (currRot * way > rotMax | currRot * way < rotMin)
            way *= -1;
        Debug.DrawRay(gun.transform.position, gun.transform.right * 1000, Color.red);

        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(gun.transform.position, gun.transform.right, 1000);
            if (hit)
                Debug.Log("HIT");
            else Debug.Log("NOT HIT");
        }
    }

}
