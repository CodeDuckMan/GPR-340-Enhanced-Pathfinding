using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;
using Utils;

public class Path
{
    public List<Point2D> FindPath(Point2D start, Point2D goal, World world)
    {
        Node startNode = new Node(start);
        Node goalNode = new Node(goal);

        var distances = new Dictionary<Node, float>();
        var cameFrom = new Dictionary<Node, Node>();
        var queue = new PriorityQueue<Node, float>(); // setting variables here.
        

        distances[startNode] = 0;
        queue.Enqueue(startNode, 0); // setting the start point. 

        while (queue.Count > 0) // while there is something in the queue.
        {
            var current = queue.Dequeue(); // checking the one we are taking off the queue.
            if (current == goalNode) // if we find a goal space. Stop searching.
            {
                break;
            }

            foreach (var neighbor in GetNeighbors(current, world)) // get the neighbors of the point.
            {
                float newDistance = distances[current] + 1; // add cost to path.
                if (!distances.ContainsKey(neighbor) || newDistance < distances[neighbor]) // checks cost and neighbors.
                { 
                    distances[neighbor] = newDistance; 
                    cameFrom[neighbor] = current;
                    queue.Enqueue(neighbor, newDistance); // sets data and cost then enqueues it.
                }
            }
        }

        return ReversePath(cameFrom, startNode, goalNode); // returns a path after search is finished. 
    }

    private List<Point2D> ReversePath(Dictionary<Node, Node> path, Node first, Node last)
    {
        List<Point2D> newPath = new(); // setting variables.
        Node current = last; 

        while (current != first) // will check the list backwards until it reaches the start node.
        {
            newPath.Add(current.Position);
            if (!path.TryGetValue(current, out var value)) // if it cant make a path then return empty.
            {
                return new List<Point2D>();
            }
            current = value;
        }

        newPath.Add(first.Position); 
        newPath.Reverse(); 
        return newPath; // once finished returns the path. 
    }

    private List<Node> GetNeighbors(Node node, World world)
    {
        List<Node> neighbors = new List<Node>(); // sets a base neighbor list.
        List<Point2D> neighborPos = new List<Point2D> // sets a base neighbor position list.
        {
            World.NE(ref node.Position),
            World.E(ref node.Position),
            World.SE(ref node.Position),
            World.SW(ref node.Position),
            World.W(ref node.Position),
            World.NW(ref node.Position)
        };

        foreach (Point2D p in neighborPos.ToList()) // this will remove any invalid neighbors.
        {
            if (!CheckPosition(p, world)) // checks if point is out of bounds or on a wall. 
            {
                neighborPos.Remove(p); // if so remove it from the neighbors list.
            }
            else 
            {
                Node neighbor = new Node(p); // else adds it to a neighbor list. 
                neighbors.Add(neighbor); 
            }
        }

        return neighbors;
    }

    private bool CheckPosition(Point2D position, World world) 
    {
        return world.IsValidPosition(ref position) && (!world.getPointState(position));
    }
}
