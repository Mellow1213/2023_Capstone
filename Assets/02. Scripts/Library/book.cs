using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class book : MonoBehaviour
{
    private GameObject[] books;// 책 오브젝트들을 담을 배열

    Rigidbody[] rigid;// 책들의 Rigidbody 컴포넌트 배열
    Vector3[] p;// 중심으로부터 책 각각의 위치의 방향을 저장하는 벡터 배열

    //used, random
    HashSet<int> exclude = new HashSet<int>();//중복되지 않도록 사용
    int adf = 5;//책이 나가는 속도
    private bool sEvent1 = false;// 이벤트 상태 확인 변수

    private AudioSource audioSource;
    public AudioClip[] clips;// 책이 떨어질 때 재생할 오디오 클립들


    // Start is called before the first frame update
    void Start()
    {
        books = GameObject.FindGameObjectsWithTag("books");// 책이 날아갈 때 재생할 오디오 클립들
        rigid = new Rigidbody[books.Length];//books.Length만큼 rigid 배열 선언
        p = new Vector3[books.Length];//books.Length만큼 p 배열 선언
        for (int i = 0; i < books.Length; i++)
        {
            rigid[i] = books[i].GetComponent<Rigidbody>();
            p[i] = (this.transform.position - books[i].transform.position).normalized;//중심으로부터 책 각각의 위치의 방향을 구함
        }
        audioSource = GetComponent<AudioSource>();
    }

    int ExceptRandom() // 중복되지 않는 랜덤한 숫자를 반환
    {
        var range = Enumerable.Range(0, books.Length).Where(i => !exclude.Contains(i));
        var rand = new System.Random();
        int index = rand.Next(0, books.Length - exclude.Count);
        return range.ElementAt(index);
    }

    void AddForceToBooks() // 책들에 힘을 가해 날아가게 함
    {
        int x = ExceptRandom();
        exclude.Add(x);//중복되지 않도록 사용한 값을 저장
        rigid[x].AddForce(-1.0f * adf * p[x], ForceMode.Impulse);//중심의 반대(-1을 곱했기 때문)로 책이 날아감
        StartCoroutine(bookSound());//책떨어지는 소리
        StartCoroutine(BookTriggerTrue(x));//isTrigger 체크
    }

    // Update is called once per frame
    void Update()
    {
        if (FTU.Instance.BPMEvent == 1 && !sEvent1)//심박수에 도달한다면 이벤트 실행
        {
            //Debug.Log("work");
            sEvent1 = true;
            AddForceToBooks();
        }
    }

    IEnumerator bookSound()//책이 떨어질 때 소리가 나게하기 위해서
    {
        int x;
        yield return new WaitForSeconds(1.5f);
        x = Random.Range(0, 4);
        audioSource.clip = clips[x];//4개중 하나의 clip 실행
        audioSource.PlayOneShot(audioSource.clip);
        sEvent1 = false;
    }

    IEnumerator BookTriggerTrue(int x)// 책 Collider 설정 변경 코루틴, 책이 바닥에 쌓여도도 무시하고 지나갈 수 있기 위해서
    {
        yield return new WaitForSeconds(5);
        books[x].transform.GetComponent<Rigidbody>().useGravity = false;
        books[x].transform.GetComponent<Collider>().isTrigger = true;
    }
}
