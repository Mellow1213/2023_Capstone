using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class lockerRotation : MonoBehaviour
{
    private float Gauge = 0.3f;

    private GameObject ob;//원래는 hit을 사용해서 닿는 object를 감지했는데, 번거로워서 테스트용으로 움직일 사물함 문을 임의로 설정함.

    //private OVRPlayerCtrl playerCtrl;
    public GameObject player;

    public AudioClip doorSound;
    private AudioSource audioSource;

    //Angular Velocity, 블로그 참고
    Quaternion previousRotation; //전 프레임의 로테이션 값
    Vector3 angularVelocity; //각속도를 관리할 변수
    Vector3 speed;

    public bool DoorEventTrue = false;

    private bool opening = false;

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
        //playerCtrl = player.GetComponent<OVRPlayerCtrl>();
        audioSource = this.GetComponent<AudioSource>();
        audioSource.clip = doorSound;
        audioSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        //audioSource.Play();

        if (opening)
        {
            speed = GetPedestrianAngularVelocity();
            Debug.Log("speed : " + speed);
            Gauge += Time.deltaTime * speed.magnitude; //각속도가 곧 사운드(소음) 크기
            audioSource.volume = Gauge;

            if (Gauge > 0.7)
            {
                //문두들기는 소리, 애니메이션 실행
                DoorEventTrue = true;
                Gauge = 0.3f;
                Debug.Log("good");
            }
        }
        else
        {
            Gauge = 0.3f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("handle"))//if (other.gameObject.CompareTag("handle") && playerCtrl.down)//조이스틱을 잡고 있을 때
        {
            Debug.Log("touch");
            opening = true;
            ob = other.transform.parent.gameObject;
        }
        else
        {
            opening = false;
        }
    }
}
