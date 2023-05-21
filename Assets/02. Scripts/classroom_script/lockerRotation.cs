using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class lockerRotation : MonoBehaviour
{
    private float Gauge = 0.3f;

    private GameObject ob;//������ hit�� ����ؼ� ��� object�� �����ߴµ�, ���ŷο��� �׽�Ʈ������ ������ �繰�� ���� ���Ƿ� ������.

    //private OVRPlayerCtrl playerCtrl;
    public GameObject player;

    public AudioClip doorSound;
    private AudioSource audioSource;

    //Angular Velocity, ��α� ����
    Quaternion previousRotation; //�� �������� �����̼� ��
    Vector3 angularVelocity; //���ӵ��� ������ ����
    Vector3 speed;

    public bool DoorEventTrue = false;

    private bool opening = false;

    public Vector3 GetPedestrianAngularVelocity()//�� �ӵ��� ���ϴ� �Լ�
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
        //playerCtrl = player.GetComponent<OVRPlayerCtrl>();
        audioSource = this.GetComponent<AudioSource>();
        audioSource.clip = doorSound;
        audioSource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        //audioSource.Play();

        if (opening)
        {
            speed = GetPedestrianAngularVelocity();
            Debug.Log("speed : " + speed);
            Gauge += Time.deltaTime * speed.magnitude; //���ӵ��� �� ����(����) ũ��
            audioSource.volume = Gauge;

            if (Gauge > 0.7)
            {
                //���ε��� �Ҹ�, �ִϸ��̼� ����
                DoorEventTrue = true;
                Gauge = 0.3f;
                Debug.Log("good");
            }
        }
        else
        {
            Gauge = 0.3f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("handle"))//if (other.gameObject.CompareTag("handle") && playerCtrl.down)//���̽�ƽ�� ��� ���� ��
        {
            Debug.Log("touch");
            opening = true;
            ob = other.transform.parent.gameObject;
        }
        else
        {
            opening = false;
        }
    }
}
