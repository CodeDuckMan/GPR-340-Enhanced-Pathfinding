using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using Utils;

public class Path : MonoBehaviour
{
    
    public Point2D start = new Point2D();
    public Point2D end = new Point2D();
    List<Point2D> path  = new List<Point2D>();

    public int gregX = 0;
    public int gregY = 0;

    public int larryX = 5;
    public int larryY = 5;

    public int leftBound = -10;
    public int rightBound = 10;
    public int topBound = -10;
    public int bottomBound = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        Point2D agent = new Point2D(gregX, gregY); // placeholder for agent. 
        end = new Point2D(larryX, larryY);
        start = agent;
    }
    
    /*
     * a struct line segment with 2 functions, one interpolates the other creates the path
     * 
     * struct line segment (Point start and point end)
     * {
     *      point2D interpolation(float t) {
     * // need add and multiply, subtract operators
     * auto delta = end - start
     * return delta * t + start
     * }
     *
     * Point2D inter2Seg(Point2D p0, Point2D p1, point2D p2, float t) {
     * lineSegment ls1 = {p0, p1}
     * lineSegment ls1 = {p1, p2}
     *
     * Point2D pt1 = ls1.interpolation(t)
     * Point2D pt2 = ls2.interpolation(t)
     *
     * LineSegment lsFinal = {pt1, pt2}
     *
     * return lsFinal.interpolation
     * }
     *
     *
     * 
     * }
     */

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartPathfinding(Point2D start, Point2D end)
    {
        PriorityQueue<Point2D, Point2D> something; 

        // get startingPoint (agent)
        // get size of grid (to make goal point)
    }

    void printPath()
    {
        
    }

    void resetPath()
    {
        path = new List<Point2D>();
    }

    void calculateHerustic() 
    { 
    
    }

    void showFlowField() 
    { 
    
    }
}
