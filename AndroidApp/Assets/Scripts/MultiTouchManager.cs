using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MultiTouchManager : MonoBehaviour
{
    internal Dictionary<int, TouchHandler> touchManagers = new Dictionary<int, TouchHandler>();

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch t in Input.touches)
            {
                if(t.phase == TouchPhase.Began)
                {
                    GameObject newTouchObject = new GameObject("NewTouchObject" + t.fingerId.ToString());
                    TouchHandler touchManager = newTouchObject.AddComponent<TouchHandler>();
                    GestureAction action = newTouchObject.AddComponent<GestureAction>();
                    touchManager.SelfAware(this);

                    touchManagers.Add(t.fingerId, touchManager);
                }

                touchManagers[t.fingerId].HandleTouch(t);
            }
        }

        if(touchManagers.Count == 2)
        {
            FingerDistance(touchManagers[0], touchManagers[1]);

            GestureAction ThisAction = touchManagers[0].MyActionScript();
            GestureAction ThatAction = touchManagers[1].MyActionScript();

            ThisAction.ScaleObject(Vector2.Distance(ThisAction.ConstPos,ThatAction.ConstPos)/Vector2.Distance(ThisAction.StartPos, ThatAction.StartPos));
            
        }
    }

    internal double FingerDistance(TouchHandler FirstFinger, TouchHandler SecondFinger)
    {
        float Distance = Vector2.Distance(FirstFinger.touchPosition, SecondFinger.touchPosition);
        print(Distance);

        return Distance;
    }

    internal void RemoveTouch(int fingerId)
    {
        touchManagers.Remove(fingerId);
    }
}
