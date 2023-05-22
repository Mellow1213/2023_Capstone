using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class lockerRotation : MonoBehaviour
{
    private float Gauge = 0.25f;

    //Angular Velocity, ��α� ����
    Quaternion previousRotation; //�� �������� �����̼� ��
    Vector3 angularVelocity; //���ӵ��� ������ ����
    Vector3 speed;


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
        pastRotation = this.transform.rotation;
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
        //Debug.Log("speed : " + speed);
        Gauge += 0.008f * speed.magnitude; //���ӵ��� �� ����(����) ũ��
        Debug.Log("Gauge : " + Gauge);
        if (Gauge > 0.6f)
        {
            //���ε��� �Ҹ�, �ִϸ��̼� ����
            LockerOpenSound.instance.PlayOpenSound();
            DoorEvent.instance.DoorEventFunc();
            StartCoroutine(Security.instance.activeSecuity());
            Gauge = 0.25f;
            //Debug.Log("good");
        }

        yield return new WaitForSeconds(2);
        pastRotation = currentRotation;
    }

}