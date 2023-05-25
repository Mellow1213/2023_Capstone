using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drop_sound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip clips;// 물건이 떨어질 때 재생할 오디오 클립들

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))//바닥과 닿았을 시 지정해둔 음원 실행
        {
            audioSource.clip = clips;
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}
