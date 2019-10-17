using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PairComparer : IEqualityComparer<Pair>
{
    public bool Equals(Pair x, Pair y)
    {
        Debug.Log($"Comparing {x.ToString()} and {y.ToString()}, answer {(x.x == y.x && x.y == y.y) || (x.x == y.y && x.y == y.x)}");
        return (x.x == y.x && x.y == y.y) || (x.x == y.y && x.y == y.x);
    }

    public int GetHashCode(Pair obj)
    {
        Debug.Log($"Returning hash code {(obj.x > obj.y ? obj.x * 10 + obj.y : obj.y * 10 + obj.x)}");
        return obj.x > obj.y ? obj.x * 10 + obj.y : obj.y * 10 + obj.x;
    }
    
}
