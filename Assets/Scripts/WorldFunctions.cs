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

    private void Initialize()
    {
        worldStateHash = new Dictionary<Point2D, bool>();
        worldStateGameObjects =  new Dictionary<Point2D, GameObject>();
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

    public bool getPointState(Point2D point)
    {
        return worldStateHash[point];
    }
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

    public List<Point2D> GetEmptyNeighbors(Point2D point)
    {
        var neighbors = new List<Point2D>();
        
        if(worldStateHash.ContainsKey(NE(ref point)))
            if(!worldStateHash[NE(ref point)])
                neighbors.Add(NE(ref point));
        
        if(worldStateHash.ContainsKey(NW(ref point)))
            if(!worldStateHash[NW(ref point)])
                neighbors.Add(NW(ref point));
        
        if(worldStateHash.ContainsKey(E(ref point)))
            if(!worldStateHash[E(ref point)])
                neighbors.Add(E(ref point));
        
        if(worldStateHash.ContainsKey(W(ref point)))
            if(!worldStateHash[W(ref point)])
                neighbors.Add(W(ref point));
        
        if(worldStateHash.ContainsKey(SW(ref point)))
            if(!worldStateHash[NE(ref point)])
                neighbors.Add(NE(ref point));
        
        if(worldStateHash.ContainsKey(SE(ref point)))
            if(!worldStateHash[SE(ref point)])
                neighbors.Add(SE(ref point));
        
        return neighbors;
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
            
            return true;
        }

        return false;
    }

    private void ResetHash()
    {
        worldStateHash.Clear();
        foreach (var hexGameObjectKey in worldStateGameObjects)
        {
            GameObject hexGameObject = hexGameObjectKey.Value;
            Destroy(hexGameObject);
        }
    }
    private void CreateHexagonOnPoint(double x, double y)
    {
        // Add to hash
        Point2D thePoint = new Point2D((int)Math.Floor(x), (int)Math.Floor(y));
        
        worldStateHash[thePoint] = false;
        
        // Create new GameObject
        GameObject newHexagonGameObject = new GameObject("Hexagon: " + Math.Floor(x) + ", " + Math.Floor(y));
        worldStateGameObjects[thePoint] = newHexagonGameObject;
        
        // Add image component
        Image newImage = newHexagonGameObject.AddComponent<Image>();
        
        // Check for sprite
        if (hexagonSprite != null)
        {
            newImage.sprite = hexagonSprite;
        }
        else
        {
            Debug.LogWarning(newHexagonGameObject.name + " does not have a sprite");
        }
        
        // Add to canvas
        newHexagonGameObject.transform.SetParent(targetCanvas.transform, false);
        
        RectTransform hexagonRectTransform = newHexagonGameObject.GetComponent<RectTransform>();
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