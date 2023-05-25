using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileDrop : MonoBehaviour
{
    private GameObject player;           // 플레이어 GameObject를 저장할 변수
    private Vector3 TargetPos;           // 이동할 위치를 저장할 변수
    private bool sEvent2 = false;        // 이벤트 2 진행 여부를 확인하는 변수
    public GameObject tile;              // 타일 GameObject 프리팹
    private Queue<GameObject> queue = new Queue<GameObject>();   // 타일을 순서대로 저장할 큐
    private Vector3 pos;                 // 타일 생성 위치를 저장할 변수

    private void Start()
    {
        player = GameObject.FindWithTag("Player");   // "Player" 태그를 가진 GameObject를 찾아서 변수에 저장
    }

    void Update()
    {        
        // 플레이어의 위치를 기준으로 이동할 위치 계산
        TargetPos = new Vector3(
            player.transform.position.x - 3,
            player.transform.position.y + 4,
            player.transform.position.z
        );
        this.transform.position = TargetPos;

        if (FTU.Instance.BPMEvent == 2 && !sEvent2)   // FTU.Instance.BPMEvent 값이 2 이상이고, 이벤트 2가 진행 중이 아닌 경우
        {
            //Debug.Log("work");
            sEvent2 = true;
            StartCoroutine(ChangeTFforEvent2());   // 이벤트 2를 진행하는 코루틴 실행
        }
    }

    IEnumerator ChangeTFforEvent2()
    {
        for (int i = 0; i < 7; i++)
        {
            int a = Random.Range(-2, 3);
            pos = new Vector3(transform.position.x - (a * 0.8f), transform.position.y, transform.position.z + a); // 타일이 발생할 랜덤한 위치 지정
            queue.Enqueue(Instantiate(tile, pos, Quaternion.identity));   // 타일을 생성하여 큐에 저장
        }

        yield return new WaitForSeconds(3);

        for (int i = 0; i < 7; i++)
        {
            Destroy(queue.Dequeue());   // 큐에서 순서대로 타일을 제거
        }

        yield return new WaitForSeconds(10);

        sEvent2 = false;   // 이벤트 2 종료
    }

}
