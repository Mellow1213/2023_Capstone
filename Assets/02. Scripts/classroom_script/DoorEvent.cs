using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEvent : MonoBehaviour
{//���� ������ ������ ���ƴٴϴ� ��� ������� ���� ������ ���� �Ÿ��� �̺�Ʈ. 5�ʰ� ����
    private int num;
    private Animator anim;
    public GameObject[] doors;
    public GameObject[] monster;

    public GameObject player;
    private lockerOpen LO;

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    // Start is called before the first frame update
    void Start()
    {
        LO = player.GetComponent<lockerOpen>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (LO.DoorEventTrue)//���� ������ ������ ���� �ޱ׶� �Ÿ��� �̺�Ʈ
        {
            num = Random.Range(1, 4);
            Debug.Log(num);
            LO.DoorEventTrue = false;
            switch (num % 2)
            {
                case 0:
                    anim = doors[0].GetComponent<Animator>();
                    anim.SetTrigger("doorLock");
                    Debug.Log("lest door");
                    StartCoroutine(activeMonster(0));
                    break;
                case 1:
                    anim = doors[1].GetComponent<Animator>();
                    anim.SetTrigger("doorLock");
                    Debug.Log("right door");
                    StartCoroutine(activeMonster(1));
                    break;
            }
        }
    }
    IEnumerator activeMonster(int i)//�� �տ� ���ִ� ����
    {
        monster[i].SetActive(true);
        yield return new WaitForSeconds(6);
        monster[i].SetActive(false);
    }
}