using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class lockerRotation : MonoBehaviour
{
    //Angular Velocity
    Quaternion previousRotation;
    Vector3 angularVelocity;
    Vector3 speed;

    private float Gauge = 0.0f; //기본 게이지
    private float adjustmentValue = 0.007f; //게이지 조정 값
    private Quaternion pastRotation, currentRotation; //이전회전값, 현재회전값

    public Vector3 GetPedestrianAngularVelocity()//Angular Velocity 블로그에서 받아옴
    {
        Quaternion deltaRotation = this.transform.rotation * Quaternion.Inverse(previousRotation);

        previousRotation = this.transform.rotation;

        deltaRotation.ToAngleAxis(out var angle, out var axis);

        angle *= Mathf.Deg2Rad;

        angularVelocity = (1.0f / Time.deltaTime) * angle * axis;

        return angularVelocity;
    }
    // Start is called before the first frame update
    void Start()
    {
        pastRotation = this.transform.rotation; //초기 상태의 회전값 저장
    }

    // Update is called once per frame
    void Update()
    {
        currentRotation = this.transform.rotation; //움직이는 회전값 저장
        if(currentRotation != pastRotation)
        {
            StartCoroutine(curRotation());
        }
        else
        {
            Gauge = 0.0f; //움직이지 않았다면 게이지 0
        }
    }

    IEnumerator curRotation()
    {
        speed = GetPedestrianAngularVelocity();
        Gauge += adjustmentValue * speed.magnitude; //Gauge를 조절하려면 앞에 
        //Debug.Log("Gauge : " + Gauge); //Gauge값 출력
        if (Gauge > 0.6f)
        {
            LockerOpenSound.instance.PlayOpenSound();//발생 시킬 이벤트
            DoorEvent.instance.DoorEventFunc();//발생 시킬 이벤트
            StartCoroutine(Security.instance.activeSecuity());//발생 시킬 이벤트
            Gauge = 0.0f; // 다시 게이지 0으로 변경
            //Debug.Log("run_A"); //실행 확인
        }

        yield return new WaitForSeconds(2); //2초 뒤 속도 조정, 연속적으로 Gauge값이 차는 것을 방지
        pastRotation = currentRotation;
    }
}