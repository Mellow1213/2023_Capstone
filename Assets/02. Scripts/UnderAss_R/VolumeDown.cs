using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class VolumeDown : MonoBehaviour
{
    public AudioClip event1;        // 이벤트 1의 오디오 클립
    public AudioClip[] event2;      // 이벤트 2의 오디오 클립 배열
    private AudioSource m_Source;   // 오디오 소스 컴포넌트

    private bool sEvent1 = false;   // 이벤트 1 진행 여부를 확인하는 변수
    private bool sEvent3 = false;   // 이벤트 3 진행 여부를 확인하는 변수

    void Start()
    {
        m_Source = GetComponent<AudioSource>();   // AudioSource 컴포넌트를 가져옴
        m_Source.volume = 0.8f;                    // 초기 볼륨 설정
    }

    void Update()
    {
        if (FTU.Instance.BPMEvent == 1 && !sEvent1)   // FTU.Instance.BPMEvent 값이 1 이고, 이벤트 1이 진행 중이 아닌 경우
        {
            StartCoroutine(Event1Sound());   // Event1Sound 실행
            sEvent1 = true;
            StartCoroutine(ChangeTFforEvent1());   // 이벤트 1 종료까지 대기하는 코루틴 실행
        }
        else if (FTU.Instance.BPMEvent == 3 && !sEvent3)   // FTU.Instance.BPMEvent 값이 3 이고, 이벤트 3이 진행 중이 아닌 경우
        {
            StartCoroutine(Event2Sound());   // Event2Sound 실행
            sEvent3 = true;
            StartCoroutine(ChangeTFforEvent3());   // 이벤트 3 종료까지 대기하는 코루틴 실행
        }
    }

    IEnumerator Event1Sound()
    {
        float sizeDown = 0.01f; // 줄어들 소리 변수
        m_Source.clip = event1;
        m_Source.loop = true; // 소리 반복
        m_Source.Play();

        while (true)// 1초마다 소리가 점점 줄어듦. 소리 볼륨이 0이 되었을 때 반복문을 빠져나옴.
        {
            yield return new WaitForSeconds(0.1f);
            m_Source.volume -= sizeDown;
            if (m_Source.volume <= 0f)
            {
                m_Source.loop = false;
                break;
            }
        }
    }

    IEnumerator Event2Sound()
    {
        int i = Random.Range(0, 2);
        yield return new WaitForSeconds(1);
        m_Source.clip = event2[i]; // 랜덤한 소리 발생
        m_Source.PlayOneShot(m_Source.clip);
    }

    IEnumerator ChangeTFforEvent1()
    {
        yield return new WaitForSeconds(120);   // 120초 대기
        sEvent1 = false;   // 이벤트 1 종료
    }

    IEnumerator ChangeTFforEvent3()
    {
        yield return new WaitForSeconds(120);   // 120초 대기
        sEvent3 = false;   // 이벤트 3 종료
    }
}
