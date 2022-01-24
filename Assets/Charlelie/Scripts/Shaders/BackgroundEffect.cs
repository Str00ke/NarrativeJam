using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundEffect : MonoBehaviour
{
    public Material greyMat;
    public Material blurMat;
    SpriteRenderer sr;
    public Vector2 greyMinMax;
    public Vector2 blurMinMax;

    public float speed;

    bool isOn = false;
    bool isUpdating = false;
    public enum Shader
    {
        GREY,
        BLUR
    }

    public Shader shader;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (shader == Shader.BLUR) 
        {
            sr.material = blurMat;
            sr.material.SetFloat("_Strength", blurMinMax.x);
        }        
        else  
        {
            sr.material = greyMat;
            sr.material.SetFloat("_Strength", greyMinMax.x);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) UpdateShader();
    }

    public void UpdateShader()
    {
        if (isUpdating) return;
        isOn = !isOn;
        StartCoroutine(cor());
    }

    IEnumerator cor()
    {
        isUpdating = true;
        float t = 0;
        Vector2 vec = Vector2.zero;
        if (shader == Shader.BLUR) vec = blurMinMax;
        else vec = greyMinMax;
        if (!isOn)
        {
            float tmp = vec.y;
            vec.y = vec.x;
            vec.x = tmp;
        }
        while (t < 1)
        {
            sr.material.SetFloat("_Strength", Mathf.Lerp(vec.x, vec.y, t));
            t += Time.deltaTime * speed;
            yield return null;
        }
        isUpdating = false;
        yield return null;
    }
}
