using UnityEngine;
public static class Define
{
    public static string Up = "Up";
    public static string Down = "Down";
    public static string Right = "Right";
    public static string Left = "Left";

    //상우하좌
    public static readonly Vector2Int[] fourDirections = new Vector2Int[]{
        new Vector2Int(0,1),
        new Vector2Int(1,0),
        new Vector2Int(0,-1),
        new Vector2Int(-1,0)
    };


}



public enum CharacterState{
    follow = 0,
    random = 1
}

public enum Direction
{
    None = -1, Right = 0, Up, Left, Down, Count
}


//상우하좌 (시계방향)
