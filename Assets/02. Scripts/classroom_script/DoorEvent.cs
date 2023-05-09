using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEvent : MonoBehaviour
{
    private int num;
    private Animator anim;
    public GameObject[] doors;
    public GameObject player;
    private lockerOpen LO;

    //캐릭터+빨간조명 추가
    // Start is called before the first frame update
    void Start()
    {
        LO = player.GetComponent<lockerOpen>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (LO.DoorEventTrue)
        {
            num = Random.Range(1, 4);
            switch (num % 2)
            {
                case 0:
                    anim = doors[0].GetComponent<Animator>();
                    anim.SetTrigger("doorLock");
                    break;
                case 1:
                    anim = doors[1].GetComponent<Animator>();
                    anim.SetTrigger("doorLock");
                    break;
            }
        }   
    }
}
