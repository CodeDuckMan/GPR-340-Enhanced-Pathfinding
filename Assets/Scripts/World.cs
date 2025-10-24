using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public partial class World : MonoBehaviour
{
    public int sideSize = 11;
    public Point2D lastPosition = Point2D.Empty;
    
    private Vector<bool> worldState;
    
    private void Start()
    {
        Debug.Log("Hex Grid Generator Start");
        CheckSideSize();

    }

    private void FixedUpdate()
    {
        CheckSideSize();
    }
}
