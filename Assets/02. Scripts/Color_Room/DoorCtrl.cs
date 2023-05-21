using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��� ���� �� ��Ʈ�� ��ũ��Ʈ

public class DoorCtrl : MonoBehaviour
{
    private bool door_open;
    // Start is called before the first frame update
    void Start()
    {
        door_open = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (transform.name.Equals("Key_BlueRoom") && other.gameObject.CompareTag("BlueRoom"))   // Ű�� �Ķ����̰� Ű�е��� �±װ� �Ķ����̸� �� �� �� ����
        {
            door_open = true;
            // ��ܼ� �� ���� �ڵ� �ʿ�
        } else if(transform.name.Equals("Key_RedRoom") && other.gameObject.CompareTag("RedRoom"))   // Ű�� �������̰� Ű�е��� �±װ� �������̸� �� �� �� ����
        {
            door_open = true;
            // ��ܼ� �� ���� �ڵ� �ʿ�
        }
        else if (transform.name.Equals("Key_GreenRoom") && other.gameObject.CompareTag("YellowRoom"))    // Ű�� �ʷϻ��̰� Ű�е��� �±װ� ������̸� �� �� �� ����
        {
            door_open = true;
            // ��ܼ� �� ���� �ڵ� �ʿ�
        }
    }
}
