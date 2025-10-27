using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using Utils;

public class Path
{
    public List<Point2D> FindPath(Node start, Node goal, World world)
    { // swap out world for actual thingy
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

            foreach (var neighbor in getNeighbors(current)) // need to look into this, make a function
            {
                float newDistance = distances[current] + 1; // + get the cost of current to neighbor
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

    public List<Node> getNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();
        List<Point2D> neighborPos = new List<Point2D>();

        neighborPos.Add(World.NE(ref node.Position));
        neighborPos.Add(World.E(ref node.Position));
        neighborPos.Add(World.SE(ref node.Position));
        neighborPos.Add(World.SW(ref node.Position));
        neighborPos.Add(World.W(ref node.Position));
        neighborPos.Add(World.NW(ref node.Position));

        foreach (Point2D p in neighborPos) 
        {
            if (!checkPosition(p))
            {
                neighborPos.Remove(p);
            }
            else 
            { 
                Node neighbor = new Node();
                neighbor.Position = p;
                neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }

    public bool checkPosition(Point2D position) 
    {
        if (position.x < 0 || position.y < 0) 
        {
            return false;
        }

        if (position.x > 10 || position.y > 10) 
        {
            return false;
        }
        
        position.GetType(); // look into this and what it does

        return true;
    }
}
