using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BallAgent : MonoBehaviour
{
    public float speed = 10.0f;
    public GameObject gridMaker;
    public Canvas targetCanvas;
    private Dictionary<Point2D, GameObject> _worldStateGameObjects;
    [SerializeField] private World worldScript;
    private List<Point2D> _currentPath;
    private Point2D _currentTargetPoint;
    private Point2D _nextPointOnPath;
    private RectTransform _baTransform;
    private DynamicPath _dynamicPath;
    
    void Awake()
    {
        // Check for required Game Objects
        gridMaker = GameObject.Find("GridMaker");
        if (gridMaker == null)
        {
            Debug.LogError("GridMaker not found");
            Destroy(gameObject);
        }
        
        targetCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        if (targetCanvas == null)
        {
            Debug.LogError("TargetCanvas not found");
            Destroy(gameObject);
            
        }

        _dynamicPath = GameObject.Find("PathSmothingGen").GetComponent<DynamicPath>();
        if (_dynamicPath == null)
        {
            Debug.LogError("PathSmothingGen not found");
            Destroy(gameObject);
        }

        // Initialize variables
        _currentPath = new List<Point2D>();
        _baTransform = GetComponent<RectTransform>();
        
        worldScript = gridMaker.GetComponent<World>();
        _worldStateGameObjects = worldScript.worldStateGameObjects;

        setPath();
    }

    void FixedUpdate()
    {
        if (_dynamicPath.WallsChanged())
        {
            setPath();
            _dynamicPath.wallCreated = false;
        }

        FollowPath(_currentPath);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void FollowPath(List<Point2D> path)
    {
        // This gets the closest point with the current position, when the closest is the other one it will change
        Point2D closestPointOnPath = worldScript.GetClosestPointOnPath(_baTransform, path, _worldStateGameObjects);
        int closestPointIndex = path.FindIndex(point => point.Equals(closestPointOnPath));

        if (closestPointIndex + 1 < path.Count)
        {
            _nextPointOnPath = path[closestPointIndex + 1];
        }

        _baTransform.position = Vector3.MoveTowards(_baTransform.position, _worldStateGameObjects[_nextPointOnPath].transform.position, speed * Time.deltaTime);
        
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void setPath()
    {
        // bool pathIntact = true;
        
        // tells us there already is a path
        
        if (_currentPath != null)
        {
            foreach (Point2D point in _currentPath)
            {
                if (!worldScript.getPointState(point))
                {
                    worldScript.SetPointColor(point, Color.white);
                }
            }

            if (worldScript.getPointState(_dynamicPath.start))
            {
                _dynamicPath.start = _currentPath[1];
            }

            if (worldScript.getPointState(_dynamicPath.end))
            {
                _dynamicPath.end = _currentPath[^2];
            }
        }

        _currentPath = _dynamicPath.CreatePath();
        
        // Check if path exists
        
        if (_currentPath == null)
        {
            Debug.LogError("Path does not exist");
            Destroy(gameObject);
        }
        else
        {
            foreach (Point2D point in _currentPath)
            {
                worldScript.SetPointColor(point, Color.magenta);
            }
            
            RectTransform startPointTransform = _worldStateGameObjects[_currentPath.First()].GetComponent<RectTransform>();
            _baTransform.localPosition = startPointTransform.localPosition;

            this.transform.SetParent(targetCanvas.transform, false);
            this.transform.SetAsLastSibling();
        }
    }

}

