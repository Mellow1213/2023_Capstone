using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tigershark_move : MonoBehaviour
{
    public float speed = 8f; //상어 이동 속도
    public Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // 플레이어를 태그를 이용해 찾음
    }
    void Update()
    {
        Vector3 target = player.position;// 플레이어의 위치를 목표로 설정
        transform.LookAt(target);// 플레이어를 바라봄

        if (inwater.player_inwater)// player가 수중에 있을 경우
        {

            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);// 현재 위치에서 목표 위치로 일정한 속도로 이동

            if (player.transform.position.y <= 250f)
            {

                Destroy(gameObject);
            }

        }

    }
}
