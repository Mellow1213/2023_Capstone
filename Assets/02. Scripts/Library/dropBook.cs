using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropBook : MonoBehaviour
{
    //맵에서 두번 실행. 한 번 이벤트가 발생한 곳에서는 다시는 일어나지 않음.
    public GameObject[] books;
    private Queue<GameObject> queue = new Queue<GameObject>();
    private int cnt = 0;
    private int x, y;

    private void OnTriggerEnter(Collider other)//q플레이어 머리 위에서 책 떨어지는 이벤트
    {
        if (other.gameObject.CompareTag("Player") && cnt<3) {
            y = Random.Range(10, 20);//책의 개수
            Vector3 pos = new Vector3(other.transform.position.x, 12, other.transform.position.z);
            for (int i = 0; i  < y; i++)
            {
                x = Random.Range(0, 4);//책의 종류
                queue.Enqueue(Instantiate(books[x], pos, Quaternion.identity));//매번 생성되는 책의 개수가 다르기 때문에 queue사용
                StartCoroutine(deleteBooks());
            }
            this.transform.Rotate(0f, 120f, 0f);
        }
    }
    IEnumerator deleteBooks()//10초 뒤 생성된 책 Clone 삭제
    {
        cnt++;
        yield return new WaitForSeconds(8);
        while(queue.Count > 0)
            Destroy(queue.Dequeue());
    }
}
