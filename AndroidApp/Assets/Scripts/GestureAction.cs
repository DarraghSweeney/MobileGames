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
    internal bool DragBeganOnTarget = false;
    
    internal void tapAt(Vector2 position)
    {
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
            if (objectHit == selectedObject)
                {
                    DragBeganOnTarget = true;
                }
        }

        if(selectedObject != null && DragBeganOnTarget)
         {
            selectedObject.processDrag(screenPoint);
         }     
    }
}
