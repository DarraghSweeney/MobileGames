using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IInteractable
{
    void processTap();

    void processDrag(Vector3 position);
    void unSelect();
    void distanceOnTap(float hitDistance);
    void ScaleAmount(float ScaleValue);
    bool SelectedCheck();
}
