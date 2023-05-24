using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mannequin_surprise : MonoBehaviour
{
    public AudioClip dropSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private bool isEvent = false;
    // Update is called once per frame
    void Update()
    {
        if (FTU.Instance.BPMEvent == 2 && !isEvent)
        {
            StartCoroutine(DropEvent());
            isEvent = true;
        }
    }

    IEnumerator DropEvent()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(0.5f);
        GetComponent<AudioSource>().PlayOneShot(dropSound);
    }
}
