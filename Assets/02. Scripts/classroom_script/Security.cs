using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Security : MonoBehaviour
{
    //��� ���ƴٴϴ� ���
    private int i = 0;
    public GameObject[] patrols;

    public GameObject player;
    private lockerOpen LO;
    private float dis;

    public AudioClip securityFootstep;
    private AudioSource m_Source;
    // Start is called before the first frame update
    void Start()
    {
        LO = player.GetComponent<lockerOpen>();
        m_Source = GetComponent<AudioSource>();
        m_Source.clip = securityFootstep;
    }

    // Update is called once per frame
    void Update()
    {
        playerDistanceSound();
        if (LO.DoorEventTrue)
        {
            StartCoroutine(activeSecuity());
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, patrols[i%2].transform.position, Time.deltaTime * 2);
        }
    }

    private void playerDistanceSound()
    {
        dis = Vector3.Distance(player.transform.position, this.gameObject.transform.position);
        m_Source.Play();
        m_Source.loop = true;
        if (dis > 20)
            m_Source.volume -= 0.07f;
        else
            m_Source.volume += 0.07f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("end")) {
            this.transform.Rotate(0, 180, 0);
            i++;
        }
    }
    IEnumerator activeSecuity()//���� ������ ������ �Һ�(������ ����)�� ��� ����
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        m_Source.Stop();
        yield return new WaitForSeconds(6);
        this.transform.GetChild(0).gameObject.SetActive(true);
        m_Source.Play();
    }
}
