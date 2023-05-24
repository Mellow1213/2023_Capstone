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

    private AudioSource audioSource;
    public AudioClip clips;

    //safe door open
    public GameObject safe;

    private bool sEvent1 = false;
    private bool sEvent2 = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clips;
    }

    // Update is called once per frame
    void Update()
    {
        //player �������� ����ŷ ȸ��
        ptPos = new Vector3(playerTarget.transform.position.x, transform.position.y, playerTarget.transform.position.z);
        transform.LookAt(ptPos);


        if(FTU.Instance.BPMEvent == 1 && !sEvent1)//BPM Level (Singleton Instance) 
        {
            Mannequin(1);
            StartCoroutine(changeTFforEvent1());
        }
        else if (FTU.Instance.BPMEvent == 2 && !sEvent2)
        {
            Mannequin(1.5f);
            StartCoroutine(changeTFforEvent2());
        }

        if (separate || FindMemo.instance.isFind)//safe door open
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
        if (other.gameObject.CompareTag("Down"))
        {
            separate = true;
        }
    }

    void Mannequin(float Level)
    {
        audioSource.PlayOneShot(audioSource.clip);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - (3f * Level), this.transform.position.z);
    }

    IEnumerator changeTFforEvent1()
    {
        yield return new WaitForSeconds(15);
        sEvent1 = false;
    }
    IEnumerator changeTFforEvent2()
    {
        yield return new WaitForSeconds(30);
        sEvent2 = false;
    }
}
