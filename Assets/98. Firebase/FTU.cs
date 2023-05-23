using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;


public class FTU : MonoBehaviour // Firebase to Unity
{   private static FTU instance;

    // 싱글톤 인스턴스에 접근할 수 있는 속성
    public static FTU Instance
    {
        get
        {
            if (instance == null)
            {
                // 씬에서 MySingleton을 찾거나 없을 경우 새로 생성
                instance = FindObjectOfType<FTU>();

                // 씬에 MySingleton 인스턴스가 없는 경우 새로운 게임오브젝트에 추가
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<FTU>();
                    singletonObject.name = "FTU";
                }

                // 씬 전환 시에도 유지하기 위해 삭제하지 않음
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }
    
    
    private DatabaseReference database; // 데이터베이스

    // Start is called before the first frame update
    void Start()
    {
        // 데이터베이스의 루트 참조 위치 가져옴
        database = FirebaseDatabase.DefaultInstance.RootReference;
        database.ValueChanged += HandleValueChanged; // 데이터가 변경될 때마다 읽음
    }

    public int BPMEvent = 0;

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