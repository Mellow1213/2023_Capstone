using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class lockerRotation : MonoBehaviour
{
    private float Gauge = 0.3f;

    public AudioClip lockerSound;
    private AudioSource audioSource;

    //Angular Velocity, ��α� ����
    Quaternion previousRotation; //�� �������� �����̼� ��
    Vector3 angularVelocity; //���ӵ��� ������ ����
    Vector3 speed;

    public static lockerRotation instance;
    public bool DoorEventTrue = false;

    private Quaternion pastRotation, currentRotation;
    private bool isRotate = false;

    public Vector3 GetPedestrianAngularVelocity()//�� �ӵ��� ���ϴ� �Լ�
    {
        Quaternion deltaRotation = this.transform.rotation * Quaternion.Inverse(previousRotation);

        previousRotation = this.transform.rotation;

        deltaRotation.ToAngleAxis(out var angle, out var axis);

        angle *= Mathf.Deg2Rad;

        angularVelocity = (1.0f / Time.deltaTime) * angle * axis;

        return angularVelocity;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        audioSource.clip = lockerSound;
        pastRotation = this.transform.rotation;
        lockerRotation.instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        currentRotation = this.transform.rotation;
        if(currentRotation != pastRotation)
        {
            isRotate = true;
        }
        else
        {
            isRotate = false;
        }
        isRotation();
    }

    private void isRotation()
    {
        if (isRotate)
        {
            StartCoroutine(curRotation());
        }
        else
        {
            Gauge = 0.3f;
        }
    }
    IEnumerator curRotation()
    {
        speed = GetPedestrianAngularVelocity();
        Debug.Log("speed : " + speed);
        Gauge += Time.deltaTime * speed.magnitude; //���ӵ��� �� ����(����) ũ��
        audioSource.volume = Gauge;

        if (Gauge > 0.7)
        {
            //���ε��� �Ҹ�, �ִϸ��̼� ����
            audioSource.PlayOneShot(audioSource.clip);
            DoorEventTrue = true;
            Gauge = 0.3f;
            Debug.Log("good");
        }

        yield return new WaitForSeconds(5);
        pastRotation = currentRotation;

    }
}