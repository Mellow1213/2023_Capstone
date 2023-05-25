using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 input; // SteamVR의 Vector2 컨트롤러 액션으로부터 입력 받음

    public float speed = 1; // 플레이어의 이동 속도

    private CharacterController characterController; // CharacterController 컴포넌트에 대한 참조

    private void Start()
    {
        characterController = GetComponent<CharacterController>(); // GameObject에 부착된 CharacterController 컴포넌트 가져오기
    }

    // 매 프레임마다 호출되는 업데이트 함수
    void Update()
    {
        // 플레이어의 머리 회전을 기준으로 VR 컨트롤러 입력에 따른 방향을 가져옴
        Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));

        // 플레이어 캐릭터 컨트롤러를 지정된 방향으로 이동시킴
        characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) - new Vector3(0, 6.5f, 0) * Time.deltaTime);
    }
}
