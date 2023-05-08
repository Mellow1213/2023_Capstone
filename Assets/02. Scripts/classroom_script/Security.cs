using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Security : MonoBehaviour
{
    private bool isMoving = false;
    private int i = 0;
    public GameObject[] patrols;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {

        }
        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, patrols[i%2].transform.position, Time.deltaTime * 2);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("end")) {
            this.transform.Rotate(0, 180, 0);
            i++;
        }
    }
}
