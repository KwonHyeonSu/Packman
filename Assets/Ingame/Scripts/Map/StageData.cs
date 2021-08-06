using UnityEngine;
using System;

/*
클래스 선언 위에 CreateAssetMenu를 붙이면 Project View의 생성 메뉴에서
해당 클래스 타입의 Asset 생성 가능


ScriptableObject는 공유 데이터를 저장할 수 있는 클래스
-게임에 사용되는 데이터를 저장해두고 게임 중간에 불러오기 기능
-게임 도중 데이터 변경 가능
    Editor Mode : 게임 도중 변경된 데이터 그대로 유지.
    Release Mode : 게임이 종료되면 처음 데이터로 리셋된다.

*/

[CreateAssetMenu]
public class StageData : ScriptableObject
{
    [SerializeField]
    private Vector2 limitMin = Vector2.zero;

    [SerializeField]
    private Vector2 limitMax= Vector2.zero;


    public Vector2 LimitMin => limitMin;
    public Vector2 LimitMax => limitMax;

    public readonly int Limit = 12; //가로 세로 길이

    [SerializeField]
    private Vector2Int _PlayerStartPos = new Vector2Int(2, 7);
    public Vector2Int PlayerStartPos{
        get => _PlayerStartPos;
        set => _PlayerStartPos = value;
    }

    [SerializeField]
    public Vector2Int RosaStartPos = new Vector2Int(9, 8);

    [SerializeField]
    public Vector2Int RaymondStartPos = new Vector2Int(1, 10);
    [SerializeField]
    public Vector2Int JakeStartPos = new Vector2Int(7, 7);

    [SerializeField]
    public Vector2Int AmyStartPos = new Vector2Int(7, 2);

    public const int pojang_y = 31;
    public const int pojang_x = 38;
    public readonly int [,] pojang = new int [pojang_y,pojang_x];
    
}

public class MAP
{
    public Node[,] NodeMap {get; set;}

    int _y;

    public int y {
        get => _y;
        set => _y = value;
    }
    int _x;

    public int x {
        get => _x;
        set => _x = value;
    }
}
