using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropBook : MonoBehaviour
{
    //�ʿ��� �ι� ����. �� �� �̺�Ʈ�� �߻��� �������� �ٽô� �Ͼ�� ����.
    public GameObject[] books;
    private Queue<GameObject> queue = new Queue<GameObject>();
    private int cnt = 0;
    private int x, y;

    private AudioSource audioSource;
    public AudioClip[] clips;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)//q�÷��̾� �Ӹ� ������ å �������� �̺�Ʈ
    {
        if (other.gameObject.CompareTag("Player") && cnt<3) {
            y = Random.Range(5, 12);//å�� ����
            Vector3 pos = new Vector3(other.transform.position.x, 12, other.transform.position.z);
            for (int i = 0; i  < y; i++)
            {
                int x = Random.Range(0, 4);//å�� ����
                queue.Enqueue(Instantiate(books[x], pos, Quaternion.identity));//�Ź� �����Ǵ� å�� ������ �ٸ��� ������ queue���
                //StartCoroutine(deleteBooks());
                StartCoroutine(BookSound());
            }
            this.transform.Rotate(0f, 120f, 0f);
        }
    }
    IEnumerator BookSound()
    {
        yield return new WaitForSeconds(1.5f);
        for(int i = 0; i < y; i++)
        {
            x = Random.Range(0, 3);
            audioSource.clip = clips[x];
            audioSource.PlayOneShot(audioSource.clip);
        }

    }
    IEnumerator deleteBooks()//10�� �� ������ å Clone ����
    {
        cnt++;
        yield return new WaitForSeconds(10);
        while(queue.Count > 0)
            Destroy(queue.Dequeue());
    }
}
