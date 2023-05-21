using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 컬러룸 철장 열리는 스크립트인데 변경 예정

public class DoorCtrl : MonoBehaviour
{
    private string obj_name;            // 몇 번 철장인지 확인 위함
    private float pre_position_y;       // 철장 닫혀있을 때 y 좌표 저장할 변수

    // Start is called before the first frame update
    void Start()
    {
        obj_name = transform.name;      // 오브젝트 이름 저장
        pre_position_y = transform.position.y;      // 철장 닫혀있을 때 y 좌표 저장
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0) && transform.position.y == pre_position_y && obj_name.Equals("Cage1")) // 왼쪽 마우스 클릭 시, y좌표가 철장 닫혀있을 때 y 좌표와 같으면(= 철장 닫혀있음), 오브젝트 이름이 Cage1 일 때
        {
            // 철장의 y좌표가 30초 동안 + 4만큼 천천히 올라가다가 점점 빠르게 올라감 -> 철장 올라가는 애니메이션
            transform.DOMove(new Vector3(transform.position.x, transform.position.y + 4.0f, transform.position.z), 20.0f, false).SetEase(Ease.InQuad);

        }

        if (Input.GetMouseButton(1) && transform.position.y == pre_position_y && obj_name.Equals("Cage2")) // 왼쪽 마우스 클릭 시, y좌표가 철장 닫혀있을 때 y 좌표와 같으면(= 철장 닫혀있음), 오브젝트 이름이 Cage2 일 때
        {
            // 철장의 y좌표가 30초 동안 + 4만큼 천천히 올라가다가 점점 빠르게 올라감 -> 철장 올라가는 애니메이션
            transform.DOMove(new Vector3(transform.position.x, transform.position.y + 4.0f, transform.position.z), 20.0f, false).SetEase(Ease.InQuad);

        }

    }
}
