using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;


public class FTU : MonoBehaviour // Firebase to Unity
{
    private DatabaseReference database;

    private string bpm;

    // Start is called before the first frame update
    void Start()
    {
        // 데이터베이스의 루트 참조 위치 가져옴
        database = FirebaseDatabase.DefaultInstance.RootReference;
        database.ValueChanged += HandleValueChanged; // 데이터가 변경될 때마다 읽음
    }

    private string bpmValue;
    private string[] bpmValues;

    public string[] GetBpmValues()
    {
        return bpmValues;
    }

    public string GetBpmValues(int num)
    {
        if (num == 0) return bpmValues[0];
        else return bpmValues[1];
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.Log(args.DatabaseError.Message); // 에러 메세지
            return;
        }

        bpmValue = args.Snapshot.Child("bpm").Value.ToString(); // bpm 값 가져오기
        bpmValues = bpmValue.Split(' '); // 스페이스바를 기준으로 문자열 나누기
    }
}