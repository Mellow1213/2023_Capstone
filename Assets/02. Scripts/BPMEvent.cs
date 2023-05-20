using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPMEvent : MonoBehaviour
{
    [SerializeField]
    private int bpm;

    [SerializeField] private float average_bpm;
    // Start is called before the first frame update
    void Start()
    {
        bpm = 0;
        if (average_bpm == 0)
            average_bpm = 85.0f;
    }

    // Update is called once per frame
    void Update()
    {
        BPMLevel();   
    }

    void BPMLevel()
    {
        if (bpm >= average_bpm * 1.2f)
        {
            Debug.Log("심박수 매우 높음. 3단계");
        }
        else if (bpm >= average_bpm * 1.12f)
        {
            Debug.Log("심박수 높음. 2단계");
            
        }
        else if((bpm >= average_bpm * 1.05f))
        {
            Debug.Log("심박수 다소 높음. 1단계");
        }
        else
        {
            Debug.Log("정상 심박수. 0단계");
        }
    }
}
