using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class footStep : MonoBehaviour
{
    private Vector3 previousPosition;
    private Vector3 currentPosition;
    private double velocity;

    public AudioClip[] footClip;
    private AudioSource footSource;
    int x, y;

    void Start()
    {
        footSource = GetComponent<AudioSource>();
        previousPosition = transform.position; //초기화      
    }

    void Update()
    {
        //이동여부 판단
        currentPosition = transform.position; //매 프레임마다 currentPosition에 현재 Position을 초기화 해준다
        var distance = Vector3.Distance(previousPosition, currentPosition);
        velocity = distance / Time.deltaTime; //거 속 시 를 사용해 속력구하기
        previousPosition = currentPosition;

        if(velocity >= 14)
        {
            StartCoroutine(stepSound());
        }
        else
        {
            footSource.Stop();
        }

    }

    IEnumerator stepSound() {
        x = Random.Range(0, footClip.Length);
        y = Random.Range(0, footClip.Length);
        footSource.clip = footClip[x];
        footSource.Play();
        yield return new WaitForSeconds(1.5f);
        footSource.clip = footClip[y];
        footSource.Play();
    }
}