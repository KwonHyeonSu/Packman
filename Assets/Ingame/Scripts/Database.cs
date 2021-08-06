using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Database;
using Firebase.Unity;

public class Database : MonoBehaviour
{
    public class User{
        public string username;
        public string content;
        public string score;
        public User(string username, string score, string content){
            this.username = username;
            this.score = score;
            this.content = content;
        }
    }

    DatabaseReference reference;
    int count = 1;
    void Start() {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void OnClickSave(){
        writeNewUser("현수", "2000", "easy");
        count++;
        writeNewUser("나영", "3000", "바보");
        count++;
    }

    public void LoadBtn(){
        readUser("Personal Information");
    }

    private void readUser(string username){
        reference.Child(username).GetValueAsync().ContinueWith(task =>{
            if(task.IsFaulted){
                //handle the error...
                Debug.Log("error");
            }
            else if(task.IsCompleted){
                DataSnapshot snapshot = task.Result;
                Debug.Log("snapshot : " + snapshot.ChildrenCount);

                foreach(DataSnapshot data in snapshot.Children){
                    IDictionary personInfo = (IDictionary)data.Value;
                    Debug.Log("이름 : " + personInfo["username"] + ", 점수: " + personInfo["score"] + ", 내용 :" + personInfo["content"]);
                }
            }
        });
    }

    private void writeNewUser(string username, string score, string content){
        User user = new User(username, score, content);
        string json = JsonUtility.ToJson(user);
        reference.Child("Personal Information").Child("num" + count.ToString()).SetRawJsonValueAsync(json);
    }
}
