using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour, IInteractable
{
    bool IsSelected = false;
    float dragDistance = 0;
    Transform StartingScale;

    void Start()
    {
        StartingScale.transform.localScale = transform.localScale;
    }

    public void processTap()
    {
        GetComponent<Renderer>().material.color = Color.red;
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

    public void ScaleAmount(float ScaleValue)
    {
       transform.localScale = StartingScale.transform.localScale * ScaleValue;
    }

    public bool SelectedCheck()
    {
        return IsSelected;
    }
}
