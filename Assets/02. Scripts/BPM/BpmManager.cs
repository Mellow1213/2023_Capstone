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

    private double currentBPM = 0;
    private float timer = 0;
    void Start()
    {
        _firebaseToUnity = GameObject.Find(FirebaseManagerName).GetComponent<FTU>();
    }

    // Update is called once per frame
    void Update()
    {
        currentBPM = Double.Parse(_firebaseToUnity.GetBpmValues(0));
        
        timer += Time.deltaTime;
        if (timer > 1)
        {
            timer = 0;
            Debug.Log("출력");
            if(currentBPM != 0.0)
                BPM_TextMeshPro.text = "BPM : " + currentBPM;
            else
            {
                Debug.Log("0으로 측정 중. 이전 값을 출력합니다");
            }
        }
    }
}
