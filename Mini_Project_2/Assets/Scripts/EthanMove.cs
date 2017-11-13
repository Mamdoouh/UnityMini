using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EthanMove : MonoBehaviour {

	Animator anim;

	void Start () {
		anim = GetComponent<Animator> ();
	}

	void Update () {

		// Moving forward
		if (Input.GetAxis ("Vertical") > 0) {
			anim.SetFloat ("walkingSpeed", 1);
			anim.SetBool ("movingForward", true);
		}

		else if (Input.GetAxis ("Vertical") < 0) {
			anim.SetFloat ("walkingSpeed", -1);
			anim.SetBool ("movingForward", true);
		}

		else if (Input.GetAxis ("Vertical") == 0) {
			anim.SetFloat ("walkingSpeed", 1);
			anim.SetBool ("movingForward", false);
		}

		// Turning
		if (Input.GetAxis ("Horizontal") > 0) {
			anim.SetBool ("turningRight", true);
		} 
		else if (Input.GetAxis ("Horizontal") < 0) {
			anim.SetBool ("turningLeft", true);
		} 
		else {
			anim.SetBool ("turningLeft", false);
			anim.SetBool ("turningRight", false);
		}

	}
}
