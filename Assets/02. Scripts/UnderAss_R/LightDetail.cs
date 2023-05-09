using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetail : MonoBehaviour
{
    //불 깜박이는 스크립트. 블로그에서 가져옴.
    //public GameObject illusion; //깜박거릴 때 나타면 좋은 유령??
    private Light theLight;
    private float targetIntensity;
    private float currentIntensity;

    void Start()
    {
        theLight = GetComponent<Light>();
        currentIntensity = theLight.intensity;
        targetIntensity = Random.Range(0.0f, 4.0f);
    }

    void Update()
    {
        if (Mathf.Abs(targetIntensity - currentIntensity) >= 0.01)
        {
            if (targetIntensity - currentIntensity >= 0)
                currentIntensity += Time.deltaTime * 10f;
            else
            {
                currentIntensity -= Time.deltaTime * 10f;
            }
            theLight.intensity = currentIntensity;
            theLight.range = currentIntensity + 7;
        }
        else
        {
            targetIntensity = Random.Range(0.0f, 4.0f);
        }
    }
}