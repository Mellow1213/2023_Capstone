using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetail : MonoBehaviour
{
    //불 깜박이는 스크립트. 블로그에서 가져옴.

    //코드는 조명의 강도 값을 랜덤하게 변경하며, 목표 강도와 현재 강도 사이의 차이가 0.01 이상인 경우에만 강도를 조정합니다.
    //이를 통해 조명이 부드럽게 변화하고, 강도에 따라 조명의 범위도 함께 업데이트됩니다.
    private Light theLight;          // 조명 컴포넌트를 저장할 변수
    private float targetIntensity;   // 목표 강도 값
    private float currentIntensity;  // 현재 강도 값

    void Start()
    {
        theLight = GetComponent<Light>();           // 조명 컴포넌트를 가져옴
        currentIntensity = theLight.intensity;       // 현재 강도 값을 초기화
        targetIntensity = Random.Range(0.0f, 4.0f);  // 랜덤한 목표 강도 값을 설정
    }

    void Update()
    {
        if (Mathf.Abs(targetIntensity - currentIntensity) >= 0.01)  // 현재 강도와 목표 강도의 차이가 0.01 이상일 경우
        {
            if (targetIntensity - currentIntensity >= 0)  // 목표 강도가 현재 강도보다 큰 경우
                currentIntensity += Time.deltaTime * 10f;  // 강도를 증가시킴
            else
                currentIntensity -= Time.deltaTime * 10f;  // 강도를 감소시킴

            theLight.intensity = currentIntensity;             // 조명의 강도를 업데이트
            theLight.range = currentIntensity + 7;             // 조명의 범위를 강도에 따라 업데이트
        }
        else  // 현재 강도와 목표 강도의 차이가 0.01 미만일 경우
        {
            targetIntensity = Random.Range(0.0f, 4.0f);  // 새로운 랜덤한 목표 강도 값을 설정
        }
    }

}