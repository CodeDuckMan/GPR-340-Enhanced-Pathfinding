using System.Collections;
using System.Collections.Generic;
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
        pathingButton.onClick.AddListener(createPath);
        startInput.onSubmit.AddListener(readStartInput);
        goalInput.onSubmit.AddListener(readGoalInput);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void readGoalInput(string userText) 
    {
        end = ConvertInput.IntakeCoordinates(userText);
    }

    void readStartInput(string userText) 
    {
        start = ConvertInput.IntakeCoordinates(userText);
    }

    void createPath() 
    {
        if (start == new Point2D(0, 0) || end == new Point2D(0, 0))
        {
            Debug.Log("Error: unanitialized coordinates or both just (0,0)");
            return;
        }
        else if (start == end) 
        {
            Debug.Log("Error: coordinates are the same");
            return;
        }
        else
        {
            Debug.Log("Coordiantes Acceptable with Start: " + start.x.ToString() + "'" + start.y.ToString() + " and goal: " + end.x.ToString() + "'" + end.y.ToString());
            path = pathMaker.FindPath(start, end, world);
            foreach (var point in path) 
            {
                Debug.Log("The next point in the path is: " + point.ToString());
            }
        }      
    }
}
