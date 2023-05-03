using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ButtonValue : MonoBehaviour
{
    private Vector3 SceenCenter;//Temporary version uses raycast input

    //button Save
    private Queue<int> s = new Queue<int>();//fifo
    string[] textValue = { "1231", "8677", "5555", "2231" };
    // File.ReadAllLines(@"D:\password.txt");//Read with password entered in Notepad
    private GameObject[] buttons;

    //Door
    public GameObject Doors;
    public Text t;

    private KeyCode[] keyCodes = {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9,
    };
void Elevator(int x)
    //If the password you entered is correct, recall the layer corresponding to the password.
    // int x : Information about layers is obtained by obtaining the line length of Notepad in i
    {
        Doors.GetComponent<Animator>().SetTrigger("DoorCloses");

        switch (x)
        {
            case 0:
                t.text = "floor 1";
                Debug.Log("floor 1");
                break;
            case 1:
                t.text = "floor 2";
                Debug.Log("floor 2");
                break;
            case 2:
                t.text = "floor 3";
                Debug.Log("floor 3");
                break;
            case 3:
                t.text = "floor 4";
                Debug.Log("floor 4");
                break;
        }
    }

    void print()
    {
        String emp = "";
        
        while (s.Count > 0)
        {
            emp += s.Dequeue();//Replace the entered value with a string
        }

        for (int i = 0; i < textValue.Length; i++)
        {//Call function if password matches input value; reset input if password does not match
            if (emp.Equals(textValue[i]))
            {
                Elevator(i);
                break;
            }
        }
        s.Clear();
    }
    void Start()
    {
        //Temporary version uses raycast input
        //SceenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
        //Cursor.lockState = CursorLockMode.Locked;

        //Storing gameobjects in an array
        buttons = GameObject.FindGameObjectsWithTag("buttons");

        for (int i = 0; i < textValue.Length; i++)
        {
            Debug.Log(textValue[i]);
        }
    }

    void Update()
    {
        inputValue();
    }
    private void inputValue()//input
    {
        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                buttons[i].GetComponentInChildren<Animator>().SetTrigger("Clicked");
                s.Enqueue(1+i);
            }
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            buttons[10].GetComponentInChildren<Animator>().SetTrigger("Clicked");
            print();
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            buttons[9].GetComponentInChildren<Animator>().SetTrigger("Clicked");
            s.Clear();
        }
    }
  
    private void RayInputValue()//Temporary version uses raycast input
    {
        Ray ray = Camera.main.ScreenPointToRay(SceenCenter);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10.0f))
        {
            if (Input.GetMouseButtonDown(0))
            {
                hit.transform.gameObject.GetComponentInChildren<Animator>().SetTrigger("Clicked");

                if (hit.collider.CompareTag("1"))
                {
                    s.Enqueue(1);
                }
                else if (hit.collider.CompareTag("2"))
                {
                    s.Enqueue(2);
                }
                else if (hit.collider.CompareTag("3"))
                {
                    s.Enqueue(3);
                }
                else if (hit.collider.CompareTag("4"))
                {
                    s.Enqueue(4);
                }
                else if (hit.collider.CompareTag("5"))
                {
                    s.Enqueue(5);
                }
                else if (hit.collider.CompareTag("6"))
                {
                    s.Enqueue(6);
                }
                else if (hit.collider.CompareTag("7"))
                {
                    s.Enqueue(7);
                }
                else if (hit.collider.CompareTag("8"))
                {
                    s.Enqueue(8);
                }
                else if (hit.collider.CompareTag("9"))
                {
                    s.Enqueue(9);
                }
                else if (hit.collider.CompareTag("enter"))
                {
                    print();
                }
                else if (hit.collider.CompareTag("delete"))
                {
                    s.Clear();
                }
            }
        }
    }
}
