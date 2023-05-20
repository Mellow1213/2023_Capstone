using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class book : MonoBehaviour
{
    private GameObject[] books;

    public GameObject target;
    Rigidbody[] rigid;
    Vector3[] p;

    //used, random
    HashSet<int> exclude = new HashSet<int>();//�ߺ����� �ʵ��� ���
    int adf = 5; // �ɹڼ��� ���� ���� �� ��.

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))//������ å�� ������ �������� ���ؼ� addforcetoBooks�� �׸�ŭ �ݺ�
        {
            AddForceToBooks();
        }
    }
}