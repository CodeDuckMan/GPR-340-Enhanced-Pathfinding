using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAgent : MonoBehaviour
{
    public float speed = 10.0f;
    public GameObject GridMaker;
    private Dictionary<Point2D, GameObject> _worldStateGameObjects;
    [SerializeField] private World worldScript;
    [SerializeField] private List<Point2D> _currentPath;
    [SerializeField] private Point2D currentTargetPoint;
    private RectTransform _baTransform;
    
    void Start()
    {
        _currentPath = new List<Point2D>();
        _baTransform = GetComponent<RectTransform>();
        worldScript = GridMaker.GetComponent<World>();
        _worldStateGameObjects = worldScript.worldStateGameObjects;
        
        _currentPath.Add(new Point2D(-3, 0));
        worldScript.SetPointColor(new Point2D(-3, 0), Color.magenta);
        _currentPath.Add(new Point2D(-2, 0));
        worldScript.SetPointColor(new Point2D(-2, 0), Color.magenta);
        _currentPath.Add(new Point2D(-1, 0));
        worldScript.SetPointColor(new Point2D(-1, 0), Color.magenta);
        _currentPath.Add(new Point2D(0, 0));
        worldScript.SetPointColor(new Point2D(0, 0), Color.magenta);
        _currentPath.Add(new Point2D(0, 1));
        worldScript.SetPointColor(new Point2D(0, 1), Color.magenta);
        _currentPath.Add(new Point2D(0, 2));
        worldScript.SetPointColor(new Point2D(0, 2), Color.magenta);
        _currentPath.Add(new Point2D(0, 3));
        worldScript.SetPointColor(new Point2D(0, 3), Color.magenta);
        _currentPath.Add(new Point2D(0, 4));
        worldScript.SetPointColor(new Point2D(0, 4), Color.magenta);
        

    }

    void FixedUpdate()
    {
        FollowPath(_currentPath);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void FollowPath(List<Point2D> path)
    {
        Point2D closestPointOnPath = worldScript.GetClosestPointOnPath(_baTransform, path, _worldStateGameObjects);
        Point2D nextPointOnPath = new Point2D();
        int closestPointIndex = path.FindIndex(point => point.Equals(closestPointOnPath));

        if (closestPointIndex + 1 < path.Count)
        {
            nextPointOnPath = path[closestPointIndex + 1];
        }
        else
        {
            nextPointOnPath = path[closestPointIndex];
        }
        
        
        _baTransform.position = Vector3.MoveTowards(_baTransform.position, _worldStateGameObjects[nextPointOnPath].transform.position, speed * Time.deltaTime);


    }
    
}
