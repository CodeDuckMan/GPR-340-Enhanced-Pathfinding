using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public partial class World : MonoBehaviour
{
    public Canvas targetCanvas;
    public Sprite hexagonSprite;
    
    public int newSideSize = 11;
    public float hexagonSize = 100;
    public int wallSpawnChance = 10;
    
    // true means that the hex is blocked
    private Dictionary<Point2D, bool> worldStateHash;
    public Dictionary<Point2D, GameObject> worldStateGameObjects;
    
    private int sideSize = 11;
    private bool sizeHasChanged = false;

    private void Awake()
    {
        Initialize();
        
        Debug.Log("Hex Grid Generator Start");
        CheckSideSize();
        
        DrawGrid();
        
        Point2D somePoint = new Point2D(0, 0);
        List<Point2D> valid = GetEmptyNeighbors(somePoint);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ResetHash();
    }
}
