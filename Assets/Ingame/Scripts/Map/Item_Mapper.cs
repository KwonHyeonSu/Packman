using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Mapper : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;

    List<GameObject> mchild = new List<GameObject>();

    private void Awake()
    {
        //코인 받아오기
        Coin_Mapping();
    }


    void Coin_Mapping(){
        for(int i = 0; i<this.transform.childCount; i++){
            GameObject a = (this.transform.GetChild(i).gameObject);
            if(a.name == "Item_coin"){
                mchild.Add(a);
            }
        }

        Debug.Log("Item_coin - Mapping Complete => Total : " + mchild.Count);

    }
    

    
}
