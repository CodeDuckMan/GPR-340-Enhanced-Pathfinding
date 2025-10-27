using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using Utils;

public class Path
{
    public List<Point2D> FindPath(Node start, Node goal, World world) { // swap out world for actual thingy
        var distances = new Dictionary<Node, float>();
        var cameFrom = new Dictionary<Node, Node>();
        var queue = new PriorityQueue<Node, float>();

        distances[start] = 0;
        queue.Enqueue(start, 0);

        while (queue.Count > 0) 
        {
            var current = queue.Dequeue();
            if (current == goal) 
            {
                break;
            }

            foreach (var node in world.getNeighbors) // need to look into this, make a function
            { 
                var neighbor = new Node();
                float newDistance = distances[current]; // + get the cost of current to neighbor
                // need to make a neighbor variable or function
                if (!distances.ContainsKey(neighbor) || newDistance < distances[neighbor])
                {
                    distances[neighbor] = newDistance;
                    cameFrom[neighbor] = current;
                    queue.Enqueue(neighbor, newDistance);
                }
            } 
        }

        return reversePath(cameFrom, start, goal);
    }

    public List<Point2D> reversePath(Dictionary<Node, Node> path, Node first, Node last) 
    {
        List<Point2D> newPath = new();
        Node current = last;
        
        while (current != first) 
        {
            newPath.Add(current.Position);
            if (!path.ContainsKey(current)) 
            {
                return new List<Point2D>();
            }
            current = path[current];
        }

        newPath.Add(first.Position);
        newPath.Reverse();
        return newPath;
    }
}
