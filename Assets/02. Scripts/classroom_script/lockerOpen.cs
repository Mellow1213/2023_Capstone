using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class lockerOpen : MonoBehaviour
{
    private Vector3 ScreenCenter;
    private bool isTriggered = false;
    private bool isTriggered2 = false;
    private float GaugeTimer;
    
    private float Gauge = 0.3f;

    public LayerMask myLayer;
    public GameObject ob;

    public AudioClip doorSound;
    private AudioSource audioSource;

    //Angular Velocity
    Quaternion previousRotation; //�� �������� �����̼� ��
    Vector3 angularVelocity; //���ӵ��� ������ ����
    Vector3 speed;

    //�� �Լ��� ������Ʈ���� �����ݴϴ�.
    public Vector3 GetPedestrianAngularVelocity()
    {
        Quaternion deltaRotation = ob.transform.rotation * Quaternion.Inverse(previousRotation);

        previousRotation = ob.transform.rotation;

        deltaRotation.ToAngleAxis(out var angle, out var axis);

        //�������� �������� ��ȯ
        angle *= Mathf.Deg2Rad;

        angularVelocity = (1.0f / Time.deltaTime) * angle * axis;

        //���ӵ� ��ȯ
        return angularVelocity;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        ScreenCenter = new Vector3(Camera.main.pixelWidth/2, Camera.main.pixelHeight/2);
        audioSource.clip = doorSound;
        audioSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        isTriggered = Input.GetMouseButton(0);
        isTriggered2 = Input.GetMouseButton(1);
        audioSource.Play();

        if (isTriggered || isTriggered2)
        {
            speed = GetPedestrianAngularVelocity();
            Gauge += Time.deltaTime * speed.magnitude;
        }
        else
        {
            Gauge = 0.3f;
        }

        if (isTriggered)
        {
            GaugeTimer = -1 * Time.deltaTime * 20;//���⿡ �� ���ϱ�
            audioSource.volume = Gauge;
            if (ob.transform.eulerAngles.y >= 170) ob.transform.Rotate(0, GaugeTimer, 0);
            //Debug.Log("ob.transform.rotation.y = " + ob.transform.rotation.y);
            //Debug.Log("ob.transform.eulerAngles.y = " + ob.transform.eulerAngles.y);
        }
        else if (isTriggered2)
        {
            GaugeTimer = Time.deltaTime * 20;//���⿡ �� ���ϱ�
            ob.transform.Rotate(0, GaugeTimer, 0);
            Debug.Log("Close");
            audioSource.volume = Gauge;
            
            if (ob.transform.eulerAngles.y >= 270f) ob.transform.rotation = Quaternion.Euler(0, 270, 0);
            
            //Debug.Log("ob.transform.eulerAngles.y = " + ob.transform.eulerAngles.y);
        }
        else
        {
            GaugeTimer = 0;
        }

        /*
        Ray ray = Camera.main.ScreenPointToRay(ScreenCenter);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100.0f, myLayer))
        {
            GameObject ob = hit.transform.gameObject;
            if(isTriggered)
            {
                GaugeTimer = -1 * Time.deltaTime * 20;
                if (ob.transform.eulerAngles.y >= 170) ob.transform.Rotate(0, GaugeTimer, 0);
            }
            if (isTriggered2)
            {
                GaugeTimer = Time.deltaTime * 10;
                ob.transform.Rotate(0, GaugeTimer, 0);
                if (ob.transform.eulerAngles.y >= 270f) ob.transform.rotation = Quaternion.Euler(0, 270, 0);
            }
        }
         */
    }
}
