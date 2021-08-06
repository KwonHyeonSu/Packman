using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    private float life;
    
    //움직임
    private void FixedUpdate()
    {
        //입력받기
        GetInput();

        //움직임
        KeepMoving();

        // bool check = CheckMoving();
    
        // if(check) //벽 없음
        // {
        //     moving = moveDirection;
        // }
        // else{ //벽 있음
        //     //  Debug.Log("벽 발견!");
        // }
        // RaycastHit2D hit = Physics2D.Raycast(transform.position, moving, 0.55f, tileLayer);
        // Debug.DrawLine(transform.position, transform.position + moving, Color.blue);
        // if(hit.transform == null){
        //     Move(moving);
        // }
    }

    private void KeepMoving(){
        // MoveNode(TargetNode);
    }
    
    public static int inputing = 0;
    private void GetInput(){
        if (Input.GetKey(KeyCode.W)) inputing = 0;

        else if (Input.GetKey(KeyCode.A)) inputing = 1;

        else if (Input.GetKey(KeyCode.D)) inputing = 2;

        else if (Input.GetKey(KeyCode.S)) inputing = 3;

        switch(inputing){
            case 0:
                moveDirection = Vector2Int.up; //벽 유무 확인용
                break;
            case 1:
                moveDirection = Vector2Int.left;
                break;

            case 2:
                moveDirection = Vector2Int.right;
                break;
            case 3:
                moveDirection = Vector2Int.down;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
        }

        if(collision.CompareTag("Enemy")){
            //플레이어 캐릭터의 체력 감소 등 처리

        }
    }

    // private bool CheckMoving(){
    //     Vector3[] directions = new Vector3[3];
    //     bool[] isPossibleMoves = new bool[3];
    //     directions[0] = moveDirection;

    //     if(directions[0].x != 0){
    //         directions[1] = directions[0] + new Vector3(0,0.65f, 0);
    //         directions[2] = directions[0] + new Vector3(0,-0.65f, 0);
    //     }
    //     else if(directions[0].y != 0){
    //         directions[1] = directions[0] + new Vector3(0.65f,0, 0);
    //         directions[2] = directions[0] + new Vector3(-0.65f,0, 0);
    //     }
        
    //     int possibleCount = 0;
    //     for(int i = 0; i < 3 ; i++){
    //         if(i == 0){
    //             isPossibleMoves[i] = Physics2D.Raycast(transform.position, directions[i], 1, tileLayer);
    //             Debug.DrawLine(transform.position, transform.position + directions[i], Color.red);
    //         }
    //         else{
    //             isPossibleMoves[i] = Physics2D.Raycast(transform.position, directions[i], 0.8f, tileLayer);
    //             Debug.DrawLine(transform.position, transform.position + directions[i] * 0.8f, Color.yellow);

    //         }
    //         if(isPossibleMoves[i] == false){
    //             possibleCount++;
    //         }

    //     }
    //     if(possibleCount == 3){
    //         return true;
    //     }
    //     return false;

    // }
}
