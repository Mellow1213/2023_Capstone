using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropBook : MonoBehaviour
{
    public GameObject[] books;
    private List<GameObject> spawnedBooks = new List<GameObject>();
    private bool eventTriggered = false;

    private void OnTriggerEnter(Collider other)//q플레이어 머리 위에서 책 떨어지는 이벤트
    {
        if (other.gameObject.CompareTag("Player") && !eventTriggered) {
            eventTriggered = true;
            int bookCount = Random.Range(10, 20);
            Vector3 pos = new Vector3(other.transform.position.x, 12, other.transform.position.z);
            for (int i = 0; i  < bookCount; i++)
            {
                int randomBookIndex = Random.Range(0, books.Length);
                GameObject spawnedBook = Instantiate(books[randomBookIndex], pos, Quaternion.identity);
                spawnedBooks.Add(spawnedBook);
            }
            StartCoroutine(deleteBooks());
        }
    }
    IEnumerator deleteBooks()//10초 뒤 생성된 책 Clone 삭제
    {
        yield return new WaitForSeconds(8);
        foreach (GameObject book in spawnedBooks)
        {
            Destroy(book);
        }
        spawnedBooks.Clear();
    }
}
