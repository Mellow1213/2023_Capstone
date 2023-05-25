using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerOpenSound : MonoBehaviour
{//AudioSource component를 하나만 사용하기 위해 만들어진 스크립트
    public AudioClip lockerSound; //사물함 열림 소리를 담을 AudioClip 변수
    private AudioSource audioSource;//AudioSource 컴포넌트를 담을 변수

    public static LockerOpenSound instance;//다른 스크립트에서 LockerOpenSound 인스턴스에 접근하기 위한 정적 변수

    // Start is called before the first frame update
    void Start()
    {
        LockerOpenSound.instance = this;
        audioSource = this.GetComponent<AudioSource>(); //AudioSource 컴포넌트를 초기화
        audioSource.clip = lockerSound;//lockerSound AudioClip을 할당합니다.
        audioSource.volume = 0.9f;
    }

    // Update is called once per frame
    public void PlayOpenSound()
    {
        audioSource.Play();//AudioSource의 Play() 함수를 호출하여 사물함 열림 소리를 재생합니다.
    }
}
