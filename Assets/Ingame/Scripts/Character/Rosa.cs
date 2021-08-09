using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//술맨의 뒤를 따라감
public class Rosa : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        speed = 0.4f;
    }

    private Vector2Int prevPos;
    private Queue<Vector2Int> moveList = new Queue<Vector2Int>();

    private bool isFirst = true;

    private void Start() {
        
        var p = GameMgr.Instance.Player.pos;
        Astar(pos, p, GameMgr.currentMap);
        prevPos = p;
        
    }

    protected override void Following(){

        //술맨의 처음 위치로 감 (astar)
        FirstFollow();

        //술맨의 이동 위치를 리스트에 넣음
        ListAdding();

        //리스트에서 하나씩 빼서 이동


        if(!isFirst && !isMove){
            //큐에 있을 때
            if(moveList.Count != 0){
                Go();
                if(FinalNodeList != null){
                    Move(Follow(FinalNodeList));
                }

            }

            //큐가 비었을 떄
            else{
                TargetRandomPositioning();
                Astar(pos, targetPos, GameMgr.currentMap);
                if(FinalNodeList != null)
                    Move(Follow(FinalNodeList));
            }
        }

    }
   private void FirstFollow(){
        if(isFirst){
            if(FinalNodeList.Count > 1){
                Move(Follow(FinalNodeList));
                target.transform.position = new Vector3(stageData.PlayerStartPos.x, stageData.PlayerStartPos.y, 0);
                targetPos = new Vector2Int((int)stageData.PlayerStartPos.x, (int)stageData.PlayerStartPos.y);
                return;
            }
            if(FinalNodeList.Count == 0){
                isFirst = false;
                return;
            }
        }
        else return;
    }

    private int count = 0;
    private bool ListAdding(){
        var PlayerPos = GameMgr.Instance.Player.pos;
        if(PlayerPos != prevPos){
            count++;
            prevPos = PlayerPos;
            if(count == 5){
                moveList.Enqueue(PlayerPos);
                count = 0;
                return true;
            }
        }
        return false;
    }
    private Vector2Int tPos = Vector2Int.zero;
    private void Go(){
        if(tPos == Vector2Int.zero){
            tPos = moveList.Dequeue();
            target.transform.position = VecIntToV3(tPos);
            targetPos = tPos;
            Astar(pos, targetPos, GameMgr.currentMap);
        }
        else if(targetPos == pos){
            tPos = moveList.Dequeue();
            target.transform.position = VecIntToV3(tPos);
            targetPos = tPos;
            Astar(pos, targetPos, GameMgr.currentMap);
        }

        
    }


    protected override Vector2Int Follow(List<Node> finalNodeList){
        if(isMove == false && finalNodeList != null && finalNodeList.Count > 1 && finalNodeList[1] != null)
        {
            var m = new Vector2Int(finalNodeList[1].x, finalNodeList[1].y) - this.pos;
            finalNodeList.RemoveAt(1);
            if(finalNodeList.Count == 1) finalNodeList.Clear();
            return m;
        }

        return Vector2Int.zero;
    }

    protected override void OnDrawGizmos()
    {
        if(FinalNodeList != null && FinalNodeList.Count > 1){
            for(int i=1;i<FinalNodeList.Count-1;i++){
                Debug.DrawLine(new Vector2(FinalNodeList[i].x, FinalNodeList[i].y), new Vector2(FinalNodeList[i+1].x, FinalNodeList[i+1].y), Color.red);
            }
        } 
    }

 
    

    // public void Update()
    // {
    //     //처음엔 플레이어 위치로 A*를 통해 감
        

    //     // else{

    //     //     // FollowingFunc();
    //     // }
        
    //     // if(target != null && isMove == false){
    //     //     var p = GameMgr.Instance.Player.pos;
    //     //     target.transform.position = new Vector3(p.x, p.y, 0);

    //     //     Astar(pos, p, GameMgr.TestMap_Rotated);
    //     //     // mov = FindNextNode(target.transform.position);

    //     //     if(FinalNodeList != null)
    //     //         Move(Follow(FinalNodeList));
    //     // }
    // }

    
    

}
