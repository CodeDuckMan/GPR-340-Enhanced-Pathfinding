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
    [SerializeField] private List<Point2D> _currentPath;
    [SerializeField] private Point2D _currentTargetPoint;
    [SerializeField] private Point2D _nextPointOnPath;
    [SerializeField] private Sprite targetSprite;
    [SerializeField] private GameObject targetPoint;
    private RectTransform _baTransform;
    
    void Start()
    {
        gridMaker = GameObject.Find("GridMaker");
        targetCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        
        _currentPath = new List<Point2D>();
        _baTransform = GetComponent<RectTransform>();
        
        worldScript = gridMaker.GetComponent<World>();
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
        
        targetPoint = new GameObject("Target" );
        Image newImage = targetPoint.AddComponent<Image>();
        newImage.sprite = targetSprite;

        RectTransform startPointTransform = _worldStateGameObjects[_currentPath.First()].GetComponent<RectTransform>();
        _baTransform.localPosition = startPointTransform.localPosition;

        this.transform.SetParent(targetCanvas.transform, false);
        this.transform.SetAsLastSibling();
    }

    void FixedUpdate()
    {
        FollowPath(_currentPath);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void FollowPath(List<Point2D> path)
    {
        Point2D closestPointOnPath = worldScript.GetClosestPointOnPath(_baTransform, path, _worldStateGameObjects);
        int closestPointIndex = path.FindIndex(point => point.Equals(closestPointOnPath));

        if (closestPointIndex + 1 < path.Count)
        {
            _nextPointOnPath = path[closestPointIndex + 1];
        }

        _baTransform.position = Vector3.MoveTowards(_baTransform.position, _worldStateGameObjects[_nextPointOnPath].transform.position, speed * Time.deltaTime);


        targetPoint.transform.position = _worldStateGameObjects[_nextPointOnPath].transform.position;

    }

}

