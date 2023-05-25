using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class BPMEvent : MonoBehaviour
{
    [SerializeField]
    private float bpm; // 현재 BPM 값

    [SerializeField] private float average_bpm; // 평균 BPM 값
    
    private const float FIRST_TRUMPET = 1.05f; // 첫 번째 트럼펫 상수
    private const float SECOND_TRUMPET = 1.12f; // 두 번째 트럼펫 상수
    private const float THIRD_TRUMPET = 1.2f; // 세 번째 트럼펫 상수
    private const float BPMSYSDOWN = 1.26f; // 심박수가 너무 빨라졌을 때의 상수
    private const float REST_TIME = 3; // 휴식 시간

    private bool isRest = false; // 휴식 중인지 여부를 나타내는 변수

    public GameObject RestObject; // 휴식 표시용 객체
    private Material RestObjMat; // 휴식 표시용 객체의 머티리얼

    // Start is called before the first frame update
    void Start()
    {
        bpm = 0;
        if (average_bpm == 0)
            average_bpm = 85.0f;

        RestObjMat = RestObject.GetComponent<Renderer>().material; // 휴식 표시용 객체의 머티리얼 가져오기
    }

    // Update is called once per frame
    void Update()
    {
        bpm = float.Parse(FTU.Instance.GetBpmValues(0)); // 현재 BPM 값 가져오기
        BPMLevel();   
    }

    IEnumerator RestOn()
    {
        isRest = true;
        RestObject.SetActive(true); // 휴식 표시용 객체 활성화
        RestObjMat.DOFade(1, 2); // 휴식 표시용 객체의 투명도 조절
        FTU.Instance.BPMEvent = 0; // BPM 이벤트 초기화
        yield return new WaitForSeconds(REST_TIME); // 휴식 시간 동안 대기
        RestObjMat.DOFade(0, 2); // 휴식 표시용 객체의 투명도 조절
        yield return new WaitForSeconds(2);
        RestObject.SetActive(false); // 휴식 표시용 객체 비활성화
        isRest = false; // 휴식 상태 해제
    }
    
    void BPMLevel()
    {
        if (!isRest)
        {
            BPMLevelCheck(); // BPM 수준 체크
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
            StartCoroutine(RestOn()); // 휴식 시작
            Debug.Log("심박수 이상 상태. 이벤트 종료");
        }
        else if (bpm >= average_bpm * THIRD_TRUMPET)
        {
            Debug.Log("심박수 매우 높음. 3단계");
            FTU.Instance.BPMEvent = 3; // 3단계 이벤트 설정
        }
        else if (bpm >= average_bpm * SECOND_TRUMPET)
        {
            Debug.Log("심박수 높음. 2단계");
            FTU.Instance.BPMEvent = 2; // 2단계 이벤트 설정
        }
        else if((bpm >= average_bpm * FIRST_TRUMPET))
        {
            Debug.Log("심박수 다소 높음. 1단계");
            FTU.Instance.BPMEvent = 1; // 1단계 이벤트 설정
        }
        else
        {
            Debug.Log("정상 심박수. 0단계");
            FTU.Instance.BPMEvent = 0; // 0단계 이벤트 설정
        }
    }
}
