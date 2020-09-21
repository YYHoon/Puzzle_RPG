using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Index
{
    public int x;
    public int y;
    
    public Index(int mx, int my)
    {
        x = mx;
        y = my;
    }

    public Index(Vector2 vector)
    {
        x = (int)vector.x;
        y = (int)vector.y;
    }

    public static Index right { get { return new Index(1, 0); } }
    public static Index left { get { return new Index(-1, 0); } }
    public static Index up { get { return new Index(0, -1); } }
    public static Index down { get { return new Index(0, 1); } }
}
