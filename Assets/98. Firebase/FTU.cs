using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using TMPro;


public class FTU : MonoBehaviour // Firebase to Unity
{
    private DatabaseReference database;
    public TextMeshProUGUI BPM_TextMeshPro;
    private string bpm;
    // Start is called before the first frame update
    void Start()
    {
        // 데이터베이스의 루트 참조 위치 가져옴
        database = FirebaseDatabase.DefaultInstance.RootReference;
        database.ValueChanged += HandleValueChanged;    // 데이터가 변경될 때마다 읽음
    }

    private string bpmValue;
    private string[] bpmValues;
    private float timer = 0;
    
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            timer = 0;
            Debug.Log("출력");
            if(bpmValues[0] != "0.0")
                BPM_TextMeshPro.text = "BPM : " + bpmValues[0];
            else
            {
                Debug.Log("0으로 측정 중. 이전 값을 출력합니다");
            }
        }
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if(args.DatabaseError != null) 
        {
            Debug.Log(args.DatabaseError.Message); // 에러 메세지
            return;
        }
        bpmValue = args.Snapshot.Child("bpm").Value.ToString(); // bpm 값 가져오기
        bpmValues = bpmValue.Split(' '); // 스페이스바를 기준으로 문자열 나누기
    }
}
