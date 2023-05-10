using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class lockerOpen : MonoBehaviour
{
    //사물함 여는 속도에 따라서 소리 사이즈 변경되는 script
    //일정 속도(소리 크기)를 넘으면 경비가 문두들김.
    private bool isTriggered = false;
    private bool isTriggered2 = false;
    private float GaugeTimer;
    
    private float Gauge = 0.3f;

    public GameObject ob;//원래는 hit을 사용해서 닿는 object를 감지했는데, 번거로워서 테스트용으로 움직일 사물함 문을 임의로 설정함.

    public AudioClip doorSound;
    private AudioSource audioSource;

    //Angular Velocity, 블로그 참고
    Quaternion previousRotation; //전 프레임의 로테이션 값
    Vector3 angularVelocity; //각속도를 관리할 변수
    Vector3 speed;

    public bool DoorEventTrue = false;

    public Vector3 GetPedestrianAngularVelocity()//각 속도를 구하는 함수
    {
        Quaternion deltaRotation = ob.transform.rotation * Quaternion.Inverse(previousRotation);

        previousRotation = ob.transform.rotation;

        deltaRotation.ToAngleAxis(out var angle, out var axis);

        //각도에서 라디안으로 변환
        angle *= Mathf.Deg2Rad;

        angularVelocity = (1.0f / Time.deltaTime) * angle * axis;

        //각속도 반환
        return angularVelocity;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        audioSource.clip = doorSound;
        audioSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        isTriggered = Input.GetMouseButton(0);//문 열기 위해서 임시로 사용
        isTriggered2 = Input.GetMouseButton(1);//문을 닫기 위해서 임시로 사용
        //audioSource.Play();

        if (isTriggered || isTriggered2)
        {
            speed = GetPedestrianAngularVelocity();
            Gauge += Time.deltaTime * speed.magnitude; //각속도가 곧 사운드(소음) 크기
            if(Gauge > 0.7)
            {
                //문두들기는 소리, 애니메이션 실행
                DoorEventTrue = true;
                Gauge = 0.3f;
            }
        }
        else
        {
            Gauge = 0.3f;
        }

        if (isTriggered)
        {
            GaugeTimer = -1 * Time.deltaTime * 20;//여기에 힘 곱하기
            audioSource.volume = Gauge;
            if (ob.transform.eulerAngles.y >= 170) ob.transform.Rotate(0, GaugeTimer, 0);
            //Debug.Log("ob.transform.rotation.y = " + ob.transform.rotation.y);
            //Debug.Log("ob.transform.eulerAngles.y = " + ob.transform.eulerAngles.y);
        }
        else if (isTriggered2)
        {
            GaugeTimer = Time.deltaTime * 20;//여기에 힘 곱하기
            ob.transform.Rotate(0, GaugeTimer, 0);
            Debug.Log("Close");
            audioSource.volume = Gauge;
            
            if (ob.transform.eulerAngles.y >= 270f) ob.transform.rotation = Quaternion.Euler(0, 270, 0);
            
            //Debug.Log("ob.transform.eulerAngles.y = " + ob.transform.eulerAngles.y);
        }
        else
        {
            GaugeTimer = 0;
        }
    }
}
