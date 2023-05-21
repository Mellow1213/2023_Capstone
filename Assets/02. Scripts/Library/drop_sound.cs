using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drop_sound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip clips;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            audioSource.clip = clips;
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}
