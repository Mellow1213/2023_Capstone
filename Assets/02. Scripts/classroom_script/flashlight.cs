using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashlight : MonoBehaviour
{
    private Animator anim;
    private float sound = 0;//�÷��̾ �繰�� ���� �Ҹ�
    
    bool z = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        z = Input.GetKey(KeyCode.P);
        if (z)//sound > 30
        {
            anim.SetBool("player", true);
            Debug.Log("player");
        }
        else
        {
            anim.SetBool("player", false);

        }
    }

}
