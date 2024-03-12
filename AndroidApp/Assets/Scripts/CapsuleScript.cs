using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleScript : MonoBehaviour, IInteractable
{
    bool IsSelected = false;
    float dragDistance = 0;
    Vector3 StartingScale;
    Quaternion StartingRotation;

    public void processTap()
    {
        GetComponent<Renderer>().material.color = Color.blue;
        IsSelected = true;
    }

    public void processDrag(Vector3 newPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(newPos);
        transform.position = ray.GetPoint(dragDistance);
    }

    public void unSelect()
    {
        GetComponent<Renderer>().material.color = Color.white;
        IsSelected = false;
    }

    public void distanceOnTap(float startDistanceOnSelect)
    {
        dragDistance = startDistanceOnSelect;
    }

    public bool SelectedCheck()
    {
        return IsSelected;
    }

    public Vector3 GetStartScale()
    {
        StartingScale = transform.localScale;

        return StartingScale;
    }

    public void ScaleAmount(float ScaleValue)
    {
        transform.localScale = StartingScale / ScaleValue;
    }

    public void RotateAmount(float RotationValue)
    {
       
    }

    public Quaternion GetStartRotation()
    {
        StartingRotation = transform.rotation;

        return StartingRotation;
    }
}
