using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    static bool AABBCol(Rect box1, Rect box2)
    {
        if ((box2.x >= box1.x + box1.width)      // trop à droite
    || (box2.x + box2.width <= box1.x) // trop à gauche
    || (box2.y >= box1.y + box1.height) // trop en bas
    || (box2.y + box2.height <= box1.y))  // trop en haut
            return false;
        else
            return true;
    }
}
