// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Raymond : Enemy
// {
//     Character player;
    
//     protected override void Awake()
//     {
//         base.Awake();

//         player = GameMgr.Instance.Player;

//         //target 색 구분
//         target.GetComponent<SpriteRenderer>().color = Color.cyan; 

//         moveTime = 0.4f;
        
//     }

//     protected override void Following()
//     {
//         base.Following();
//         if(player.isMOVE == false){
//             //진행 방향에 타겟을 놓는다.
//             targetPos = TargetPositioning();
//         }

//         if(!isMove && target != null){

//             if(targetPos == pos){
//                 TargetRandomPositioning();
//             }

//             UsingAstar(pos, targetPos, GameMgr.currentMap);
//         }
//     }

//     Vector2Int TargetPositioning(){

//         for(int i = 1 ; i < stageData.Limit; i++){
//             Vector2Int p = player.pos + new Vector2Int((int)player.moving.x, (int)player.moving.y) * i;

//             if(BoundaryCheck(p)){
//                 //벽일 때
//                 if(GameMgr.TestMap_Rotated[p.x, p.y] == 1){
                    
//                     target.transform.position = new Vector3(p.x - player.moving.x, p.y - player.moving.y, 0);
//                     return new Vector2Int(p.x - (int)player.moving.x, p.y - (int)player.moving.y);
//                 }
//             }
//         }
//         return Vector2Int.zero;
//     }

//     protected override void OnDrawGizmos()
//     {
//         if(FinalNodeList != null && FinalNodeList.Count != 0){
//             for(int i=0;i<FinalNodeList.Count-1;i++){
//                 Debug.DrawLine(new Vector2(FinalNodeList[i].x, FinalNodeList[i].y), new Vector2(FinalNodeList[i+1].x, FinalNodeList[i+1].y), Color.cyan);
//             }
//         }    
//     }


// }
