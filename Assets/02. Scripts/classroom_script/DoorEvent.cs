using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEvent : MonoBehaviour
{//일정 소음이 넘으면 돌아다니던 경비가 사라지고 문을 열려고 벌컥 거리는 이벤트. 5초간 진행
    private int num;
    private Animator anim;
    public GameObject[] doors;
    public GameObject[] monster;

    public GameObject player;

    public AudioClip[] event1;
    public AudioClip[] event2;
    private AudioSource m_Source;

    public static DoorEvent instance;

    // Start is called before the first frame update
    void Start()
    {
        m_Source = GetComponent<AudioSource>();
        DoorEvent.instance = this;
    }

    public void DoorEventFunc()
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
    IEnumerator activeMonster(int i)//문 앞에 서있는 몬스터
    {
        monster[i].SetActive(true);
        yield return new WaitForSeconds(6);
        monster[i].SetActive(false);
    }
    IEnumerator doorSoundEvent() {
        yield return new WaitForSeconds(0.8f);
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