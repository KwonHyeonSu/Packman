using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{

    public bool isWall;
    public int x, y;
    public int g;
    public int h;
    public Node parent;
    public Node(bool _isWall, int _x, int _y){
        isWall = _isWall;
        x = _x;
        y = _y;
        pos = new Vector2Int(x, y);
    }

    public Vector2Int pos;

    public int f{
        get{
            return g + h;
        }
    }
}
