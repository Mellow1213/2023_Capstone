using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeDown : MonoBehaviour
{
    public AudioClip event1;
    public AudioClip[] event2;
    private AudioSource m_Source;
    // Start is called before the first frame update
    void Start()
    {
        m_Source = GetComponent<AudioSource>();
        m_Source.volume = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))//어떠한 심박수에 도달한다면
            StartCoroutine(event1Sound());
        if (Input.GetKeyDown(KeyCode.P))//어떠한 심박수에 도달한다면
            StartCoroutine(event2Sound());

    }

    IEnumerator event1Sound()
    {
        float sizeDown = 0.01f;
        m_Source.clip = event1;
        m_Source.loop = true;
        m_Source.Play();
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            m_Source.volume -= sizeDown;
            if (m_Source.volume <= 0f)
            {
                m_Source.loop = false;
                break;
            }
        }
    }

    IEnumerator event2Sound()
    {
        int i = Random.Range(0, 2);
        yield return new WaitForSeconds(1);
        m_Source.clip = event2[i];
        m_Source.PlayOneShot(m_Source.clip);
    }
}
