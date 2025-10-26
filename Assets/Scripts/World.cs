using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public partial class World : MonoBehaviour
{
    public Canvas targetCanvas;
    public Sprite hexagonSprite;
    
    public int newSideSize = 11;
    public float hexagonSize = 100;
    
    private List<bool> worldState;
    private int sideSize = 11;
    private bool sizeHasChanged = false;

    private void Start()
    {
        Debug.Log("Hex Grid Generator Start");
        CheckSideSize();
        
        DrawGrid();

    }

    private void FixedUpdate()
    {
        
    }
}
