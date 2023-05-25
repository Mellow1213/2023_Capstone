using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOff : MonoBehaviour
{//빈 오브젝트를 만들어서 플레이어를 따라다니게 하기
    private GameObject player;       // 플레이어 GameObject를 저장할 변수
    private Vector3 TargetPos;       // 이동할 위치를 저장할 변수

    private void Start()
    {
        player = GameObject.FindWithTag("Player");  // "Player" 태그를 가진 GameObject를 찾아서 변수에 저장
    }

    private void FixedUpdate()
    {
        // 플레이어의 위치를 기준으로 이동할 위치 계산
        TargetPos = new Vector3(
            player.transform.position.x + 1,
            player.transform.position.y + 2,
            player.transform.position.z
        );

        // 현재 GameObject의 위치를 이동할 위치로 설정
        this.transform.position = TargetPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Light"))  // "Light" 태그를 가진 객체와 충돌했을 경우
        {
            Destroy(other.gameObject);  // 충돌한 객체를 파괴하여 불을 끔
        }
        else if (other.gameObject.CompareTag("Light2"))  // "Light2" 태그를 가진 객체와 충돌했을 경우
        {
            Transform t = other.gameObject.GetComponentInChildren<Transform>(true);
            t.GetChild(0).gameObject.SetActive(true);  // 충돌한 객체의 자식 중 첫 번째 GameObject를 활성화하여 불을 켬
        }
    }

}