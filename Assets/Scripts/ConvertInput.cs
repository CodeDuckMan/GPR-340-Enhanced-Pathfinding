using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConvertInput : MonoBehaviour
{
    public TMP_InputField inputField;

    public void IntakeCoordinates() 
    { 
        string input = inputField.text.Trim();
        string[] parts = input.Split(',');
        if (parts.Length == 2 && int.TryParse(parts[0], out int xPos) && int.TryParse(parts[1], out int yPos))
        {
            Point2D startPoint = new Point2D(xPos, yPos);
        }
        else 
        {
            inputField.text = "Error";
        }
    }
}
