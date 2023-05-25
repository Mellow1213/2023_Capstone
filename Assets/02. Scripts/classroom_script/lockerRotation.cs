using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class lockerRotation : MonoBehaviour
{
    private Quaternion previousRotation;// 이전 회전 값을 저장하는 Quaternion 변수
    private float gauge = 0.0f; // 기본 게이지
    public float adjustmentValue = 0.007f; // 게이지 조정 값

    private void Start()
    {
        previousRotation = transform.rotation; // 초기 상태의 회전값 저장
    }

    private void Update()
    {
        Quaternion currentRotation = transform.rotation; // 움직이는 회전값 저장

        if (HasRotationChanged(currentRotation))
        {
            UpdateGauge();
        }
        else
        {
            gauge = 0.0f; // 움직이지 않았다면 게이지 0
        }
    }

    private bool HasRotationChanged(Quaternion currentRotation)
    //Quaternion.Angle 메서드를 사용하여 회전 값의 변경 여부를 판단
    //일정한 각도 이상으로 회전되었는지 확인하여 불필요한 게이지 업데이트를 방지
    {
        return Quaternion.Angle(currentRotation, previousRotation) > 0.01f;
    }

    private void UpdateGauge()
    {
        Vector3 speed = GetAngularVelocity();
        gauge += adjustmentValue * speed.magnitude; //Gauge를 조절하려면 adjustmentValue 조정

        if (gauge > 0.5f)
        {
            TriggerEvents();//일정 게이지 값 이상이 되면 여러 이벤트를 실행
            gauge = 0.0f;
        }

        StartCoroutine(ResetRotation()); //게이지 업데이트 후 회전 값을 재설정하는 코루틴을 시작
    }

    private Vector3 GetAngularVelocity()
    {
        Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(previousRotation);
        previousRotation = transform.rotation;

        deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);
        angle *= Mathf.Deg2Rad;

        return (1.0f / Time.deltaTime) * angle * axis;
    }

    private IEnumerator ResetRotation()
    {
        yield return new WaitForSeconds(2); //회전 값을 재설정
        previousRotation = transform.rotation; //2초 뒤 속도 조정, 연속적으로 Gauge값이 차는 것을 방지
    }

    private void TriggerEvents()
    {
        LockerOpenSound.instance.PlayOpenSound();//발생 시킬 이벤트
        DoorEvent.instance.DoorEventFunc();//발생 시킬 이벤트
        StartCoroutine(Security.instance.activeSecuity());//발생 시킬 이벤트
    }
}
