using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class DynamicPath : MonoBehaviour
{
    List<Point2D> path = new List<Point2D>();
    Point2D start;
    Point2D end;

    World world; 
    Path pathMaker = new Path();

    public Button pathingButton;
    public TMP_InputField startInput;
    public TMP_InputField goalInput;



    // Start is called before the first frame update
    void Start()
    {
        world = GameObject.Find("GridMaker").GetComponent<World>();
        pathingButton.onClick.AddListener(() => CreatePath());
        startInput.onSubmit.AddListener(ReadStartInput);
        goalInput.onSubmit.AddListener(ReadGoalInput);
    }

    void ReadGoalInput(string userText) 
    {
        end = ConvertInput.IntakeCoordinates(userText);
        if (!world.IsValidPosition(ref end))
        {
            Debug.Log("Warning: coordinates are not valid");
        }
    }

    void ReadStartInput(string userText) 
    {
        start = ConvertInput.IntakeCoordinates(userText);
        if (!world.IsValidPosition(ref start))
        {
            Debug.Log("Warning: coordinates are not valid");
        }
    }

    public List<Point2D> CreatePath() 
    {
        if (!world.IsValidPosition(ref start) || !world.IsValidPosition(ref end))
        {
            Debug.Log("Error: one or both coordinates are invalid.");
            return  null;
        }
        else if (start == end) 
        {
            Debug.Log("Error: coordinates are the same");
            return null;
        }
        else
        {
            Debug.Log("Coordinates Acceptable with Start: " + $"Point2D: {start.x}, {start.y}" + " and goal: " + $"Point2D: {end.x}, {end.y}");
            path = pathMaker.FindPath(start, end, world);
            foreach (var point in path) 
            {
                Debug.Log($"Point2D: {point.x}, {point.y}");
            }
            Debug.Log("Path Finished");
        }     
        return path;
    }
}
