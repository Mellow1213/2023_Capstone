using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOff : MonoBehaviour
{//빈 오브젝트를 만들어서 플레이어를 따라다니게 하기
    private GameObject player;
    private Vector3 TargetPos;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void FixedUpdate()
    {
        TargetPos = new Vector3(
            player.transform.position.x + 1,
            player.transform.position.y + 2,
            player.transform.position.z
            );
        this.transform.position = TargetPos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Light"))//불꺼짐
        {
            Destroy(other.gameObject);
        }else if (other.gameObject.CompareTag("Light2"))//불이 역으로 켜짐
        {
            Transform t = other.gameObject.GetComponentInChildren<Transform>(true);
            t.GetChild(0).gameObject.SetActive(true);
        }
    }
}