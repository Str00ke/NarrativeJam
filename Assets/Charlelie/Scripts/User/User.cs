using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    float pinchPrevDist = 0;
    int way = 0; //1 = Zoom out / 0 = Zoom in
    float timeSinceStartPinch = 0;
    bool isPinching = false;
    bool isZooming = false;
    int zoomOutMax, zoomInMax;
    int currZoomVal = 0;
    Camera cam;

    public AnimationCurve zoomEase;
    public float maxHoldTimeToZoom = 0.75f;
    public int maxZoomDezoom;
    public float zoomSize;
    public float camZoomSpeed;
    void Start()
    {
        cam = Camera.main;
        zoomOutMax = -maxZoomDezoom;
        zoomInMax = maxZoomDezoom;
    }


    void Update()
    {
        #region pinch
        if (Input.touchCount == 2 && !isZooming)
        {

            float dist = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);

            if (!isPinching)
            {
                isPinching = true;
                pinchPrevDist = dist;
                Debug.Log("PINCHING");
            }

            if (dist > pinchPrevDist)
            {
                way = 0;
            }
            else if (dist < pinchPrevDist)
            {
                way = 1;
            }
            pinchPrevDist = dist;

            timeSinceStartPinch += Time.deltaTime;
        }

        if (Input.touchCount != 2 && isPinching)
        {
            //Debug.Log("NOT PINCHING");
            isPinching = false;
            pinchPrevDist = 0;
            if (timeSinceStartPinch < maxHoldTimeToZoom)
            {
                if (way == 1 && currZoomVal < zoomInMax) { ZoomCamera(1); currZoomVal++; }
                else if (way == 0 && currZoomVal > zoomOutMax) { ZoomCamera(-1); currZoomVal--; }
            }
            timeSinceStartPinch = 0;
        }

        #endregion
    }

    void ZoomCamera(int way)
    {
        StartCoroutine(ZoomCor(cam.orthographicSize, cam.orthographicSize + (zoomSize * way)));
    }

    IEnumerator ZoomCor(float start, float end)
    {
        isZooming = true;
        float t = 0;
        while (t < 1)
        {
            cam.orthographicSize = Mathf.Lerp(start, end, t);
            t += Time.deltaTime * camZoomSpeed * zoomEase.Evaluate(t);
            yield return null;
        }

        isZooming = false;
        yield return null;
    }

}
