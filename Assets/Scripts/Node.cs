using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public Point2D Position = new();
    public List<Node> Neighbors = new();
    public Dictionary<Node, float> Costs = new();
}
