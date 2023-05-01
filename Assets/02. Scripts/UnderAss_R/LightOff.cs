using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOff : MonoBehaviour
{
    private GameObject[] lights;//Light must be tagged as "Light"
    public GameObject player;
    private float distance = 0.0f;
    private int i = 0;
    private float dis = 3.1f;

    void Start()
    {
        lights = GameObject.FindGameObjectsWithTag("Light");
    }

    void Update()
    {
        distance = Vector3.Distance(lights[i].transform.position, player.transform.position);//Lighting and player distance (calculated distance based on lighting)
        //Debug.Log(distance);
        Vector3 dir = player.transform.position - lights[i].transform.position;
        //Debug.Log(dir);
        if (distance >= dis && dir.x > -1)//Using dir.x to prevent reverse values
        //*important* If it does not work properly, the starting position of the competitor is far from the dis.
        //The dis value should be calculated directly by looking at the distance between lights.
        {
            lights[i].SetActive(false);//Disable lighting
            if (i < lights.Length - 1)//Add i until less than the light array length value
            {
                i++;
            }
        }
    }
}