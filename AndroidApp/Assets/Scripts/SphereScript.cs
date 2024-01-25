using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour, IInteractable
{
    float dragDistance = 0;
    public void processTap()
    {
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void processDrag(Vector3 newPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(newPos);
        transform.position = ray.GetPoint(dragDistance);
    }

    public void unSelect()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    public void distanceOnTap(float startDistanceOnSelect)
    {
        dragDistance = startDistanceOnSelect;
    }
}