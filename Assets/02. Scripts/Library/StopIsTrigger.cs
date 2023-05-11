using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopIsTrigger : MonoBehaviour
{
    private DownMannequin DM;
    private GameObject[] mannequin;
    // Start is called before the first frame update
    void Start()
    {
        DM = this.gameObject.GetComponent<DownMannequin>();
        mannequin = GameObject.FindGameObjectsWithTag("mannequinpart");
    }

    // Update is called once per frame
    void Update()
    {
        if (DM.DestroyMannequin)
        {
            for(int i = 0; i < mannequin.Length; i++)
            {
                Destroy(mannequin[i], 3);
            }
        }
    }
}
