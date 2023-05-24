using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoor : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
            animator.SetTrigger("Open");
        if(Input.GetKeyDown(KeyCode.C))
            animator.SetTrigger("Close");
        
    }
    
}
