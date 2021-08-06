using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{   
    public static string nextScene;

    [SerializeField]
    Slider progressBar;
    

    private float waitTime = 1.0f; //1초간 강제로 로딩하게 만들기

    private void Start(){
        StartCoroutine(LoadScene());
    }
    
    public static void LoadScene(string sceneName){
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }

    IEnumerator LoadScene(){
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        // float timer = 0.0f;
        float timer_force = 0.0f;


        while(!op.isDone){
            yield return null;
            // timer += Time.deltaTime;

            // if(op.progress < 0.9f){
            //     progressBar.value = Mathf.Lerp(progressBar.value, op.progress, timer);
            //     if(progressBar.value >= op.progress){
            //         timer = 0f;
            //     }
            // }               
            while(timer_force <= waitTime){
                timer_force += Time.deltaTime;
                progressBar.value = Mathf.Lerp(progressBar.value, 1, timer_force);
                yield return null;
            }
            if(progressBar.value >= 1.0f)
                op.allowSceneActivation = true;

        }
    }
    

}
