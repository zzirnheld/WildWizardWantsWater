using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pair
{
    public int x;
    public int y;

    public Pair(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override bool Equals(object obj)
    {
        if(!(obj is Pair)) return false;
        Pair p = obj as Pair;
        return (x == p.x && y == p.y) || (x == p.y && y == p.x);
    }

    public override string ToString()
    {
        return x.ToString() + ", " + y.ToString();
    }
}
