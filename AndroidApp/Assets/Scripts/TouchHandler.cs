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
    private GestureAction MyActOn;
    private MultiTouchManager MyHandler;
    internal Vector2 touchPosition;
    internal bool IsSlave = false;
    internal TouchHandler OtherHandler;


    private void Start()
    {
    }
    public void HandleTouch(Touch t)
    {
        touchPosition = t.position;
        switch (t.phase)
        {
            case TouchPhase.Began:
                hasMoved = false;
                touchTimer = 0f;
                MyActOn.InitialTouch(t.position, t.fingerId);
                MyActOn.initialTouch = false;
                MyActOn.MyFingerStartPos(t.position);
                break;
            case TouchPhase.Moved:
                hasMoved = true;
                MyActOn.drag(t.position, t.fingerId);
                MyActOn.FingerScale(t.position);
                break;
            case TouchPhase.Stationary:
                touchTimer += Time.deltaTime;
                break;
            case TouchPhase.Ended:
                if (touchTimer < MaxTapTime && !hasMoved)
                {
                    MyActOn.tapAt(t.position);
                }
                MyActOn.DragBeganOnTarget = false;
                MyActOn.initialTouch = true;

                Destroy(gameObject);
                MyHandler.RemoveTouch(t.fingerId);
                break;
        }
    }

    internal void SelfAware(MultiTouchManager Mh)
    {
        MyHandler = Mh;
        MyActOn = GetComponent<GestureAction>();
    }

    internal GestureAction MyActionScript()
    {
        return MyActOn;
    }
}
