using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public partial class World
{
    // World constructor
    public World(int size)
    {
        this.newSideSize = size;
    }

    public int GetSideSize()
    {
        return newSideSize;
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
        float sideOver2 = newSideSize / 2;
        return point.x >= -sideOver2 && point.x <= sideOver2 && point.y >= -sideOver2 && point.y <= sideOver2;
    }

    public bool IsNeighbor(ref Point2D point1, ref Point2D point2)
    {
        return SW(ref point1) == point2 || NE(ref point1) == point2 || 
               W(ref point1) == point2 || E(ref point1) == point2 ||
               SW(ref point1) == point2 || SE(ref point1) == point2;
    }

    private bool CheckSideSize()
    {
        // Clamp the newSideSize
        int newSize = (newSideSize / 4) * 4 + 1;
        if (newSize < 5)
            newSize = 5;
        
        if (sideSize != newSize)
        {
            sideSize = newSize;
            newSideSize = newSize;
            worldState = new List<bool>();
            worldState.Capacity = sideSize * sideSize;
            return true;
        }

        return false;
    }

    private void CreateHexagonOnPoint(double x, double y)
    {
        
        // Create new gameobject
        GameObject newHexigonGameObject = new GameObject("Hexagon: " + Math.Floor(x) + ", " + Math.Floor(y));
        
        // Add image component
        Image newImage = newHexigonGameObject.AddComponent<Image>();
        
        // Check for sprite
        if (hexagonSprite != null)
        {
            newImage.sprite = hexagonSprite;
        }
        else
        {
            Debug.LogWarning(newHexigonGameObject.name + " does not have a sprite");
        }
        
        // Add to canvas
        newHexigonGameObject.transform.SetParent(targetCanvas.transform, false);
        
        RectTransform hexagonRectTransform = newHexigonGameObject.GetComponent<RectTransform>();
        hexagonRectTransform.anchoredPosition = new Vector2((float)x * hexagonSize, (float)y * hexagonSize);
        hexagonRectTransform.sizeDelta = new Vector2(hexagonSize, hexagonSize);

    }

    private void DrawGrid()
    {
        for (int x = -sideSize / 2; x < sideSize / 2; x++)
        {
            for (int y = -sideSize / 2;  y < sideSize / 2; y++)
            {
                if (y % 2 == 0)
                    CreateHexagonOnPoint(x,y);
                
                else
                    CreateHexagonOnPoint(x + 0.5,y);
            }
        }
    }
}