using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class VolumeDown : MonoBehaviour
{
    public AudioClip event1;
    public AudioClip[] event2;
    private AudioSource m_Source;

    private bool sEvent1 = false;
    private bool sEvent3 = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Source = GetComponent<AudioSource>();
        m_Source.volume = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (FTU.Instance.BPMEvent == 1 && !sEvent1)//어떠한 심박수에 도달한다면
        {
            StartCoroutine(event1Sound());
            sEvent1 = true;
            StartCoroutine(changeTFforEvent1());
        }
        else if (FTU.Instance.BPMEvent == 3 && !sEvent3)//어떠한 심박수에 도달한다면
        {
            StartCoroutine(event2Sound());
            sEvent3 = true;
            StartCoroutine(changeTFforEvent3());
        }
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
    IEnumerator changeTFforEvent1()
    {
        yield return new WaitForSeconds(120);
        sEvent1 = false;
    }
    IEnumerator changeTFforEvent3()
    {
        yield return new WaitForSeconds(120);
        sEvent3 = false;
    }
}
