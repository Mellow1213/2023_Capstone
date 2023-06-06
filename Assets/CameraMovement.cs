using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 200.0f;

    private float yaw;
    private float pitch;

    void Start()
    {
        // 마우스 커서를 숨기고 잠금 상태로 만듭니다.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // WASD 키 입력을 가져와서 이동 방향 벡터 계산
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(xInput, 0f, zInput).normalized;

        // 이동 방향 벡터를 카메라의 로컬 공간으로 변환
        Vector3 relativeMovement = transform.TransformDirection(movement);

        // 이동 벡터에 이동 속도와 시간 간격을 곱하여 이동 거리 계산 및 적용
        transform.position += relativeMovement * moveSpeed * Time.deltaTime;

        // 마우스 입력을 가져와 카메라 회전
        yaw += Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;

        // 상하 회전 범위를 -90 ~ 90도로 제한
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        // 카메라 회전 적용
        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }
}