using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class DoorRotate_Temp : MonoBehaviour
{
    public GameObject target; // 회전할 대상 객체

    private Vector3 T; // 회전 대상 위치
   
    // Update is called once per frame
    void Update()
    {
        var position = target.transform.position; // 대상 객체의 위치 가져오기
        T = new Vector3(position.x, transform.position.y, position.z); // 대상 객체의 y 좌표를 현재 객체의 y 좌표로 맞추고 회전 대상 위치 설정
        
        transform.LookAt(T); // 현재 객체가 회전 대상을 향하도록 설정
    }
}