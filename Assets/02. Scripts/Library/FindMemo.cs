using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindMemo : MonoBehaviour
{
    public GameObject safe;
    public Text display;
    private Animator ani;
    public bool isFind = false;
    // Start is called before the first frame update
    void Start()
    {
        ani = safe.GetComponent<Animator>();
    }

    public void FindHintEvent()//memo를 grab
    {
        ani.SetTrigger("openDoor");
        display.text = "5214";
        isFind = true;
    }
}
