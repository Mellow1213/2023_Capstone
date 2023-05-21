using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ö�� �� ���� ��ũ��Ʈ

public class KeyCage : MonoBehaviour
{
    public GameObject cage;         // ö�� ������Ʈ ������ ����
    private float pre_position_y;   // ö�� �������� �� y��ǥ ������ ����

    // Start is called before the first frame update
    void Start()
    {
        pre_position_y = cage.transform.position.y;     // ö�� �������� �� y��ǥ ����
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cage") && cage.transform.position.y == pre_position_y)
        {
            // ö���� y��ǥ�� 30�� ���� + 4��ŭ õõ�� �ö󰡴ٰ� ���� ������ �ö� -> ö�� �ö󰡴� �ִϸ��̼�
            cage.transform.DOMove(new Vector3(cage.transform.position.x, cage.transform.position.y + 4.0f, cage.transform.position.z), 20.0f, false).SetEase(Ease.InQuad);
        }
    }
}
