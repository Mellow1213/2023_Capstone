using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 컬러룸 조명 스크립트

public class LightCtrl : MonoBehaviour
{
    private float spot_targetIntensity;             // spot light의 타겟 intensity
    private float spot_currentIntensity;            // spot light의 현재 intensity

    private float point_currentIntensity;           // point light의 현재 intensity

    private GameObject[] spot_lights, point_lights; // spot light, point light 넣을 gameobject 배열

    // Start is called before the first frame update
    void Start()
    {
        spot_lights = GameObject.FindGameObjectsWithTag("SpotLight");       // 태그가 SpotLight 인 자식들 배열에 넣음
        point_lights = GameObject.FindGameObjectsWithTag("PointLight");     // 태그가 PointLight 인 자식들 배열에 넣음

        spot_currentIntensity = spot_lights[0].GetComponent<Light>().intensity;     // spot light의 현재 intensity 저장
        spot_targetIntensity = Random.Range(0.0f, 50.0f);                           // spot light의 타겟 intensity는 0~50중 랜덤으로 저장


        point_currentIntensity = point_lights[0].GetComponent<Light>().intensity;       // point light의 현재 intensity 저장
    }

    // Update is called once per frame
    void Update()
    {
        // light 깜빡거리는 스크립트 -> 심박수 이벤트 1단계
        if (FTU.Instance.BPMEvent == 1)
        {
            
            if(Mathf.Abs(spot_targetIntensity - spot_currentIntensity) >= 0.01)        // spot light의 타겟 intensity에서 현재 intensity를 뺀 값의 절대값이 0.01보다 크거나 같으면
            {
                if (spot_targetIntensity - spot_currentIntensity >= 0)                 
                {
                    spot_currentIntensity += Time.deltaTime * 50f;                      
                    point_currentIntensity = spot_currentIntensity / 100.0f;            // point light의 intensity는 spot light의 intensity에서 100 나눈 값
                }
                else
                {
                    spot_currentIntensity -= Time.deltaTime * 50f;
                    point_currentIntensity = spot_currentIntensity / 100.0f;            // point light의 intensity는 spot light의 intensity에서 100 나눈 값
                }

                foreach (GameObject light in spot_lights)
                {
                    light.GetComponent<Light>().intensity = spot_currentIntensity;   // spot light의 intensity를 spot_currentIntensity 값으로 변경

                }

                foreach (GameObject light in point_lights)
                {
                    light.GetComponent<Light>().intensity = point_currentIntensity;           // point light의 intensity를  point_currentIntensity 값으로 변경
                }

            }
            else
            {
                spot_targetIntensity = Random.Range(0.0f, 50.0f);                       // spot light의 타겟 intensity는 0~50중 랜덤으로 저장
            }
            
        } // light 어두워지는 스크립트 -> 심박수 이벤트 2단계

        else if (FTU.Instance.BPMEvent == 2)         
        {
            foreach(GameObject light in spot_lights)    
            {
                light.GetComponent<Light>().intensity = 10;   // spot light의 intensity 10으로 변경 -> 어두워짐
            }

            foreach (GameObject light in point_lights)
            {
                light.GetComponent<Light>().intensity = 0.1f;           // point light의 intensity 0.1으로 변경 -> 어두워짐
            }
        }

        
        
    }
}
