using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 철장 문 여는 스크립트

public class KeyCage : MonoBehaviour
{
    public GameObject cage;         // 철장 오브젝트 저장할 변수
    private float pre_position_y;   // 철장 닫혀있을 때 y좌표 저장할 변수

    private AudioSource card_audio;       // 카드 찍을 때 효과음
    private AudioSource cage_audio;        // 철장 열릴 때 효과음

    // Start is called before the first frame update
    void Start()
    {
        pre_position_y = cage.transform.position.y;     // 철장 닫혀있을 때 y좌표 저장

        card_audio = GetComponent<AudioSource>();            // 카드 오디오 소스 저장
        cage_audio = cage.GetComponent<AudioSource>();       // 철장 오디오 소스 저장
    }

    // Update is called once per frame
    void Update()
    {
        if (cage.transform.position.y == pre_position_y + 4.0f)
        {
            cage_audio.Stop();
            StopCoroutine(CageOpen());

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cage") && cage.transform.position.y == pre_position_y)
        { 
         // 철장의 y좌표가 30초 동안 + 4만큼 천천히 올라가다가 점점 빠르게 올라감 -> 철장 올라가는 애니메이션
            cage.transform.DOMove(new Vector3(cage.transform.position.x, cage.transform.position.y + 4.0f, cage.transform.position.z), 20.0f, false).SetEase(Ease.InQuad);

            card_audio.Play();       // 카드 찍을 때 효과음 재생(삑-)

            StartCoroutine(CageOpen());

        } 
    }

    IEnumerator CageOpen()
    {
        yield return new WaitForSeconds(1.0f);

        cage_audio.Play();
    }
}
