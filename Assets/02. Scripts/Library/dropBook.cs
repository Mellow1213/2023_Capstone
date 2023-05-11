using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropBook : MonoBehaviour
{
    //맵에서 두번 실행. 한 번 이벤트가 발생한 곳에서는 다시는 일어나지 않음.
    public GameObject[] books;
    private Queue<GameObject> queue = new Queue<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)//q플레이어 머리 위에서 책 떨어지는 이벤트
    {
        if (other.gameObject.CompareTag("Player")) {
            int y = Random.Range(5, 12);//책의 개수
            Vector3 pos = new Vector3(other.transform.position.x, 12, other.transform.position.z);
            for (int i = 0; i  < y; i++)
            {
                int x = Random.Range(0, 4);//책의 종류
                queue.Enqueue(Instantiate(books[x], pos, Quaternion.identity));//매번 생성되는 책의 개수가 다르기 때문에 queue사용
                StartCoroutine(deleteBooks());
            }
        }
    }

    IEnumerator deleteBooks()//10초 뒤 생성된 책 Clone 삭제
    {
        yield return new WaitForSeconds(10);
        while(queue.Count > 0)
            Destroy(queue.Dequeue());
    }
}
