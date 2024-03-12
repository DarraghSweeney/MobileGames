using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour, IInteractable
{
    bool IsSelected = false;
    float dragDistance = 0;
    Vector3 StartingScale;
    Quaternion StartingRotation;

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
       transform.localScale = StartingScale/ScaleValue;
    }

    public void RotateAmount(float RotationValue)
    {
        Quaternion RotationAmount = Quaternion.AngleAxis(RotationValue,Vector3.forward);

        transform.localRotation = StartingRotation * RotationAmount;
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

    public Quaternion GetStartRotation()
    {
       StartingRotation = transform.rotation;

        return StartingRotation;
    }
}
