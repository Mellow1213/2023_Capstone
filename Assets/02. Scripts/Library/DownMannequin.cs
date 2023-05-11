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
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //player �������� ����ŷ ȸ��
        ptPos = new Vector3(playerTarget.transform.position.x, transform.position.y, playerTarget.transform.position.z);
        transform.LookAt(ptPos);

        //�ɹڼ��� ���� ����ŷ�� �Ʒ��� ������
        //if(heartbeat > default + 15)//heartbeat�� �ǽð� �ɹڼ�, default�� ���, ���̳� ����ʸ��� ����� ����ϴ� �͵� ������ ������.
        if(Input.GetMouseButtonDown(0)) 
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - (3*Time.deltaTime), this.transform.position.z);

        if (separate)
        {
            StartCoroutine(seprtemannequin());

            for (int i = 0; i < transform.childCount; i++)
            {
                this.transform.GetChild(i).GetComponent<Rigidbody>().useGravity = true;
                this.transform.GetChild(i).GetComponent<Collider>().isTrigger = false;
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        //���� ���̿� �����ϸ� ����ŷ ���� ����
        if (other.gameObject.CompareTag("Down"))
        {
            separate = true; ;
        }
    }
    IEnumerator seprtemannequin()
    {
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < transform.childCount; i++)
        {
            this.transform.GetChild(i).GetComponent<Collider>().isTrigger = true;
            this.transform.GetChild(i).transform.SetParent(null);
        }
        yield return new WaitForSeconds(0.5f);
        DestroyMannequin = true;
    }
}
