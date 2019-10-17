using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PairComparer : IEqualityComparer<Pair>
{
    public bool Equals(Pair x, Pair y)
    {
        return (x.x == y.x && x.y == y.y) || (x.x == y.y && x.y == y.x);
    }

    public int GetHashCode(Pair obj)
    {
        return obj.GetHashCode();
    }
    
}
