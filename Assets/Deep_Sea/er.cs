using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class er : MonoBehaviour
{
    public float speed;
    public float rotSpeed = 0.2f;

    public Transform[] targets;     // ÀÌµ¿ÇÒ ÁÂÇ¥ ¹è¿­
    public float distance = 10f;   // µµÂø °Å¸®
    private float gotime = 0.0f;
    private int currentTarget = 0;  // ÇöÀç ÀÌµ¿ÇÒ ÁÂÇ¥ ÀÎµ¦½º

    void Start()
    {
 
        targets = new Transform[5];
        targets[0] = new GameObject().transform;
        targets[0].position = new Vector3(156.19f, 7.48f, 102.82f);
        targets[1] = new GameObject().transform;
        targets[1].position = new Vector3(135f, 120f, 370f);
        targets[2] = new GameObject().transform;
        targets[2].position = new Vector3(386f, 110f, 344f);
        targets[3] = new GameObject().transform;
        targets[3].position = new Vector3(360f, 150f, 161f);
        targets[4] = new GameObject().transform;
        targets[4].position = new Vector3(3171f, 180f, 147f);
    }

    void Update()
    {
        gotime += Time.deltaTime;
        speed = Random.Range(5.0f, 8.0f);
        Vector3 vavoid = Vector3.zero;

        if (gotime >= 40.0f)
        {
            if (currentTarget <= 2)
            {
                Debug.Log(currentTarget);
                currentTarget++;
                gotime = 0.0f;
            }
        }
        if (currentTarget == 5)
        {
            currentTarget = 0;
            Debug.Log(currentTarget);
            gotime = 0.0f;
        }

        Vector3 targetPosition = targets[currentTarget].position;
        Vector3 direction = targetPosition + vavoid - this.transform.position;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);


    }
}
