using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

public class GestureAction : MonoBehaviour
{
    private float zPos = 10f;
    private float hitDistance;
    private Vector3 offset;
    IInteractable selectedObject;
    IInteractable touchedObject;
    internal bool DragBeganOnTarget = false;
    internal bool initialTouch = true;
    private int MyTouchID;
    internal Vector2 StartPos;
    internal Vector2 ConstPos;
    private MultiTouchManager MultiTouchmanager;  

    private void Start()
    {
       MultiTouchmanager = FindAnyObjectByType<MultiTouchManager>();
    }

    internal void tapAt(Vector2 position)
    {
        print("Tap");
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo))
        {
            IInteractable objectHit = hitInfo.collider.gameObject.GetComponent<IInteractable>();
            if (objectHit != null)
            {
                hitDistance = Vector3.Distance(hitInfo.transform.position, Camera.main.transform.position);
                objectHit.distanceOnTap(hitDistance);
               
                selectedObject = objectHit;

                if (selectedObject != null && selectedObject.SelectedCheck())
                {
                    selectedObject.unSelect();
                    selectedObject = null;
                }

                else { MultiTouchmanager.NewSelectedObject(objectHit); }
               


            }
            else
            {
                if (selectedObject != null && selectedObject.SelectedCheck())
                {
                    selectedObject.unSelect();
                    selectedObject = null;
                }
            }
        }
        else
        {
            if (selectedObject != null && selectedObject.SelectedCheck())
            {
                selectedObject.unSelect();
                selectedObject = null;
            }
        }
    }

    internal void drag(Vector2 screenPoint, int fingerId)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            IInteractable objectHit = hitInfo.collider.gameObject.GetComponent<IInteractable>();
            if (objectHit == touchedObject)
            {
                DragBeganOnTarget = true;
            }
        }

        if (touchedObject != null && DragBeganOnTarget && MyTouchID.Equals(fingerId))
        {
            touchedObject.processDrag(screenPoint);
        }
    }

    internal void InitialTouch(Vector2 position, int fingerId)
    {
        if (initialTouch)
        {
            print("Initial Touch for " + fingerId.ToString());
            MyTouchID = fingerId;
            Ray ray = Camera.main.ScreenPointToRay(position);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                IInteractable objectHit = hitInfo.collider.gameObject.GetComponent<IInteractable>();
                if (objectHit != null)
                {
                    hitDistance = Vector3.Distance(hitInfo.transform.position, Camera.main.transform.position);
                    objectHit.distanceOnTap(hitDistance);
                    touchedObject = objectHit;
                }
            }
        }
    }


    internal void FingerScale(Vector2 position)
    {
        StartPos = position;
    }

    internal void MyFingerStartPos(Vector2 position)
    {  
        ConstPos = position;
    }

    internal void ScaleObject(float ScaleValue)
    {
        if(MultiTouchmanager.GetSelectedObj() != null)
        { 
            MultiTouchmanager.SelectedObject.ScaleAmount(ScaleValue,MultiTouchmanager.SelectObjStartScale); 
        }
    }
}
