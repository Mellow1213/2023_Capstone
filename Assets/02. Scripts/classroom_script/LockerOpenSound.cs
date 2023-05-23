using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerOpenSound : MonoBehaviour
{
    public AudioClip lockerSound;
    private AudioSource audioSource;

    public static LockerOpenSound instance;

    // Start is called before the first frame update
    void Start()
    {
        LockerOpenSound.instance = this;
        audioSource = this.GetComponent<AudioSource>();
        audioSource.clip = lockerSound;
        audioSource.volume = 0.65f;
    }

    // Update is called once per frame
    public void PlayOpenSound()
    {
        audioSource.Play();
    }
}
