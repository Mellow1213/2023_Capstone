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
        // �����ͺ��̽��� ��Ʈ ���� ��ġ ������
        database = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))    // ���� ���콺 Ŭ�� ��
        {
            database.ValueChanged += HandleValueChanged;    // �����Ͱ� ����� ������ ����
        }
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if(args.DatabaseError != null) 
        {
            Debug.Log(args.DatabaseError.Message); // ���� �޼���
            return;
        }
        Debug.Log(args.Snapshot.Child("bpm").Value); // bpm�� �ִ� ������ �α� ����
    }
}
