using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crBMPEvent : MonoBehaviour
{

    public Material classRoomsky;
    public Material classRoomskyChange;

    public GameObject[] blood;
    public GameObject sizeUpBlood;

    private int level = 1;

    private bool[] isFilled;

    // Start is called before the first frame update
    void Start()
    {
        isFilled = new bool[blood.Length];
        for (int i = 0; i < blood.Length; i++)
        {
            blood[i].SetActive(false);
            isFilled[i] = false;
        }
        RenderSettings.skybox = classRoomsky;
        sizeUpBlood.SetActive(false); 
        Invoke("bloodOn", 100f);
    }

    // Update is called once per frame
    void Update()
    {
        if(FTU.Instance.BPMEvent >= level)
        {//Input.GetKeyDown(KeyCode.L)
            RenderSettings.skybox = classRoomskyChange;
            StartCoroutine(changeColor());
            ActiveBlood();
            sizeUpBlood.transform.localScale *= 1.2f;
            level++;
        }
    }

    void ActiveBlood()
    {
        int x = Random.Range(0, blood.Length);

        if (!isFilled[x])
        {
            blood[x].SetActive(true);
            isFilled[x] = true;
        }
    }
    IEnumerator changeColor()
    {
        yield return new WaitForSeconds(10);
        RenderSettings.skybox = classRoomsky;
    }
    void bloodOn()
    {
        sizeUpBlood.SetActive(true);
    }
}
