using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demoBehaviour : MonoBehaviour {
	Transform pyvaoq1 = null;
	Transform pyvaoq2 = null;
	Animator an;	

	// Use this for initialization
	void Start () {
		pyvaoq1 = transform.GetChild(1);
		pyvaoq2 = transform.GetChild(2);
		an = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.Keypad8)) {
			pyvaoq1.localPosition = pyvaoq1.localPosition + new Vector3 (0f, 0f, 0.02f);
		}
		if (Input.GetKey (KeyCode.Keypad2)) {
			pyvaoq1.localPosition = pyvaoq1.localPosition + new Vector3 (0f, 0f, -0.02f);
		}
		if (Input.GetKey (KeyCode.Keypad4)) {
			pyvaoq2.localPosition = pyvaoq2.localPosition + new Vector3 (0f, 0f, 0.02f);
		}
		if (Input.GetKey (KeyCode.Keypad6)) {
			pyvaoq2.localPosition = pyvaoq2.localPosition + new Vector3 (0f, 0f, -0.02f);
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			Quaternion r = transform.rotation;
			r.eulerAngles += new Vector3 (0.75f, 0f, 0f);
			transform.rotation = r;
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			Quaternion r = transform.rotation;
			r.eulerAngles += new Vector3 (-0.75f, 0f, 0f);
			transform.rotation = r;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			Quaternion r = transform.rotation;
			r.eulerAngles += new Vector3 (0f, 0.75f, 0f);
			transform.rotation = r;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			Quaternion r = transform.rotation;
			r.eulerAngles += new Vector3 (0f, -0.75f, 0f);
			transform.rotation = r;
		}
		if (Input.GetKey (KeyCode.W)) {
			transform.localPosition += new Vector3 (0f, 0.02f, 0f);
		}
		if (Input.GetKey (KeyCode.S)) {
			transform.localPosition += new Vector3 (0f, -0.02f, 0f);
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.localPosition += new Vector3 (-0.02f, 0f, 0f);
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.localPosition += new Vector3 (0.02f, 0f, 0f);
		}
		if (Input.GetKeyDown (KeyCode.F8)) {
			if (an.enabled) {
				an.enabled = false;
			}else{
				an.enabled = true;
			}
		}
	}
}
