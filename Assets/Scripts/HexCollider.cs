using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HexCollider : MonoBehaviour, IPointerClickHandler
{
    Point2D thePoint;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        thePoint = ConvertInput.IntakeCoordinates(gameObject.name);
        Debug.Log($"Point2D: {thePoint.x}, {thePoint.y}");
    }

    void OnMouseDown()
    {
        Debug.Log("Clicked");
    }
}
