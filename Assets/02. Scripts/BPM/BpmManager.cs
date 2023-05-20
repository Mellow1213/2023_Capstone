using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BpmManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI BPM_TextMeshPro;
    private FTU _firebaseToUnity;
    public string FirebaseManagerName = "FTUManager";

    private string currentTime;         // 데이터베이스에서 가져온 날짜 및 시간 저장하는 변수
    private double currentBPM = 0;
    private float timer = 0;

    BpmData bpmData = new BpmData();    // json 파일로 저장할 데이터 객체
    private string fileName;    // json 파일명

    void Start()
    {
        _firebaseToUnity = GameObject.Find(FirebaseManagerName).GetComponent<FTU>();
        fileName = "test";
    }
    
    // Update is called once per frame
    void Update()
    {
        currentBPM = Double.Parse(_firebaseToUnity.GetBpmValues(0));

        currentTime = _firebaseToUnity.GetBpmValues(1); // 날짜 + 시간 저장
        
        timer += Time.deltaTime;
        if (timer > 1)
        {
            timer = 0;
            Debug.Log("출력");
            if(currentBPM != 0.0)
            {
                BPM_TextMeshPro.text = "BPM : " + currentBPM;

                bpmData.bpm.Add(currentBPM.ToString());     // bpm 리스트에 데이터베이스에서 가져온 bpm 저장
                BpmSave.Save(bpmData, fileName);            // json 파일에 저장
            }
                
            else
            {
                Debug.Log("0으로 측정 중. 이전 값을 출력합니다");
            }

        }

    }
}
