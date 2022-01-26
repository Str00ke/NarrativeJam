using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Linq;

public class ColorEffect : MonoBehaviour
{
    ColorAdjustments colAd;
    Volume volume;
    public AnimationCurve anim;
    public float speed, contrastMin, contrastMax;
    public Color baseCol, effectCol;
    bool isBlur = false;
    bool isUpdate = false;
    void Start()
    {

        volume = GetComponent<Volume>();
        ColorAdjustments tmp;
        if (volume.profile.TryGet<ColorAdjustments>(out tmp))
        {
            colAd = tmp;
        }
        colAd.contrast.value = contrastMin;
        colAd.colorFilter.value = baseCol;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isUpdate) return;
            if (isBlur) UnCol();
            else Col();
            isBlur = !isBlur;
        }
    }

    public void UpdateColor()
    {
        if (isUpdate) return;
        if (isBlur) UnCol();
        else Col();
        isBlur = !isBlur;
    }


    public void Col()
    {
        StartCoroutine(apply(contrastMin, contrastMax, baseCol, effectCol));
    }

    public void UnCol()
    {
        StartCoroutine(apply(contrastMax, contrastMin, effectCol, baseCol));
    }

    IEnumerator apply(float s, float e, Color cs, Color ce)
    {
        isUpdate = true;
        float t = 0;
        while (t < 1)
        {
            colAd.contrast.value = Mathf.Lerp(s, e, t);
            colAd.colorFilter.value = Color.Lerp(cs, ce, t);
            t += Time.deltaTime * speed * anim.Evaluate(t);
            yield return null;
        }
        isUpdate = false;
        yield return null;
    }
}
