using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEvent : MonoBehaviour
{//���� ������ ������ ���ƴٴϴ� ��� ������� ���� ������ ���� �Ÿ��� �̺�Ʈ. 5�ʰ� ����
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

    // Update is called once per frame
    void Update()
    {

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
    IEnumerator activeMonster(int i)//�� �տ� ���ִ� ����
    {
        monster[i].SetActive(true);
        yield return new WaitForSeconds(6);
        monster[i].SetActive(false);
    }
    IEnumerator doorSoundEvent() {
        yield return new WaitForSeconds(0.8f);
        for (int i = 0; i < 7; i++)
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