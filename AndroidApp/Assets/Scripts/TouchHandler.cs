using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class TouchHandler : MonoBehaviour
{
    private float touchTimer = 0;
    private bool hasMoved = false;
    private float MaxTapTime = 1f;
    private GestureAction actOn;
    private MultiTouchManager MyHandler;
    internal Vector2 touchPosition;
    internal bool IsSlave = false;
    internal TouchHandler OtherHandler;

    public void HandleTouch(Touch t)
    {
        touchPosition = t.position;
        switch (t.phase)
        {
            case TouchPhase.Began:
                hasMoved = false;
                touchTimer = 0f;
                actOn.InitialTouch(t.position, t.fingerId);
                actOn.initialTouch = false;
                break;
            case TouchPhase.Moved:
                hasMoved = true;
                actOn.drag(t.position, t.fingerId);
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

    internal void SelfAware(MultiTouchManager Mh)
    {
        MyHandler = Mh;
        actOn = GetComponent<GestureAction>();
    }
}
