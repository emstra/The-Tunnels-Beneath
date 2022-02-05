
using System;

public class Location
{
    public int X;
    public int Y;
    public int G;// dist from start, cost 
    public int H;// dist from destination, distance
    public int F() // CostDistance
    {
        // G+H, estimate distance as crow flies
        return H + G;
    }
    public Location Parent;

    public void SetDistance(int targetX, int targetY)
    {
        // set distance from 
        this.H = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
    }
}
