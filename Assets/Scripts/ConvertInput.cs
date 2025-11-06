using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class ConvertInput
{
    public static Point2D IntakeCoordinates(string input) 
    { 
        string[] parts = input.Split(',');
        if (parts.Length == 2 && int.TryParse(parts[0], out int xPos) && int.TryParse(parts[1], out int yPos))
        {
            Point2D startPoint = new Point2D(xPos, yPos);
            Debug.Log("Inputted: " + startPoint.x.ToString() + "'" + startPoint.y.ToString());
            return startPoint;
        }
        else 
        {
            Debug.Log("Error with input: please use x,y format.");
            return new Point2D();
        }
        
    }
}
