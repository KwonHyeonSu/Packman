using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;


public class Convenience : EditorWindow
{

    [MenuItem("Pack_man_/Convenience")]
    private static void ShowWindow()
    {
        var window = GetWindow<Convenience>();
        window.titleContent = new GUIContent("Convenience");
        window.Show();
    }

    private void OnGUI()
    {
        
    }

    [MenuItem("Pack_man_/Convenience/Goto Home &_1")]
    public static void OnMoveEditGotoHome()
    {
        if(EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()){
            EditorSceneManager.OpenScene("Assets/Ingame/Scenes/Home.unity");
        }
    }

    [MenuItem("Pack_man_/Convenience/Goto Main &_2")]
    public static void OnMoveEditGotoMain()
    {
        if(EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()){
            EditorSceneManager.OpenScene("Assets/Ingame/Scenes/Main.unity");
        }
    }

    [MenuItem("Pack_man_/Convenience/Goto Introduce &_3")]
    public static void OnMoveEditGotoIntroduce()
    {
        if(EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()){
            EditorSceneManager.OpenScene("Assets/Ingame/Scenes/Introduce.unity");
        }
    }

    [MenuItem("Pack_man_/Convenience/Goto Ranking &_4")]
    public static void OnMoveEditGotoRanking()
    {
        if(EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()){
            EditorSceneManager.OpenScene("Assets/Ingame/Scenes/Ranking.unity");
        }
    }

    [MenuItem("Pack_man_/Convenience/Goto Ranking &_5")]
    public static void OnMoveEditGotoLoading()
    {
        if(EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()){
            EditorSceneManager.OpenScene("Assets/Ingame/Scenes/Loading.unity");
        }
    }
}

