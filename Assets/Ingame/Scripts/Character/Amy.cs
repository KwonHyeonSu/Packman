// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Amy : Enemy
// {
//     private Character player;
//     protected override void Awake()
//     {
//         base.Awake();
//         player = GameMgr.Instance.Player;

//         //target 색 구분
//         target.GetComponent<SpriteRenderer>().color = Color.magenta; 

//         moveTime = 0.4f;
//     }

//     protected override void Following()
//     {

//         TargetSetting();

//         if(target != null && isMove == false)
//         {
//             if(targetPos == pos){
//                 TargetRandomPositioning();
//             }
//             UsingAstar(pos, targetPos, GameMgr.currentMap);
//         }
//         // if(target != null && isMove == false){
//         //     var p = player.pos;
//         //     target.transform.position = new Vector3(p.x, p.y, 0);
//         //     targetPos = player.pos;

//         //     if(targetPos == pos){
//         //         TargetRandomPositioning();
//         //     }

//         //     if(DistanceCheck()){
//         //         Running();
//         //         UsingAstar(pos, targetPos, GameMgr.currentMap);

//         //     }
//         //     else{
//         //         UsingAstar(pos, targetPos, GameMgr.currentMap);

//         //     }
//         // }
//     }

//     private void TargetSetting(){
        
//         var pos1 = GameMgr.Instance.Jake.pos;
//         var pos2 = GameMgr.Instance.Raymond.pos;
//         var pos3 = GameMgr.Instance.Rosa.pos;

//         Vector2 _pos = (pos1 + pos2 + pos3) / 3;

//         var pos = new Vector2Int((int)_pos.x, (int)_pos.y);

//         foreach(var d in Define.fourDirections){
//             if(BoundaryCheck(pos) && GameMgr.currentMap.Map[(pos+d).x, (pos+d).y] == 0){
//                 pos = pos+d;
//                 break;
//             }
//         }

//         target.transform.position = VecIntToV3(pos);
//         targetPos = pos;
//     }

//     // private void Running(){
//     //     float shortest = 999;
//     //     Vector2Int movDir = Vector2Int.zero;
//     //     foreach(var d in Define.fourDirections){
//     //         var tmp = (pos + d - player.pos).magnitude;
//     //         if(tmp < shortest){
//     //             tmp = shortest;
//     //             movDir = d;
//     //         }
//     //     }
//     //     target.transform.position = VecIntToV3(pos + movDir);
//     //     targetPos = pos + movDir;
//     // }

//     // private float pastDist;
//     // private bool DistanceCheck(){
//     //     var flag = false;
//     //     float curDist = (pos - player.pos).magnitude;
//     //     if(curDist < pastDist){
//     //         Vector2 p = (Vector2)pos + ((Vector2)moving * -1);
//     //         if(GameMgr.currentMap[(int)p.x, (int)p.y] != 1){
//     //             Debug.LogError("Move Changed - Jake");
//     //             flag = true;
//     //         }
//     //     } 
//     //     pastDist = curDist;
//     //     return flag;
//     // }

//     // protected override void OnDrawGizmos()
//     // {
//     //     if(FinalNodeList != null && FinalNodeList.Count > 1){
//     //         for(int i=1;i<FinalNodeList.Count-1;i++){
//     //             Debug.DrawLine(new Vector2(FinalNodeList[i].x, FinalNodeList[i].y), new Vector2(FinalNodeList[i+1].x, FinalNodeList[i+1].y), Color.magenta);
//     //         }
//     //     } 
//     // }
// }
