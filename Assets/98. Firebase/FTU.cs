using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;


public class FTU : MonoBehaviour // Firebase to Unity
{
    private DatabaseReference database;
    // Start is called before the first frame update
    void Start()
    {
        // 데이터베이스의 루트 참조 위치 가져옴
        database = FirebaseDatabase.DefaultInstance.RootReference;
        database.ValueChanged += HandleValueChanged;    // 데이터가 변경될 때마다 읽음
    }
    
    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if(args.DatabaseError != null) 
        {
            Debug.Log(args.DatabaseError.Message); // 에러 메세지
            return;
        }
        Debug.Log(args.Snapshot.Child("bpm").Value); // bpm에 있는 데이터 로그 찍음
    }
}
