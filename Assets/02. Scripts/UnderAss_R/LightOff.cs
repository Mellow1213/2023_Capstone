using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOff : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Light"))//�Ҳ���
        {
            Destroy(other.gameObject);
        }else if (other.gameObject.CompareTag("Light2"))//���� ������ ����
        {
            Transform t = other.gameObject.GetComponentInChildren<Transform>(true);
            t.GetChild(0).gameObject.SetActive(true);
        }
    }

}