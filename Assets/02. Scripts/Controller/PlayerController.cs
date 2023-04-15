using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class PlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 _input;

    public float speed = 1;
    
    // Update is called once per frame
    void Update()
    {
        transform.position += speed * Time.deltaTime * new Vector3(_input.axis.x, 0, _input.axis.y);
    }
}
