using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyBook : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //상자에 책을 넣으면 엘리베이터 문이 열림
        if (other.gameObject.CompareTag("Finish"))
        {
            //엘리베이터 문이 열림
        }
    }
}
