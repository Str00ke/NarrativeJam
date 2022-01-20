using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    Camera cam;

    #region pinchVars
    float pinchPrevDist = 0;
    int way = 0; //1 = Zoom out / 0 = Zoom in
    float timeSinceStartPinch = 0;
    bool isPinching = false;
    bool isZooming = false;
    int zoomOutMax, zoomInMax;
    int currZoomVal = 0;
    Vector2 vec;



    public AnimationCurve zoomEase;
    public float maxHoldTimeToZoom = 0.75f;
    public int maxZoomDezoom;
    public float zoomSize;
    public float camZoomSpeed;
    #endregion

    public float zoomMoveSensibility = 0.001f;
    public bool zoomMoveInverse = true;
    int zoomMoveInverseVal;
    Vector2 zoomMoveVel;
    Vector2 prevZoomMoveVel;

    float distance;
    float speed, prevSpeed;
    Vector2 pos, prevPos;
    float acceleration = -2;
    Vector2 velocity, prevVelocity;
    float deltaVel;
    Vector2 frameVel = Vector2.zero;
    public float camFriction = 0.02f;

    Vector2 borders;

    void Start()
    {
        cam = Camera.main;
        zoomOutMax = -maxZoomDezoom;
        zoomInMax = maxZoomDezoom;
        if (zoomMoveInverse) zoomMoveInverseVal = -1;
        else zoomMoveInverseVal = 1;
        float vertExtents = cam.orthographicSize;
        float horzExtents = vertExtents * Screen.width / Screen.height;
        borders = new Vector2(horzExtents, vertExtents);
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
            }

            if (dist > pinchPrevDist)
            {
                way = 0;
                vec = new Vector2((cam.ScreenToWorldPoint(Input.GetTouch(0).position).x + cam.ScreenToWorldPoint(Input.GetTouch(1).position).x) / 2, (cam.ScreenToWorldPoint(Input.GetTouch(0).position).y + cam.ScreenToWorldPoint(Input.GetTouch(1).position).y) / 2);
            }
            else if (dist < pinchPrevDist)
            {
                way = 1;
                vec = new Vector2((cam.ScreenToWorldPoint(Input.GetTouch(0).position).x + cam.ScreenToWorldPoint(Input.GetTouch(1).position).x) / 2, (cam.ScreenToWorldPoint(Input.GetTouch(0).position).y + cam.ScreenToWorldPoint(Input.GetTouch(1).position).y) / 2);
            }
            pinchPrevDist = dist;

            timeSinceStartPinch += Time.deltaTime;
        }

        if (Input.touchCount != 2 && isPinching)
        {
            isPinching = false;
            pinchPrevDist = 0;
            if (timeSinceStartPinch < maxHoldTimeToZoom)
            {
                if (way == 1 && currZoomVal < zoomInMax) { currZoomVal++; ZoomCamera(1, vec); }
                else if (way == 0 && currZoomVal > zoomOutMax) { currZoomVal--; ZoomCamera(-1, vec); }
            }
            timeSinceStartPinch = 0;
        }

        #endregion

        #region zoomMove
        if (Input.touchCount == 1 && currZoomVal < 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 delta = Input.GetTouch(0).deltaPosition;
                bool good = true;
                //if camera touching border, zero-ing vel
                Vector2 camPoss = cam.transform.position + (((Vector3)delta * zoomMoveInverseVal) * zoomMoveSensibility);
                float vertExtentss = cam.orthographicSize;
                float horzExtentss = vertExtentss * Screen.width / Screen.height;

                if (camPoss.x + horzExtentss > borders.x) good = false;
                else if (camPoss.x - horzExtentss < -borders.x) good = false;
                else if (camPoss.y + vertExtentss > borders.y) good = false;
                else if (camPoss.y - vertExtentss < -borders.y) good = false;

                if (good) cam.transform.position += (((Vector3)delta * zoomMoveInverseVal) * zoomMoveSensibility);

                #region testPhysics
                //zoomMoveVel = ((Vector2)cam.transform.position - prevZoomMoveVel)/*.magnitude*/ / Time.deltaTime;
                //prevZoomMoveVel = transform.position;
                //distance = Mathf.Sqrt(Mathf.Pow((prevPos.x - pos.x), 2) + Mathf.Pow((prevPos.y - pos.y), 2));
                //pos = cam.transform.position;
                /*distance = Mathf.Sqrt(((pos.x - prevPos.x) * (pos.x - prevPos.x)) + ((pos.y - prevPos.y) * (pos.y - prevPos.y)));
                prevPos = pos;
                prevSpeed = speed;
                speed = distance / Time.deltaTime;
                prevVelocity = velocity;
                velocity = speed - prevSpeed;
                deltaVel = prevVelocity - velocity;*/
                //acceleration = deltaVel / Time.deltaTime;

                //Debug.Log(acceleration);

                //cam.transform.position= pos + velocity * Time.deltaTime + (acceleration * Mathf.Pow(Time.deltaTime, 2) / 2);
                #endregion
            }
        }
        velocity = ((Vector2)cam.transform.position - prevPos) / Time.deltaTime;
        frameVel = Vector2.Lerp(frameVel, velocity, 0.1f);
        frameVel -= velocity * camFriction;
        prevPos = cam.transform.position;
        float vertExtent = cam.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;
        //if camera touching border, zero-ing vel
        Vector2 camPos = cam.transform.position + ((Vector3)frameVel / 100);

        if (camPos.x + horzExtent > borders.x) frameVel = Vector2.zero;
        else if (camPos.x - horzExtent < -borders.x) frameVel = Vector2.zero;
        else if (camPos.y + vertExtent > borders.y) frameVel = Vector2.zero;
        else if (camPos.y - vertExtent < -borders.y) frameVel = Vector2.zero;

        if (frameVel != Vector2.zero && Input.touchCount == 0) cam.transform.position += ((Vector3)frameVel / 100);

        #endregion
        
    }

    #region zoom
    void ZoomCamera(int way, Vector2 pos)
    {
        StartCoroutine(ZoomCor(cam.orthographicSize, cam.orthographicSize + (zoomSize * way), pos));
    }

    IEnumerator ZoomCor(float start, float end, Vector2 pos)
    {
        isZooming = true;
        float t = 0;
        bool camToo = currZoomVal == 0 && cam.transform.position != new Vector3(0, 0, -10);
        bool camZoomMove = currZoomVal < 0;
        Vector3 camS = cam.transform.position;
        Vector3 camE = new Vector3(0, 0, -10);

        Vector3 _camS = cam.transform.position;
        Vector3 _camE = new Vector3(pos.x, pos.y, -10);
        while (t < 1)
        {
            cam.orthographicSize = Mathf.Lerp(start, end, t);
            if (camToo) cam.transform.position = Vector3.Lerp(camS, camE, t);
            else if (camZoomMove) cam.transform.position = Vector3.Lerp(_camS, _camE, t);
            t += Time.deltaTime * camZoomSpeed * zoomEase.Evaluate(t);
            yield return null;
        }

        isZooming = false;
        yield return null;
    }

    #endregion

}
