using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DynamicPath : MonoBehaviour
{
    List<Point2D> path = new List<Point2D>();
    public Point2D start;
    public Point2D end;

    World world; 
    Path pathMaker = new Path();

    public Button pathingButton;
    public TMP_InputField startInput;
    public TMP_InputField goalInput;
    public TMP_InputField wallInput;

    public bool wallCreated = false;

    // Start is called before the first frame update
    void Start()
    {
        world = GameObject.Find("GridMaker").GetComponent<World>();
        pathingButton.onClick.AddListener(() => CreatePath());
        startInput.onSubmit.AddListener(ReadStartInput);
        goalInput.onSubmit.AddListener(ReadGoalInput);
        wallInput.onSubmit.AddListener(createWall);
    }

    void ReadGoalInput(string userText) 
    {
        end = ConvertInput.IntakeCoordinates(userText);
        if (!world.IsValidPosition(ref end))
        {
            Debug.Log("Warning: coordinates are not valid");
        }
        if (world.getPointState(end))
        {
            Debug.Log("Warning: there is a wall here");   
        }
    }

    void ReadStartInput(string userText) 
    {
        start = ConvertInput.IntakeCoordinates(userText);
        if (!world.IsValidPosition(ref start))
        {
            Debug.Log("Warning: coordinates are not valid");
        }
        if (world.getPointState(start))
        {
            Debug.Log("Warning: there is a wall here");   
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

    void createWall(string userText)
    {
        Point2D wallCoords = ConvertInput.IntakeCoordinates(userText);

        if (world.IsValidPosition(ref wallCoords) && !world.getPointState(wallCoords))
        {
            world.SetWallState(wallCoords, true);
            wallCreated = true;
            Debug.Log("Wall created");
        }
        else
        {
            Debug.Log("Warning: coordinates do not work");
        }
    }

    public void checkInput(Vector2 mouseClick, Dictionary<Point2D, GameObject> tiles)
    {
        Point2D mousePos = new Point2D((int)mouseClick.x, (int)mouseClick.y);
        foreach (Point2D point in tiles.Keys)
        {
            if (mousePos.Equals(point))
            {
                Debug.Log("Point works :)");
            }
        }
    }

    public bool WallsChanged()
    {
        return wallCreated;
    }
}
