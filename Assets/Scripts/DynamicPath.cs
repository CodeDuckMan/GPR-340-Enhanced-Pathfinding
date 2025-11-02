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
    public World world; // get this somehow
    Path pathf;

    public Button pathingButton;
    public TMP_InputField startInput;
    public TMP_InputField goalInput;



    // Start is called before the first frame update
    void Start()
    {
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
        Debug.Log("Beans");
        end = ConvertInput.IntakeCoordinates(userText);
    }

    void readStartInput(string userText) 
    {
        Debug.Log("Memes");
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
            // path = pathf.FindPath(start, end, world);
        }
    }
}
