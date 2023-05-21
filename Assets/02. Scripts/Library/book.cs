using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class book : MonoBehaviour
{
    private GameObject[] books;

    public GameObject target;
    Rigidbody[] rigid;
    Vector3[] p;

    //used, random
    HashSet<int> exclude = new HashSet<int>();//�ߺ����� �ʵ��� ���
    float bpm = 0; // �ɹڼ�
    int bpmInt = 0;
    int adf = 5;//å�� ������ �ӵ�

    private AudioSource audioSource;
    public AudioClip[] clips;

    // Start is called before the first frame update
    void Start()
    {
        books = GameObject.FindGameObjectsWithTag("books");// �±׸� ��� å ������Ʈ ����
        rigid = new Rigidbody[books.Length];
        p = new Vector3[books.Length];
        for (int i = 0; i < books.Length; i++)
        {
            rigid[i] = books[i].GetComponent<Rigidbody>();
            p[i] = (target.transform.position - books[i].transform.position).normalized;
        }
        audioSource = GetComponent<AudioSource>();
    }

    int ExceptRandom() // ������ ����
    {
        var range = Enumerable.Range(0, books.Length).Where(i => !exclude.Contains(i));
        var rand = new System.Random();
        int index = rand.Next(0, books.Length - exclude.Count);
        return range.ElementAt(index);
    }

    void AddForceToBooks() // å�忡�� å �������� ����.
    {
        int x = ExceptRandom();
        exclude.Add(x);
        rigid[x].AddForce(-1.0f * adf * p[x], ForceMode.Impulse);
        StartCoroutine(bookSound());
    }

    // Update is called once per frame
    void Update()
    {
        bpmInt = (int)(bpm * 10);

        for (int i = 1; i < bpmInt; i++)//������ å�� ������ �������� ���ؼ� addforcetoBooks�� �׸�ŭ �ݺ�
        {
            AddForceToBooks();
        }
    }

    IEnumerator bookSound()
    {
        int x;
        yield return new WaitForSeconds(1.5f);
        x = Random.Range(0, 4);
        audioSource.clip = clips[x];
        audioSource.PlayOneShot(audioSource.clip);
    }
}
