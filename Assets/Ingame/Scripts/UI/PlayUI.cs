using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayUI: MonoBehaviour
{
    public static int inputing = 0;

    public void 방향키_위(){
        Debug.Log("위 방향키");

        inputing = 0;
    }

    public void 방향키_왼(){
        Debug.Log("왼 방향키");
        inputing = 1;
    }

    public void 방향키_오른(){
        Debug.Log("위 방향키");
        inputing = 2;
    }

    public void 방향키_아래(){
        Debug.Log("아래 방향키");
        inputing = 3;
    }

    public void 일시정지(){
        Time.timeScale = 0.0f;
    }

    public void 계속하기(){
        Time.timeScale = 1;
    }

    public void 홈으로(){
        SceneManager.LoadScene("Home");
        Time.timeScale = 1;
    }

    public void 재도전(){
        //코딩하기
    }
}
