using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Security : MonoBehaviour
{
    //경비가 돌아다니는 기능
    private int i = 0; //보안 관리자의 순찰 경로 인덱스를 나타내는 변수
    public GameObject[] patrols; // 보안 관리자의 순찰 경로를 나타내는 게임 오브젝트

    public GameObject player;//플레이어를 나타내는 게임 오브젝트
    private float dis;//보안 관리자와 플레이어 사이의 거리를 저장하는 변수

    public AudioClip securityFootstep;//보안 관리자의 발소리를 담을 AudioClip 변수
    private AudioSource m_Source;//안 관리자의 AudioSource 컴포넌트를 저장하는 변수

    public static Security instance;//Security 클래스의 인스턴스를 저장하는 정적 변수

    // Start is called before the first frame update
    void Start()//초기 설정을 수행
    {
        m_Source = GetComponent<AudioSource>();
        m_Source.clip = securityFootstep;
        m_Source.Play();
        m_Source.loop = true;
        m_Source.volume = 0.4f;
        Security.instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        playerDistanceSound();
        if (m_Source.volume != 0)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, patrols[i % 2].transform.position, Time.deltaTime * 2);
        }
    }

    private void playerDistanceSound()//플레이어와 보안 관리자 사이의 거리에 따라 보안 관리자의 소리 크기를 조절
    {
        dis = Vector3.Distance(patrols[i % 2].transform.position, this.gameObject.transform.position);
        //Debug.Log(dis);
        if (dis < 15)
            m_Source.volume -= 0.03f * Time.deltaTime;
        else
            m_Source.volume += 0.03f * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)//보안 관리자가 도착 지점에 도달하면 회전을 수행하고 순찰 경로 인덱스를 증가
    {
        if(other.gameObject.CompareTag("end")) {
            this.transform.Rotate(0, 180, 0);
            i++;
        }
    }
    public IEnumerator activeSecuity()//일정 소음이 넘으면 불빛(손전등 역할)이 잠시 꺼짐
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        m_Source.volume = 0;
        yield return new WaitForSeconds(6);
        this.transform.GetChild(0).gameObject.SetActive(true);
        m_Source.volume = 0.4f;
    }
}
