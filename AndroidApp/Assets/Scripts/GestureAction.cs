using System;
using System.Collections;
using System.Collections.Generic;
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
                objectHit.processTap();
                if (selectedObject != null)
                {
                    selectedObject.unSelect();
                }
                selectedObject = objectHit;
            }
            else
            {
                if (selectedObject != null)
                {
                    selectedObject.unSelect();
                    selectedObject = null;
                }
            }
        }
        else
        {
            if (selectedObject != null)
            {
                selectedObject.unSelect();
                selectedObject = null;
            }
        }
    }

    internal void drag(Vector2 screenPoint)
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

        if (touchedObject != null && DragBeganOnTarget)
        {
            touchedObject.processDrag(screenPoint);
        }
    }

    internal void InitialTouch(Vector2 position)
    {
        if (initialTouch)
        {
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
}
