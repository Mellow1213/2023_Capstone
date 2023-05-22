using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whale_move : MonoBehaviour
{
    public float speed; // rhfo 
    public float rotSpeed = 0.2f;
    Animator animator;
    float animSpeed;
    float animSpeedVariance;
    public float animationSpeed;
 
    public Transform[] targets;     // 이동할 좌표 배열
    private float gotime = 0.0f;
    private int currentTarget = 0;  // 현재 이동할 좌표 인덱스

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animSpeedVariance = Random.Range(0f, 0.5f);
        animSpeed = animationSpeed;
        animator.speed = animSpeed + animSpeedVariance;

        targets = new Transform[5];
        targets[0] = new GameObject().transform;
        targets[0].position = new Vector3(272f, 112f, 241f);
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
       
        if (gotime >= 30.0f)
        {
            if (currentTarget <= 2)
            {
                currentTarget++;
                gotime = 0.0f;
            }
        }
        if(currentTarget == 5)
        {
            currentTarget = 0;
            Debug.Log(currentTarget);
            gotime = 0.0f;
        }

        Vector3 targetPosition = targets[currentTarget].position;
        Vector3 direction = targetPosition  - this.transform.position;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        
    }
}
