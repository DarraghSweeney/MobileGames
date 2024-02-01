using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private float touchTimer = 0;
    private bool hasMoved = false;
    private float MaxTapTime = 1f;
    private GestureAction actOn;
    private MultiTouchHandler MyHandler;

    void Start()
    {
    }

    public void HandleTouch(Touch t)
    {
        switch (t.phase)
        {
            case TouchPhase.Began:
                hasMoved = false;
                touchTimer = 0f;
                actOn.InitialTouch(t.position);
                actOn.initialTouch = false;
                break;
            case TouchPhase.Moved:
                hasMoved = true;
                actOn.drag(t.position);
                break;
            case TouchPhase.Stationary:
                touchTimer += Time.deltaTime;
                break;
            case TouchPhase.Ended:
                if (touchTimer < MaxTapTime && !hasMoved)
                {
                    actOn.tapAt(t.position);
                }
                actOn.DragBeganOnTarget = false;
                actOn.initialTouch = true;

                Destroy(gameObject);
                MyHandler.RemoveTouch(t.fingerId);
                break;
        }
    }

    internal void SelfAware(MultiTouchHandler Mh)
    {
        MyHandler = Mh;
        actOn = FindObjectOfType<GestureAction>();
    }

 
}
