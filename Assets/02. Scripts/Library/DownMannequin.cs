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
        //player 방향으로 마네킹 회전
        ptPos = new Vector3(playerTarget.transform.position.x, transform.position.y, playerTarget.transform.position.z);
        transform.LookAt(ptPos);

        //심박수에 따라 마네킹이 아래로 내려옴
        //if(heartbeat > default + 15)//heartbeat은 실시간 심박수, default은 평균, 분이나 몇십초마다 평균을 계산하는 것도 나쁘지 않을듯.
        if(Input.GetMouseButtonDown(0)) 
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - (3*Time.deltaTime), this.transform.position.z);

        if (separate)
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
        //일정 높이에 도달하면 마네킹 공중 분해
        if (other.gameObject.CompareTag("Down"))
        {
            separate = true;
        }
    }
}
