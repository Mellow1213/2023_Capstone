using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 철장 문 여는 스크립트

public class KeyCage : MonoBehaviour
{
    public GameObject cage;         // 철장 오브젝트 저장할 변수
    private float pre_position_y;   // 철장 닫혀있을 때 y좌표 저장할 변수

    // Start is called before the first frame update
    void Start()
    {
        pre_position_y = cage.transform.position.y;     // 철장 닫혀있을 때 y좌표 저장
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cage") && cage.transform.position.y == pre_position_y)
        {
            // 철장의 y좌표가 30초 동안 + 4만큼 천천히 올라가다가 점점 빠르게 올라감 -> 철장 올라가는 애니메이션
            cage.transform.DOMove(new Vector3(cage.transform.position.x, cage.transform.position.y + 4.0f, cage.transform.position.z), 20.0f, false).SetEase(Ease.InQuad);
        }
    }
}
