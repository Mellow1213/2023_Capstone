using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 _input;

    public float speed = 1;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 direction =
            Player.instance.hmdTransform.TransformDirection(new Vector3(_input.axis.x, 0, _input.axis.y));
        transform.position += speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) ;
    }
}
