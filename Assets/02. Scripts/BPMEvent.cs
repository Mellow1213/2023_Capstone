using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BPMEvent : MonoBehaviour
{
    [SerializeField]
    private float bpm;

    [SerializeField] private float average_bpm;
    
    private const float FIRST_TRUMPET = 1.05f;
    private const float SECOND_TRUMPET = 1.12f;
    private const float THIRD_TRUMPET = 1.2f;
    private const float BPMSYSDOWN = 1.26f;
    private const float REST_TIME = 30;

    private bool isRest = false;

    public GameObject RestObject;
    private Material RestObjMat;

    // Start is called before the first frame update
    void Start()
    {
        bpm = 0;
        if (average_bpm == 0)
            average_bpm = 85.0f;

        RestObjMat = GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        //bpm = float.Parse(FTU.Instance.GetBpmValues(0));
        
        BPMLevel();   
    }

    IEnumerator RestOn()
    {
        isRest = true;
        RestObjMat.
        FTU.Instance.BPMEvent = 0;
        yield return new WaitForSeconds(REST_TIME);
        isRest = false;
    }
    void BPMLevel()
    {
        if (!isRest)
        {
            BPMLevelCheck();
            Debug.Log("UI 안 띄워놓은 상태");
        }
        else
        {
            Debug.Log("UI 띄워놓은 상태");
        }
    }

    void BPMLevelCheck()
    {
        if (bpm >= average_bpm * BPMSYSDOWN)
        {
            StartCoroutine(RestOn());
            Debug.Log("심박수 이상 상태. 이벤트 종료");
        }
        else if (bpm >= average_bpm * THIRD_TRUMPET)
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
