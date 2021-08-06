using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IMGUI : MonoBehaviour
{
    void OnGUI()
    {
        
        GUI.Box(new Rect(10,10,500,100), "에디터용 설정");
        if(GUI.Button(new Rect(20,40,80,20), "개발자 모드")){
            if(GameMgr.Instance != null)
                GameMgr.Instance.DevMod = !GameMgr.Instance.DevMod;
        }
        
    }


}
