using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    private float life;

    public GameObject dummy_3;
    public GameObject dummy_4;

    private GameObject startPosObj;
    private GameObject targetPosObj;

    private void Start() {
        startPosObj = Instantiate(dummy_3, V2IntToV3(pos), Quaternion.identity); //start
        targetPosObj = Instantiate(dummy_4, V2IntToV3(pos), Quaternion.identity); //target

    }
    
    //움직임
    private void FixedUpdate()
    {
        
        GetInput();     //입력받기
        DisplayDir();   //움직임 확인

        NodeCheck();    //노드 최신화
        MoveTo();       //움직임

        startPosObj.transform.position = V2IntToV3(startNode.pos);
        targetPosObj.transform.position = V2IntToV3(targetNode.pos);
        
    }

#region 움직임 v2

    public void NodeCheck(){
        if(pos == targetNode.pos){
            startNode = targetNode;
            pos = startNode.pos;
            if(GetNode(pos + moveDirection).isWall == false){
                targetNode = GetNode(pos + moveDirection);
                movingDir = moveDirection;
            }
            else{
                targetNode = GetNode(pos + movingDir);
            }
        }
    }

    private float t = 0.0f;
    public void MoveTo(){
        
        //왔던 길 돌아올 때 바로 반응하도록 예외처리
        if(GetNode(targetNode.pos + moveDirection).pos == startNode.pos){
            movingDir = moveDirection;
            pos = targetNode.pos;
            var tempNode = startNode;
            startNode = targetNode;
            targetNode = tempNode;
            t = 1-t;
            
        }
        
        else if(GetNode(pos + moveDirection).isWall == false && IsInt(transform.position)){
            targetNode = GetNode(pos + moveDirection);
            movingDir = moveDirection;
        }

        if(!GetNode(pos + movingDir).isWall){
            t += Time.deltaTime * speed;
            if(t <= 1)
                transform.position = Vector2.Lerp(startNode.pos, targetNode.pos, t);
            else{
                t = 0.0f;
                transform.position = V2IntToV3(targetNode.pos);
                pos = targetNode.pos;
                return;
            }
            // transform.Translate(V2IntToV3(movingDir) * speed * Time.deltaTime);
            
            Animating(movingDir);
        }
        
    }
    
    public bool IsInt(Vector3 v){
        if((v.x - (int)v.x) == 0 && (v.y - (int)v.y) == 0) return true;
        else return false;
    }
    #endregion
    
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

}
