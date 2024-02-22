using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour, IInteractable
{
    bool IsSelected = false;
    float dragDistance = 0;
    Vector3 StartingScale;

    public void processTap()
    {
        GetComponent<Renderer>().material.color = Color.yellow;
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

    public void ScaleAmount(float ScaleValue, Vector3 selectObjStartScale)
    {
        transform.localScale = StartingScale/ScaleValue;
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
}