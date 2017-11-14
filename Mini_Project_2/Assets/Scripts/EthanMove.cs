using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EthanMove : MonoBehaviour {

	bool gameRunning;

	// Buttons
	public Button start;
	public Button moreInfo;
	public Button credits;
	public Button quit;

	// Audio
	AudioSource audioSource;
	public AudioClip startMenu;
	public AudioClip gamePlay;

	Animator anim;

	void Start () {
		gameRunning = false;

		anim = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource> ();

		start.onClick.AddListener(startGame);
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
		if(Input.GetKeyDown(KeyCode.P)){
			anim.SetBool("PointingLeft", true);
		}
		if(Input.GetKeyUp(KeyCode.P)){
			anim.SetBool("PointingLeft", false);
		}
	}

	void startGame(){
		gameRunning = true;
		Destroy (GameObject.Find("StartPanel"));

		// Enable all animators
		anim.enabled = true;

		GameObject[] iPhones = GameObject.FindGameObjectsWithTag ("iPhone");
		for (var i = 0; i < iPhones.Length; i++) {
			(iPhones [i].GetComponent<Animator> ()).enabled = true;
		}

		GameObject[] macbooks = GameObject.FindGameObjectsWithTag ("mac");
		for (var i = 0; i < macbooks.Length; i++) {
			(macbooks [i].GetComponent<Animator> ()).enabled = true;
		}

		// Change audio clip
		audioSource.clip = gamePlay;
		audioSource.Play ();

	}

}
