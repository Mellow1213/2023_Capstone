using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DownMannequin : MonoBehaviour
{
    public GameObject playerTarget;
    private Vector3 ptPos = Vector3.zero;
    private bool separate = false;
    public bool DestroyMannequin = false;

    private int Level = 1; //�ɹڼ� ��, 0~1 ���� ���̶�� ����

    private AudioSource audioSource;
    public AudioClip clips;

    //safe door open
    private DoorHandler DH;
    public GameObject safe;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clips;
        DH = safe.AddComponent<DoorHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        //player �������� ����ŷ ȸ��
        ptPos = new Vector3(playerTarget.transform.position.x, transform.position.y, playerTarget.transform.position.z);
        transform.LookAt(ptPos);

        //�ɹڼ��� ���� ����ŷ�� �Ʒ��� ������
        if(FTU.Instance.BPMEvent >= 1)//BPM Level (Singleton Instance) 
        {
            audioSource.PlayOneShot(audioSource.clip);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - (3f * Level), this.transform.position.z);
        }

        if (separate || DH.isOpen)    //safe door open
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                this.transform.GetChild(i).GetComponent<Rigidbody>().useGravity = true;
                this.transform.GetChild(i).GetComponent<Collider>().isTrigger = false;

                this.transform.GetChild(i).transform.SetParent(null);
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        //���� ���̿� �����ϸ� ����ŷ ���� ����
        if (other.gameObject.CompareTag("Down"))
        {
            separate = true;
        }
    }
}
