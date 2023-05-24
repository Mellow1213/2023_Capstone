using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileDrop : MonoBehaviour
{
    private GameObject player;
    private Vector3 TargetPos;
    private bool sEvent2 = false;
    public GameObject tile;
    private Queue<GameObject> queue = new Queue<GameObject>();

    private Vector3 pos;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        TargetPos = new Vector3(
            player.transform.position.x - 3,
            player.transform.position.y + 4,
            player.transform.position.z
            );
        this.transform.position = TargetPos;

        if (FTU.Instance.BPMEvent == 2 && !sEvent2)//어떠한 심박수에 도달한다면 FTU.Instance.BPMEvent >= 2
        {
            Debug.Log("work");
         
            sEvent2 = true;
            StartCoroutine(changeTFforEvent2());
        }
    }

    IEnumerator changeTFforEvent2()
    {
        for (int i = 0; i < 7; i++)
        {
            int a = Random.Range(-2, 3);
            pos = new Vector3(transform.position.x - (a * 0.8f), transform.position.y, transform.position.z + a);
            queue.Enqueue(Instantiate(tile, pos, Quaternion.identity));
        }
        yield return new WaitForSeconds(3);
        for (int i = 0; i < 7; i++)
        {
            Destroy(queue.Dequeue());
        }
        yield return new WaitForSeconds(10);
        sEvent2 = false;
    }
}
