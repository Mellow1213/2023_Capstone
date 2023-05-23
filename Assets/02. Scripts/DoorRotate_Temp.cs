using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class DoorRotate_Temp : MonoBehaviour
{
    public GameObject target;

    private Vector3 T;
   
    // Update is called once per frame
    void Update()
    {
        var position = target.transform.position;
        T = new Vector3(position.x, transform.position.y, position.z);
        
        transform.LookAt(T);
    }
}