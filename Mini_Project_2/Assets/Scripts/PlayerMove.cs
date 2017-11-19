using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour {

	bool gameRunning;
	int turningSpeed = 40;
	
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
		if (gameRunning) {

			Vector3 forward = transform.TransformDirection (Vector3.forward) * Time.deltaTime;
			
			// Walking
			if (Input.GetAxis ("Vertical") > 0) {
				anim.SetBool ("walking", true);
				transform.position += forward;
			}
			else if (Input.GetAxis ("Vertical") < 0) {
				anim.SetBool ("walking", true);
				transform.position -= forward;
			}
			else if (Input.GetAxis ("Vertical") == 0) {
				anim.SetBool ("walking", false);
			}

			// Turning
			if (Input.GetAxis ("Horizontal") > 0) {
				transform.Rotate(Vector3.up * Time.deltaTime * turningSpeed, Space.World);
			} 
			else if (Input.GetAxis ("Horizontal") < 0) {
				transform.Rotate(Vector3.up * Time.deltaTime * -turningSpeed, Space.World);
			}

			// Pointing
			if(Input.GetKeyDown(KeyCode.P)){
				anim.SetTrigger ("point");

				GameObject closestTable = findClosestTable ();
				AudioSource tableAudio = closestTable.GetComponent<AudioSource> ();
				print (closestTable.tag);

				if (tableAudio.mute) {
					tableAudio.mute = false;
				}
				tableAudio.Play ();
			}

		}
	}

	void startGame(){
		gameRunning = true;
		Destroy (GameObject.Find("StartPanel"));

		// Enable all animations
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

	public GameObject findClosestTable()
	{
		GameObject[] macTables = GameObject.FindGameObjectsWithTag ("macTable");
		GameObject[] iphoneTables = GameObject.FindGameObjectsWithTag ("iphoneTable");

		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;

		foreach (GameObject go in macTables)
		{
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;

			if (curDistance < distance)
			{
				closest = go;
				distance = curDistance;
			}
		}

		foreach (GameObject go in iphoneTables)
		{
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;

			if (curDistance < distance)
			{
				closest = go;
				distance = curDistance;
			}
		}

		return closest;
	}

}
