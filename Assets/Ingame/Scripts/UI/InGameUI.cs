using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    private Button button;

    public void 홈화면버튼(){
        // Debug.Log("홈화면버튼 클릭");
        LoadingSceneManager.LoadScene("Home");
        //SceneManager.LoadScene("Home");
    }

    public void 게임시작버튼()
    {
        // Debug.Log("게임시작버튼 클릭");
        LoadingSceneManager.LoadScene("Main");
        //SceneManager.LoadScene("Main");
    }
/*
    public void 게임설정버튼()
    {
        Debug.Log("게임설정버튼 클릭");
        SceneManager.LoadScene("Setting");
    }
*/
    public void 게임소개버튼()
    {
        // Debug.Log("게임소개버튼 클릭");
        LoadingSceneManager.LoadScene("Introduce");
        //SceneManager.LoadScene("Introduce");
    }

    public void 명예의전당버튼()
    {
        // Debug.Log("명예의전당버튼 클릭");
        LoadingSceneManager.LoadScene("Ranking");

        //SceneManager.LoadScene("Ranking");
    }

    public void 게임종료버튼()
    {
        // Debug.Log("게임종료버튼 클릭");
        Application.Quit();
    }
}
