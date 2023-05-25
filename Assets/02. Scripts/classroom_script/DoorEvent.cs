using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEvent : MonoBehaviour
{//일정 소음이 넘으면 돌아다니던 경비가 사라지고 문을 열려고 벌컥 거리는 이벤트. 5초간 진행
    private int num;//문 이벤트에서 사용할 랜덤 숫자를 저장하는 변수
    private Animator anim;//Animator 컴포넌트를 저장하는 변수
    public GameObject[] doors;//문을 나타내는 게임 오브젝트 배열
    public GameObject[] monster;//몬스터를 나타내는 게임 오브젝트 배열

    public AudioClip[] event1;//이벤트 1에 사용할 AudioClip 배열(문 덜커덩 거리는 소리)
    public AudioClip[] event2;//이벤트 2에 사용할 AudioClip 배열(문 두들기는 소리)
    private AudioSource m_Source;//이벤트 1에 사용할 AudioClip 배열

    public static DoorEvent instance;//이벤트 1에 사용할 AudioClip 배열

    // Start is called before the first frame update
    void Start()// 초기 설정을 수행
    {
        m_Source = GetComponent<AudioSource>();
        DoorEvent.instance = this;
    }

    public void DoorEventFunc()//랜덤한 숫자를 생성하고 해당 숫자에 따라 문과 몬스터를 제어
    {
        num = Random.Range(1, 4);
        StartCoroutine(doorSoundEvent());
        switch (num % 2)
        {
            case 0:
                anim = doors[0].GetComponent<Animator>();
                anim.SetTrigger("doorLock");
                //Debug.Log("left door");
                StartCoroutine(activeMonster(0));
                break;
            case 1:
                anim = doors[1].GetComponent<Animator>();
                anim.SetTrigger("doorLock");
                //Debug.Log("right door");
                StartCoroutine(activeMonster(1));
                break;
        }
    }
    IEnumerator activeMonster(int i)//문 앞에 위치한 몬스터를 활성화하고 일정 시간 후에 비활성화
    {
        monster[i].SetActive(true);
        yield return new WaitForSeconds(6);
        monster[i].SetActive(false);
    }
    IEnumerator doorSoundEvent()
    { // 일정 시간 간격으로 이벤트에 사용할 사운드를 재생
        for (int i = 0; i < 6; i++)
        {
            int a = Random.Range(0, 4);
            yield return new WaitForSeconds(0.9f);
            m_Source.clip = event1[a];
            m_Source.PlayOneShot(m_Source.clip);
            m_Source.clip = event2[a];
            m_Source.PlayOneShot(m_Source.clip);
        }
    }
}