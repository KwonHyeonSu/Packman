using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    protected StageData stageData;

    [SerializeField]
    protected CharacterState m_state;
    
    
    [SerializeField]
    protected float moveTime = 0.5f;
    protected bool isMove = false;
    public bool isMOVE{
        get{
            return isMove;
        }
        set{
            
        }
    }
    protected bool readyToAstar = true;

    protected float rayDistance = 0.55f; 
    protected LayerMask tileLayer; 
    // protected Direction direction = Direction.Right;

    protected Vector2Int direction;
    protected Animator anim;
    protected Vector2Int moveDirection = Vector2Int.right; //움직이려는 방향
    public Vector2Int movingDir = Vector2Int.right; //현재 진행 방향


    //이동 관련
    protected Node startNode;
    protected Node TargetNode;


    [SerializeField]
    public Vector2Int pos; //현재 위치
    IEnumerator coroutine; //코루틴



    //////////////////////초기화////////////////////////

    public void Init(){

        stageData = GameMgr.stageData;
        direction = Vector2Int.right;
        tileLayer = 1 << LayerMask.NameToLayer("Tile");

        anim = gameObject.GetComponent<Animator>();
        if(anim == null) Debug.LogError("애니메이션 할당 안됨 - Character.cs");

        //노드지정
        startNode = GameMgr.Instance.GetNode(pos);
        Debug.LogError(this.name + " - startNode : " + startNode.pos);
        TargetNode = GameMgr.Instance.GetNode(pos + direction);
        Debug.LogError(this.name + " - targetNode : " + TargetNode.pos);

    }

    //해당 노드로 이동
    // public void MoveNode(Node targetNode){
    //     float percent = 0;
    //     Vector3 wasPos = transform.position;
    //     while(true){
    //         percent += Time.deltaTime;
    //         transform.position = Vector3.Lerp(wasPos, new Vector3(targetNode.x, targetNode.y, 0), moveTime);
    //     }
    // }

    // public void Move(Vector3 moving){
    //     MoveTo(moving);
    //     Animating(moving);
    // }

    // public bool MoveTo(Vector3 moving)
    // {
    //     if(isMove){
    //         return false;
    //     }
        
        
    //     coroutine = SmoothGridMovement(moving);
    //     StartCoroutine(coroutine);

    //     return true;
        
    // }

    


    // private IEnumerator SmoothGridMovement(Vector3 moving)
    // {
    //     Vector3 startPosition = transform.position;
    //     float percent = 0;

    //     readyToAstar = false;
    //     isMove = true;

    //     // if(startPosition.x <= stageData.LimitMin.x){
    //     //     if(moving == Vector3.right){}
    //     //     else startPosition.x = stageData.LimitMax.x;
    //     // }
    //     // else if(startPosition.x >= stageData.LimitMax.x){
    //     //     if(moving == Vector3.left){}
    //     //     else startPosition.x = stageData.LimitMin.x;

    //     // }
    //     // else if(startPosition.y <= stageData.LimitMin.y){
    //     //     if(moving == Vector3.up){}
    //     //     else startPosition.y = stageData.LimitMax.y;
    //     // }
    //     // else if(startPosition.y >= stageData.LimitMax.y){
    //     //     if(moving == Vector3.down){}
    //     //     else startPosition.y = stageData.LimitMin.y;
    //     // }

    //     // transform.position = startPosition;

    //     Vector3 endPosition = startPosition + moving;

    //     while (percent < moveTime)
    //     {
    //         percent += Time.deltaTime;
    //         transform.position = Vector3.Lerp(startPosition, endPosition, percent / moveTime);

    //         yield return null;
    //     }
    //     pos = new Vector2Int((int)endPosition.x, (int)endPosition.y);

    //     isMove = false;
    //     readyToAstar = true;
    // }

    protected virtual void Animating(Vector3 v){
        if(v == Vector3.up){
            anim.Play("Up");
        }
        else if(v == Vector3.right){
            anim.Play("Right");
        }
        else if(v == Vector3.down){
            anim.Play("Down");
        }
        else if(v == Vector3.left){
            anim.Play("Left");

        }
    }

    
}
