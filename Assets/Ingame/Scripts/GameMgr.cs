using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    void OnGUI()
    {
        
        GUI.Box(new Rect(10,10,500,100), "에디터용 설정");
        if(GUI.Button(new Rect(20,40,80,20), "개발자 모드")){
            if(GameMgr.Instance != null){
                GameMgr.Instance.DevMod = !GameMgr.Instance.DevMod;
                Debug.Log("개발자 모드 : " + GameMgr.Instance.DevMod);
            }
        }
        
    }

    #region 싱글톤
    private static GameMgr instance;
    public static GameMgr Instance{
        get{
            if(instance == null){
                GameObject obj = new GameObject();
            }
            return instance;
        }
    }

    void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else{
            Destroy(this.gameObject);
        }

        stageData = Resources.Load("StageData") as StageData;
        if(stageData == null) Debug.LogError("stageData가 할당되지 않았습니다. ID : " + name);
        
    }
    #endregion

#region 캐릭터 수치 설정
    public Character Player;

    [SerializeField]
    public bool DevMod = false;

    public int Player_Life = 5;
    public Character Rosa;
    public Character Amy;
    public Character Jake;
    public Character Raymond;

    [SerializeField]
    public float Rosa_moveTime = 0.4f;
    public static int [,] TestMap_Rotated;

    
#endregion
    public static StageData stageData;

    public GameObject StartPosResource;
    public Vector3 RosaStartPos;

    public static MAP currentMap;

    //map[0] : 포장마차(pojang)
    public List<MAP> mapList = new List<MAP>();

    void OnEnable(){
        SceneManager.activeSceneChanged += OnSceneLoaded;
    }


    //Scene이 게임플레이(Main) 씬으로 바뀌었을 때
    private void OnSceneLoaded(Scene prevScene, Scene changedScene){
        
        //Debug.LogError("씬 바뀜 : " + changedScene.name);
        if(changedScene.name == "Main"){
            Debug.Log("게임플레이 초기화중...");
            GameInit();
            StartCoroutine(TemporaryStop());
            
        }
    }

    #region 게임초기화
    void GameInit(){
        
        Mapping();

        StartPosResource = Resources.Load("StartPos") as GameObject;

        Player = InitCharacters("Player");
        Rosa = InitCharacters("Rosa");
        // Raymond = InitCharacters("Raymond");
        // Jake = InitCharacters("Jake");
        // Amy = InitCharacters("Amy");
        
    }
    #endregion

    public GameObject dummy_1;
    public GameObject dummy_2;

    public void Mapping(){

        Debug.Log("포장마차 Mapping...");
        MAP map = new MAP();
        map.NodeMap = new Node[StageData.pojang_x, StageData.pojang_y];

        Node[,] tmpNodes = new Node[StageData.pojang_x, StageData.pojang_y];
        map.x = StageData.pojang_x;
        map.y = StageData.pojang_y;
        
        for (int i = 0 ; i < map.x ; i++){
            for(int j = 0 ; j < map.y ; j++){
                Node tmpNode = new Node(false, 0, 0);
                Collider2D hit = Physics2D.OverlapBox(new Vector2(i, j), new Vector2(1, 1), 0);
                
                if(hit == null){
                    tmpNode.isWall = false;
                }
                else{
                    tmpNode.isWall = true;
                }
                tmpNode.pos = new Vector2Int(i,j);
                tmpNode.g = 0;
                tmpNode.h = 0;
                tmpNodes[i, j] = tmpNode;
            }
        }

        map.NodeMap = tmpNodes;

        mapList.Add(map);

        Debug.Log("포장마차 Mapping Complete!");

        //잘 매핑 되었는지 확인
        Display(mapList[0]); //포장마차 넣기

        currentMap = mapList[0];
        
    }

    // public T[,] Rotate<T>(T[,] sourceArray){


    //     int lengthY = sourceArray.GetLength(0);
    //     int lengthX = sourceArray.GetLength(1);

    //     T[,] targetArray = new T[lengthX, lengthY];

    //     for(int y = 0; y < lengthY; y++){
    //         for(int x = 0; x < lengthX; x++){
    //             targetArray[x,y] = sourceArray[lengthY - 1 - y, x];
    //         }
    //     }
    //     return targetArray;
    // }
    

    //잘 매핑 되었는지 확인
    public void Display(MAP map){
        for (int i = 0 ; i < map.x ; i++){
            for(int j = 0 ; j < map.y ; j++){                
                if(mapList[0].NodeMap[i,j].isWall == false){
                    Instantiate(dummy_2, new Vector2(i,j), Quaternion.identity); //밝음
                }
                else{
                    Instantiate(dummy_1, new Vector2(i,j), Quaternion.identity); //어두움
                }
            }
        }
    }
    
    
    public Character InitCharacters(String name){
        Character ch = null;
        Debug.Log("캐릭터를 생성 중입니다... => " + name);
        Vector2Int startPos = Vector2Int.zero;

        if(name == "Player") startPos = stageData.PlayerStartPos;
        else if(name == "Rosa") startPos = stageData.RosaStartPos;
        else if(name == "Raymond") startPos = stageData.RaymondStartPos;
        else if(name == "Jake") startPos = stageData.JakeStartPos;
        else if(name == "Amy") startPos = stageData.AmyStartPos;
        else
        {
            Debug.LogError("위치 할당 오류 - GameMgr.cs" +name);
            return null;
        }

        //시작위치 더미 생성
        GameObject StartPosObject = Instantiate(StartPosResource, new Vector3(startPos.x, startPos.y, 0), Quaternion.identity);
        StartPosObject.name = name + "_StartPos";

        //캐릭터 생성
        var res = Resources.Load("Prefeb/" + name) as GameObject;
        if(res == null){
            Debug.LogError(name + " 리소스가 없습니다. - GameMgr.cs");
            return null;
        }
        else{
            GameObject Obj = Instantiate(res, StartPosObject.transform.position, Quaternion.identity);
            Obj.name = name;
            Obj.GetComponent<SpriteRenderer>().sortingOrder = 10;

            ch = Obj.GetComponent<Character>();

            ch.pos = startPos;

        }
        //startNode 정해주는 함수
        ch.Init();

        Debug.Log("캐릭터 생성 완료! => " + name);
        
        return ch;
    }

    void OnDisable(){
        SceneManager.activeSceneChanged -= OnSceneLoaded;

    }

    IEnumerator TemporaryStop(){
        Time.timeScale = 0.0f;
        while(!Input.anyKeyDown){
            yield return null;
        }
        Time.timeScale = 1.0f;
    }
    

    public Node GetNode(Vector2Int Pos){
        var n = currentMap.NodeMap[Pos.x, Pos.y];
        // Debug.LogError("getnode!" + Pos);
        if(n != null)
            return currentMap.NodeMap[Pos.x, Pos.y];
        else return null;
    }

}
