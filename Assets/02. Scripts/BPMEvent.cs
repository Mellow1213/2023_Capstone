using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPMEvent : MonoBehaviour
{
    [SerializeField]
    private float bpm;

    [SerializeField] private float average_bpm;
    
    private const float FIRST_TRUMPET = 1.05f;
    private const float SECOND_TRUMPET = 1.12f;
    private const float THIRD_TRUMPET = 1.2f;
    
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
        bpm = float.Parse(FTU.Instance.GetBpmValues(0));
        BPMLevel();   
    }

    void BPMLevel()
    {
        if (bpm >= average_bpm * THIRD_TRUMPET)
        {
            Debug.Log("심박수 매우 높음. 3단계");
            FTU.Instance.BPMEvent = 3;
        }
        else if (bpm >= average_bpm * SECOND_TRUMPET)
        {
            Debug.Log("심박수 높음. 2단계");
            FTU.Instance.BPMEvent = 2;
        }
        else if((bpm >= average_bpm * FIRST_TRUMPET))
        {
            Debug.Log("심박수 다소 높음. 1단계");
            FTU.Instance.BPMEvent = 1;
        }
        else
        {
            Debug.Log("정상 심박수. 0단계");
            FTU.Instance.BPMEvent = 0;
        }
    }
}
