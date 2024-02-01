using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MultiTouchHandler : MonoBehaviour
{

    internal Dictionary<int, TouchManager> touchManagers = new Dictionary<int, TouchManager>();

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
                    TouchManager touchManager = newTouchObject.AddComponent<TouchManager>();
                    touchManager.SelfAware(this);

                    touchManagers.Add(t.fingerId, touchManager);
                }

                touchManagers[t.fingerId].HandleTouch(t);
            }
        }
    }

    internal void RemoveTouch(int fingerId)
    {
        touchManagers.Remove(fingerId);
    }
}
