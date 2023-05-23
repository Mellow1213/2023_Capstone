using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 컬러룸 감옥 문 컨트롤 스크립트

public class DoorCtrl : MonoBehaviour
{
    private bool door_open;
    public GameObject door;               // 문 오브젝트

    private AudioSource card_audio;       // 카드 찍을 때 효과음
    private AudioSource door_audio;       // 문 열릴 때 효과음

    // Start is called before the first frame update
    void Start()
    {
        door_open = false;
        card_audio = GetComponent<AudioSource>();            // 카드 오디오 소스 저장
        door_audio = door.GetComponent<AudioSource>();       // 문 오디오 소스 저장
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (transform.name.Equals("Key_BlueRoom") && other.gameObject.CompareTag("BlueRoom"))   // 키가 파란색이고 키패드의 태그가 파란방이면 문 열 수 있음
        {
            card_audio.Play();       // 카드 찍을 때 효과음 재생(삑-)
            StartCoroutine(DoorOpen());
            door_open = true;
            // 당겨서 문 여는 코드 필요
        } else if(transform.name.Equals("Key_RedRoom") && other.gameObject.CompareTag("RedRoom"))   // 키가 빨간색이고 키패드의 태그가 빨간방이면 문 열 수 있음
        {
            card_audio.Play();       // 카드 찍을 때 효과음 재생(삑-)
            StartCoroutine(DoorOpen());
            door_open = true;
            // 당겨서 문 여는 코드 필요
        }
        else if (transform.name.Equals("Key_GreenRoom") && other.gameObject.CompareTag("YellowRoom"))    // 키가 초록색이고 키패드의 태그가 노란방이면 문 열 수 있음
        {
            card_audio.Play();       // 카드 찍을 때 효과음 재생(삑-)
            StartCoroutine(DoorOpen());
            door_open = true;
            // 당겨서 문 여는 코드 필요
        }
    }

    IEnumerator DoorOpen()
    {
        yield return new WaitForSeconds(1.0f);

        door_audio.Play();
    }
}
