using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    protected GameObject target = null;
    protected Vector2Int targetPos = Vector2Int.zero;

    protected virtual void Awake()
    {
        GameObject Target = Resources.Load("Prefeb/Target") as GameObject;

        if(Target == null)
        {
            Debug.LogError("리소스에 타겟이 없습니다 -> " + this.name);
        }
        else
        {
            target = Instantiate(Target, transform.position, Quaternion.identity);
            target.GetComponent<SpriteRenderer>().sortingOrder = 100; 
        }
           
    }

    //target과 가장 가까운 노드로 이동
    public Vector2Int FindNextNode(Vector2 targetPos){
        float shortest = 99.0f;
        Vector2Int ret = Vector2Int.zero;
        foreach(var d in Define.fourDirections){
            Vector2Int tempPos = pos + d;

            if(BoundaryCheck(tempPos)){ //맵 밖을 벗어나지 않는지 확인

                if(checkPos(tempPos)){ //해당 노드로 이동할 수 있는지 확인
                    float dist = (targetPos - tempPos).magnitude;
                    if(shortest > dist){
                        shortest = dist;
                        ret = d;
                    }
                }
                else continue;
            }
            else continue;

        }
        return ret;
    }

    protected Vector2Int V3ToVecInt(Vector2Int v){
        return new Vector2Int((int)v.x, (int)v.y);
    }

    protected Vector3 VecIntToV3(Vector2Int v){
        return new Vector3(v.x, v.y, 0);
    }



    private void Update()
    {
        //target 지정해서 따라가기 (Virtual)
        Following();

        //targetPos 가져오기
        targetPos = new Vector2Int((int)target.transform.position.x, (int)target.transform.position.y);

    }

    protected void UsingAstar(Vector2Int pos, Vector2Int targetPos, MAP map){
        Astar(pos, targetPos, map);

        if(FinalNodeList != null)
            Move(Follow(FinalNodeList));
        
        return;
    }

    int _x = 0;
    int _y = 0;

    protected virtual void TargetRandomPositioning(){
        if(pos == targetPos){
            m_state = CharacterState.random;
            // Debug.Log("RandomPositioning...");
            _x = UnityEngine.Random.Range(1, stageData.Limit);
            _y = UnityEngine.Random.Range(1, stageData.Limit);
            while(GameMgr.currentMap.NodeMap[_x,_y].isWall == false){
                // Debug.Log("RandomPositioning...");
                _x = UnityEngine.Random.Range(1, stageData.Limit);
                _y = UnityEngine.Random.Range(1, stageData.Limit);
            }

            target.transform.position = new Vector3(_x, _y, 0);
            targetPos = new Vector2Int(_x, _y);
        }
    }

    protected virtual void Following(){
        m_state = CharacterState.follow;
    }

    public bool checkPos(Vector2Int p){
        if(GameMgr.currentMap.NodeMap[p.x, p.y].isWall){
            return false;
        }
        else return true;

    }

    public bool BoundaryCheck(Vector2Int p){
        if(p.x > stageData.LimitMax.x || p.x < stageData.LimitMin.x){
            return false;
        }
        else if(p.y > stageData.LimitMax.y || p.y < stageData.LimitMin.y){
            return false;
        }
        return true;
    }


/////////////////A STAR 알고리즘

    protected List<Node> Open = null, Closed = null, FinalNodeList = null;
    Node[,] NodeArray;
    
    private Node CurNode, StartNode, TargetNode;
    protected bool isFind = false;

    public void Astar(Vector2Int startPos, Vector2Int targetPos, MAP map){
        int sizeX = map.x, sizeY = map.y;
        if(startPos == targetPos)
            return;

        if(Open != null)
            Open.Clear();
        if(Closed != null)
            Closed.Clear();
        if(FinalNodeList != null)
            FinalNodeList.Clear();
        
        Debug.Log("Astar Calculating...");
        isFind = true;
        if(NodeArray == null){

            NodeArray = new Node[sizeX, sizeY];

            //할당하기
            for(int i = 0 ; i < map.x; i++){
                for(int j = 0 ; j < map.y ; j++){
                    bool isWall = false;
                    if(map.NodeMap[i,j].isWall){
                        isWall = true;
                    }
                    NodeArray[i,j] = new Node(isWall, i, j);
                }
            }
        }

        //Vector2Int 여야함
        StartNode = NodeArray[startPos.x, startPos.y];
        TargetNode = NodeArray[targetPos.x, targetPos.y];

        Open = new List<Node>();
        Closed = new List<Node>();
        FinalNodeList = new List<Node>();

        //Open리스트의 처음을 시작노드
        Open.Add(StartNode);

        while(Open.Count > 0){
            CurNode = Open[0];
            for(int i = 1; i < Open.Count;i++){
                if((Open[i].f <= CurNode.f) && (Open[i].h < CurNode.h)){
                    CurNode = Open[i];
                }
            }
            

            Open.Remove(CurNode);
            Closed.Add(CurNode);

            if(CurNode == TargetNode){
                Node TargetCurNode = TargetNode;

                while(TargetCurNode != StartNode){
                    FinalNodeList.Add(TargetCurNode);
                    TargetCurNode = TargetCurNode.parent;
                }

                FinalNodeList.Add(StartNode);
                FinalNodeList.Reverse();

                //이동중
                return;
            }

            OpenAdd(CurNode.x, CurNode.y+1);
            OpenAdd(CurNode.x+1, CurNode.y);
            OpenAdd(CurNode.x, CurNode.y-1);
            OpenAdd(CurNode.x-1, CurNode.y);
        }
    }

    void OpenAdd(int checkX, int checkY){
        //범위 내인지
        if((checkX <= stageData.Limit) && (checkX >= 0) && (checkY <= stageData.Limit) && (checkY>=0)) 
        {
            // Debug.LogError(checkX+ "\t"+ checkY);
            if(!NodeArray[checkX, checkY].isWall && !Closed.Contains(NodeArray[checkX, checkY])){
                Node NeighborNode = NodeArray[checkX, checkY];

                int MoveCost = 0;
                if(CurNode.x - checkX == 0 || CurNode.y - checkY == 0){
                    MoveCost = CurNode.g + 1;
                }

                if((MoveCost < NeighborNode.g) || (!Open.Contains(NeighborNode))){
                    NeighborNode.g = MoveCost;

                    NeighborNode.h = Math.Abs(NeighborNode.x - TargetNode.x) + Math.Abs(NeighborNode.y - TargetNode.y);
                    NeighborNode.parent = CurNode;
                    
                    Open.Add(NeighborNode);
                }
            }
        }
    }

    protected virtual Vector2Int Follow(List<Node> finalNodeList){
        if(finalNodeList.Count != 0)
        {
            var m = new Vector2Int(finalNodeList[1].x, finalNodeList[1].y) - this.pos;
            return m;
        }
        return Vector2Int.zero;
    }
    
    protected virtual void OnDrawGizmos()
    {
        if(FinalNodeList != null && FinalNodeList.Count != 0){
            for(int i=0;i<FinalNodeList.Count-1;i++){
                Debug.DrawLine(new Vector2(FinalNodeList[i].x, FinalNodeList[i].y), new Vector2(FinalNodeList[i+1].x, FinalNodeList[i+1].y), Color.red);
            }
        }    
    }

}
