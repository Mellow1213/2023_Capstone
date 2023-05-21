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
    HashSet<int> exclude = new HashSet<int>();//중복되지 않도록 사용
    float bpm = 0; // 심박수
    int bpmInt = 0;
    int adf = 5;//책이 나가는 속도

    private AudioSource audioSource;
    public AudioClip[] clips;

    // Start is called before the first frame update
    void Start()
    {
        books = GameObject.FindGameObjectsWithTag("books");// 태그를 골라서 책 오브젝트 수집
        rigid = new Rigidbody[books.Length];
        p = new Vector3[books.Length];
        for (int i = 0; i < books.Length; i++)
        {
            rigid[i] = books[i].GetComponent<Rigidbody>();
            p[i] = (target.transform.position - books[i].transform.position).normalized;
        }
        audioSource = GetComponent<AudioSource>();
    }

    int ExceptRandom() // 랜덤한 숫자
    {
        var range = Enumerable.Range(0, books.Length).Where(i => !exclude.Contains(i));
        var rand = new System.Random();
        int index = rand.Next(0, books.Length - exclude.Count);
        return range.ElementAt(index);
    }

    void AddForceToBooks() // 책장에서 책 랜덤으로 뽑힘.
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

        for (int i = 1; i < bpmInt; i++)//움직일 책의 개수를 랜덤값을 구해서 addforcetoBooks를 그만큼 반복
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
