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
        var distances = new Dictionary<Point2D, float>();
        var cameFrom = new Dictionary<Point2D, Point2D>();
        var queue = new PriorityQueue<Point2D, float>(); 
        var visited = new HashSet<Point2D>();

        distances[start] = 0;
        queue.Enqueue(start, 0); // setting the start point. 

        while (queue.Count > 0) // while there is something in the queue.
        {
            var current = queue.Dequeue(); // checking the one we are taking off the queue.
            if (current == goal) // if we find a goal space. Stop searching.
            {
                break;
            }

            if (!visited.Add(current)) continue;

            foreach (var neighbor in GetNeighbors(current, visited, world)) // get the neighbors of the point.
            {
                var newDistance = distances[current] + 1; // add cost to path.
                if (!distances.ContainsKey(neighbor) || newDistance < distances[neighbor]) // checks cost and neighbors.
                { 
                    distances[neighbor] = newDistance; 
                    cameFrom[neighbor] = current;
                    queue.Enqueue(neighbor, newDistance); // sets data and cost then enqueues it.
                }
            }
        }
        return ReversePath(cameFrom, start, goal); // returns a path after search is finished. 
    }

    private List<Point2D> ReversePath(Dictionary<Point2D, Point2D> path, Point2D first, Point2D last)
    {
        List<Point2D> newPath = new(); // setting variables.
        Point2D current = last; 

        while (current != first) // will check the list backwards until it reaches the start node.
        {
            newPath.Add(current);
            if (!path.TryGetValue(current, out var value)) // if it cant make a path then return empty.
            {
                return new List<Point2D>();
            }
            current = value;
        }

        newPath.Add(first); 
        newPath.Reverse(); 
        return newPath; // once finished returns the path. 
    }

    private List<Point2D> GetNeighbors(Point2D current, HashSet<Point2D> visited, World world)
    {
        var possibleNeighbors = world.GetEmptyNeighbors(current); // gets all possible neighbors
        var neighbors = new List<Point2D>();
        
        foreach (var p in possibleNeighbors) 
        {
            if (!visited.Contains(p)) // checks if neighbor has been visited
            {
                neighbors.Add(p); // if not adds it
            } 
        }
        return neighbors;
    }
}
