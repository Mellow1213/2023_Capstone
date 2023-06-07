using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crBMPEvent : MonoBehaviour
{
    public Material classRoomsky;            // 기본 스카이박스(Material)
    public Material classRoomskyChange;      // 변경할 스카이박스(Material)

    public GameObject[] blood;               // 피 이펙트 배열
    public GameObject sizeUpBlood;           // 크기 확대 피 이펙트

    private int level = 1;                   // 이벤트 레벨 변수

    private bool[] isFilled;                 // 각 피 이펙트가 활성화되었는지를 확인하는 배열

    private bool sEvent1 = false; // 심박수 이벤트1 진행 여부를 확인하는 변수

    private int cnt = 0;

    void Start()
    {
        isFilled = new bool[blood.Length];

        // 피 이펙트 초기화
        for (int i = 0; i < blood.Length; i++)
        {
            blood[i].SetActive(false);
            isFilled[i] = false;
        }

        RenderSettings.skybox = classRoomsky;   // 기본 스카이박스 설정
        sizeUpBlood.SetActive(false);           // 크기 확대 피 이펙트 비활성화

        Invoke("BloodOn", 60f);                 // 60초 후에 피 이펙트 활성화
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && level < 5 && !sEvent1)   // FTU.Instance.BPMEvent 값이 1이고 이 이벤트는 다섯번 실행 가능
                                                                   //if (FTU.Instance.BPMEvent == 1 && level < 5 && !sEvent1)   
        {
            sEvent1 = true; // 이벤트 무한 반복 방지
            RenderSettings.skybox = classRoomskyChange;   // 스카이박스 변경
            StartCoroutine(ChangeColor());                 // 일정 시간 후에 스카이박스 복원하는 코루틴 실행
            ActiveBlood();                                 // 피 이펙트 활성화
            sizeUpBlood.transform.localScale *= 1.2f;      // 피 이펙트 크기 확대
            level++;                                       // 다음 레벨로 업데이트
            StartCoroutine(ChangeTFforEvent1()); // 이벤트 1 종료까지 대기하는 코루틴 실행
        }
    }

    void ActiveBlood()
    {
        int temp = cnt;
        while(cnt == temp) // random한 x값이 이전과 중복되지 않은 값을 찾을 때까지 반복
        {
            int x = Random.Range(0, blood.Length);
            if (!isFilled[x]) // setActive 되지 않은 blood object이면 실행
            {
                blood[x].SetActive(true);
                isFilled[x] = true;
                cnt++;
            }
        }
    }

    IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(10);               // 10초 대기
        RenderSettings.skybox = classRoomsky;               // 기본 스카이박스로 복원
    }

    void BloodOn()
    {
        sizeUpBlood.SetActive(true);                        // 크기 확대 피 이펙트 활성화
    }
    IEnumerator ChangeTFforEvent1()
    {
        yield return new WaitForSeconds(15);   // 15초 대기
        sEvent1 = false;   // 이벤트 1 종료
    }
}
