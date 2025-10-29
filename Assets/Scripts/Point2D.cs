using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public struct Point2D
{
    public int x, y;

    public Point2D(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    // Overloads for arithmetic 
    public static Point2D operator +(Point2D operand) => operand; // For positive
    public static Point2D operator -(Point2D operand) => new Point2D(-operand.x, -operand.y); // For negative
    public static Point2D operator +(Point2D left, Point2D right) => new Point2D(left.x + right.x, left.y + right.y);
    public static Point2D operator -(Point2D left, Point2D right) => left + (-right);
    public static Point2D operator *(Point2D operand, int num) => new Point2D(operand.x * num, operand.y * num);
    public static Point2D operator /(Point2D operand, int num) => new Point2D(operand.x / num, operand.y / num);
    
    // We cannot do =+ or -= because we are not working in a version older than 14.0
    
    // Overloads for boolean Point2D
    public static bool operator ==(Point2D left, Point2D right) => (left.x == right.x && left.y == right.y);
    public static bool operator !=(Point2D left, Point2D right) =>!(left == right);
    public static Point2D Empty { get; set; }

    public override bool Equals(object obj) // This is for the Equals() function
    {
        if (obj is not Point2D other)
            return false;
        
        return x == other.x && y == other.y;
    }

    public override int GetHashCode()
    {
        int theCode = x + y * x;
        return HashCode.Combine(theCode);
    }
}

public struct Point2DWithPriority
{
    Point2D point;
    int priority;

    Point2DWithPriority(Point2D point, int priority)
    {
        this.point = point;
        this.priority = priority;
    }

    public static bool operator >(Point2DWithPriority left, Point2DWithPriority right)
    {
        return left.priority > right.priority;
    }

    public static bool operator <(Point2DWithPriority left, Point2DWithPriority right)
    {
        return left.priority < right.priority;
    }
}
