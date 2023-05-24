using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inwater : MonoBehaviour
{
    public static bool player_inwater = false;
    private Rigidbody player_rb;
    private AudioSource audioSource;
    public AudioClip audioClipsea; //바다 파도 소리
    public AudioClip audioClipbpm;
    public Animation bpmeffect;
    public Transform player;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        player_rb = player.GetComponent<Rigidbody>();
        audioSource.clip = audioClipsea;
        audioSource.Play();
    }

    void Update()
    {

        if (player.position.y <= 387.2f) // water surface에 들어갈 경우
        {

            player_inwater = true; // 플레이어가 물속에 있는 걸로 판단

        }
        else
        {
            player_inwater = false; // 플레이어가 물속에서 나온 걸로 판단
        }

        if (player_inwater)
        {
            player_rb.drag = 0.6f; //palyer가 바다에 들어갈 경우 중력 0.6
            audioSource.clip = audioClipbpm;
            audioSource.loop = false;

            if (Input.GetKeyDown("h")) // 특정 심박수에 다다를 경우
            {
                bpmeffect.Play(); // 화면 깜빡임 애니메이션

                audioSource.Play();
            }

        }


    }

}
