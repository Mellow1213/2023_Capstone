using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DownMannequin : MonoBehaviour
{
    private GameObject playerTarget;// 마네킹이 추적할 대상
    public GameObject memo;// 메모 오브젝트
    private FindMemo FM;// 메모 오브젝트

    private Vector3 ptPos = Vector3.zero; // 플레이어의 위치를 저장할 변수
    private bool separate = false; // 분리 가능 여부 확인 변수

    //safe door open
    public GameObject safe;// 금고 문 오브젝트

    private bool sEvent1 = false; // 이벤트 1 발생 여부 확인 변수
    private bool sEvent2 = false; // 이벤트 2 발생 여부 확인 변수


    // Start is called before the first frame update
    void Start()
    {
        FM = memo.GetComponent<FindMemo>();// FindMemo 스크립트 참조
        playerTarget = GameObject.FindWithTag("Player");//태그를 이용하여 플레이어 참조
    }

    // Update is called once per frame
    void Update()
    {
        // 마네킹이 플레이어를 바라보도록 회전
        ptPos = new Vector3(playerTarget.transform.position.x, transform.position.y, playerTarget.transform.position.z);
        transform.LookAt(ptPos);


        if(FTU.Instance.BPMEvent == 1 && !sEvent1)//BPM Level (Singleton Instance) 
        {
            Mannequin(1);
            StartCoroutine(changeTFforEvent1());
        }
        else if (FTU.Instance.BPMEvent == 2 && !sEvent2)
        {
            Mannequin(1.5f);
            StartCoroutine(changeTFforEvent2());
        }

        if (separate || FM.isFind)// 마네킹이 특정 위치까지 도달했거나 또는 메모를 찾은 경우 마네킹 분해 쇼
        {
            for (int i = 0; i < transform.childCount; i++)//부모 오브젝트에서 분리되며 중력과 isTrigger비활성화로 마네킹이 다향한 방향으로 분해됨
            {
                this.transform.GetChild(i).GetComponent<Rigidbody>().useGravity = true;
                this.transform.GetChild(i).GetComponent<Collider>().isTrigger = false;
                this.transform.GetChild(i).transform.SetParent(null);
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Down"))//down이라는 오브젝트에 닿았을 시 마네킹은 분해가 될 조건 충족
        {
            separate = true;
        }
    }

    void Mannequin(float Level)//마네킹이 심박수 level단계에 맞게 천장에서 아래로 내려감
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - (3f * Level), this.transform.position.z);
    }

    IEnumerator changeTFforEvent1()
    {
        yield return new WaitForSeconds(15);//15초 이후 event1 실행 가능
        sEvent1 = false;
    }
    IEnumerator changeTFforEvent2()
    {
        yield return new WaitForSeconds(30);//30초 이후 event2 실행 가능
        sEvent2 = false;
    }
}
