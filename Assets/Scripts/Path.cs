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
        var visited = new List<Point2D>();
        

        distances[startNode] = 0;
        queue.Enqueue(startNode, 0); // setting the start point. 

        while (queue.Count > 0) // while there is something in the queue.
        {
            var current = queue.Dequeue(); // checking the one we are taking off the queue.
            if (current == goalNode) // if we find a goal space. Stop searching.
            {
                break;
            }
            visited.Add(current.Position);

            foreach (var neighbor in GetNeighbors(current, visited, world)) // get the neighbors of the point.
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

    private List<Node> GetNeighbors(Node node, List<Point2D> visited, World world)
    {
        List<Point2D> possibleNeighbors = world.GetEmptyNeighbors(node.Position);
        List<Node> neighbors = new List<Node>();
        
        foreach (Point2D p in possibleNeighbors)
        {
            if (visited.Contains(p))
            {
                continue;
            }
            neighbors.Add(new Node(p));
        }
        // sets a base neighbor list.
        return neighbors;
    }
}
