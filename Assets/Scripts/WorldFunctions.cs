using System;
using Unity.VisualScripting;
using UnityEngine;

public partial class World
{
    // World constructor
    public World(int size)
    {
        this.sideSize = size;
    }

    public int GetSideSize()
    {
        return sideSize;
    }
    
    
    // Creates Point2D that would be in the cardinal direction
    public static Point2D E(ref Point2D point)
    {
        return new Point2D(point.x + 1, point.y);
    }
    public static Point2D W(ref Point2D point)
    {
        return new Point2D(point.x - 1, point.y);
    }
    public static Point2D NE(ref Point2D p)
    {
        if (p.y % 2 == 1)
            return new Point2D(p.x + 1, p.y - 1);
        return new Point2D(p.x, p.y - 1);
    }
    public static Point2D NW(ref Point2D p)
    {
        if (p.y % 2 == 1)
            return new Point2D(p.x, p.y - 1);
        return new Point2D(p.x - 1, p.y - 1);
    }
    public static Point2D SE(ref Point2D p)
    {
        if (p.y % 2 == 1)
            return new Point2D(p.x, p.y + 1);
        return new Point2D(p.x - 1, p.y + 1);
    }
    public static Point2D SW(ref Point2D p)
    {
        if (p.y % 2 == 1)
            return new Point2D(p.x + 1, p.y + 1);
        return new Point2D(p.x, p.y + 1);
    }
    
    // Bool checks
    public bool IsValidPosition(ref Point2D point)
    {
        float sideOver2 = sideSize / 2;
        return point.x >= -sideOver2 && point.x <= sideOver2 && point.y >= -sideOver2 && point.y <= sideOver2;
    }

    public bool IsNeighbor(ref Point2D point1, ref Point2D point2)
    {
        return SW(ref point1) == point2 || NE(ref point1) == point2 || 
               W(ref point1) == point2 || E(ref point1) == point2 ||
               SW(ref point1) == point2 || SE(ref point1) == point2;
    }

    private void CheckSideSize()
    {
        int newSize = sideSize;
        newSize = (newSize / 4) * 4 + 1;
        if (newSize < 5)
            newSize = 5;
        sideSize = newSize;
    }
}