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
    int adf = 5;//책이 나가는 속도
    private bool sEvent1 = false;

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
        StartCoroutine(BookTriggerTrue(x));
    }

    // Update is called once per frame
    void Update()
    {
        if (FTU.Instance.BPMEvent == 1 && !sEvent1)//어떠한 심박수에 도달한다면 FTU.Instance.BPMEvent >= 2
        {
            Debug.Log("work");
            sEvent1 = true;
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
        sEvent1 = false;
    }

    IEnumerator BookTriggerTrue(int x)
    {
        yield return new WaitForSeconds(5);
        books[x].transform.GetComponent<Rigidbody>().useGravity = false;
        books[x].transform.GetComponent<Collider>().isTrigger = true;
    }
}
