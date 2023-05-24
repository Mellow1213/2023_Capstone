using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 컬러룸 감옥 문 컨트롤 스크립트

public class DoorCtrl : MonoBehaviour
{
    private bool door_open;               // 잠금 해제 표현하는 변수
    public GameObject door;               // 문 오브젝트

    private AudioSource card_audio;       // 카드 찍을 때 효과음
    private AudioSource door_audio;       // 문 열릴 때 효과음

    // Start is called before the first frame update
    void Start()
    {
        door_open = false;              // 문 잠겨있음
        card_audio = GetComponent<AudioSource>();            // 카드 오디오 소스 저장
        door_audio = door.GetComponent<AudioSource>();       // 문 오디오 소스 저장
    }

    // Update is called once per frame
    void Update()
    {
        if(door_open)
        {
            StartCoroutine(DoorOpen());     // 문 열리는 효과음 재생
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (transform.name.Equals("Key_BlueRoom") && other.gameObject.name.Equals("BlueKeypad"))  // 키가 파란색이고 키패드의 태그가 파란방이면 문 열 수 있음
        {
            card_audio.Play();       // 카드 찍을 때 효과음 재생(삑-)
            door_open = true;   // 잠금 해제 표현한 변수 -> 잠금 해제가 되어야 문 열 수 있음
            

        } else if(transform.name.Equals("Key_RedRoom") && other.gameObject.name.Equals("RedKeypad"))   // 키가 빨간색이고 키패드의 태그가 빨간방이면 문 열 수 있음
        {
            card_audio.Play();       // 카드 찍을 때 효과음 재생(삑-)
            door_open = true;   // 잠금 해제 표현한 변수 -> 잠금 해제가 되어야 문 열 수 있음

        }
        else if (transform.name.Equals("Key_YellowRoom") && other.gameObject.name.Equals("YellowKeypad"))    // 키가 초록색이고 키패드의 태그가 노란방이면 문 열 수 있음
        {
            card_audio.Play();       // 카드 찍을 때 효과음 재생(삑-)
            door_open = true;   // 잠금 해제 표현한 변수 -> 잠금 해제가 되어야 문 열 수 있음

        }
    }

    IEnumerator DoorOpen()  // 문 열 때 효과음 
    {
        yield return new WaitForSeconds(1.0f);      // 1초 후에 문 열리는 효과음 재생 & 문 열림

        gameObject.SetActive(false);        // 카드 사용하면 없어짐
        door_audio.Play();      // 문 열리는 효과음 재생
        door.transform.DORotate(new Vector3(-90.0f, 0.0f, 104.0f), 2.5f).SetEase(Ease.InQuad);  // 감옥 문 열리는 애니메이션
        door_open = false;      
    }
}
