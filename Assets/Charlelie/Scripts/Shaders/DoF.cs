using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Linq;

public class DoF : MonoBehaviour
{
    DepthOfField dof;
    Volume volume;
    public AnimationCurve anim;
    public float speed, min, max;
    bool isBlur = false;
    bool isUpdate = false;
    void Start()
    {

        /*var componentTypes = System.AppDomain.CurrentDomain.GetAssemblies()
     .SelectMany(assembly => assembly.GetTypes())
     .Where(type => type.IsClass && type.IsSubclassOf(typeof(Component)))
 ;

        foreach (var type in componentTypes)
        {
            Debug.Log(type.FullName);
        }*/

        volume = GetComponent<Volume>();
        DepthOfField tmp;
        if (volume.profile.TryGet<DepthOfField>(out tmp))
        {
            dof = tmp;
        }
        dof.focalLength.value = min;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            if (isUpdate) return;
            if (isBlur) UnBlur();
            else Blur();
            isBlur = !isBlur;
        }
    }



    public void Blur()
    {
        Debug.Log("BLUR");
        StartCoroutine(apply(min, max));
    }

    public void UnBlur()
    {
        Debug.Log("UNBLUR");
        StartCoroutine(apply(max, min));
    }

    IEnumerator apply(float s, float e)
    {
        isUpdate = true;
        float t = 0;
        while (t < 1)
        {
            dof.focalLength.value = Mathf.Lerp(s, e, t);
            t += Time.deltaTime * speed * anim.Evaluate(t);
            yield return null;
        }
        isUpdate = false;
        yield return null;
    }
}
