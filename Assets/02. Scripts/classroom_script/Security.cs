using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Security : MonoBehaviour
{
    private int i = 0;
    public GameObject[] patrols;

    public GameObject player;
    private lockerOpen LO;

    // Start is called before the first frame update
    void Start()
    {
        LO = player.GetComponent<lockerOpen>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LO.DoorEventTrue)
        {
            StartCoroutine(activeSecuity());
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, patrols[i%2].transform.position, Time.deltaTime * 2);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("end")) {
            this.transform.Rotate(0, 180, 0);
            i++;
        }
    }
    IEnumerator activeSecuity()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(6);
        this.transform.GetChild(0).gameObject.SetActive(true);

    }
}
