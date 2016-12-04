using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class CheckNewControllers : MonoBehaviour {

	public Sprite selected;
	public AudioSource selectedAudio;

	private int currentPlugged;
	private int currentAccepted;
	private List<GameObject> playersSelector;
	private List<int> pluggedControllersId;
	private List<XboxInput> xboxInputs;

	// Use this for initialization
	void Awake () {
		//Clear global variables when entering back in the scene
		GameVariables.players.Clear ();
		xboxInputs = new List<XboxInput> ();

		playersSelector = new List<GameObject> ();
		for (int i = 1; i <= 4; i++) {
			playersSelector.Add (GameObject.Find("Player " + i));
			xboxInputs.Add (new XboxInput (i));
		}

		currentPlugged = getNumberOfDevices();
		currentAccepted = 0;
		pluggedControllersId = new List<int> ();

		int k = 0;
		foreach (GameObject go in playersSelector) {
			go.GetComponent<PlayerSelectionController> ().enabled = false;
			if (k < currentPlugged) {
				k++;
				go.transform.FindChild("enabled").gameObject.SetActive(true);
				go.transform.FindChild ("disabled").gameObject.SetActive (false);
			}
		}


	}

	//get number of connected controllers
	private int getNumberOfDevices(){
		int i = 0;
		foreach (string p in Input.GetJoystickNames ()) {
			if (p != "")// because GetJoystickNames gets buggy empty strings
				i++;
		}
		return i;
	}


	void Update()
	{
		//check if there is a change in connected controllers
		int newPlugged = getNumberOfDevices ();


		if (newPlugged != currentPlugged) {
			//Reaload scene if a controller is removed
			if (newPlugged < currentPlugged) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
			} else {
				//Change Debug.Loging if controller is added
				for (int j = currentPlugged + 1; j <= newPlugged - currentPlugged + 1; j++) {
					playersSelector [j - 1].transform.FindChild ("enabled").gameObject.SetActive (true);
					playersSelector [j - 1].transform.FindChild ("disabled").gameObject.SetActive (false);
				}
			}
			currentPlugged = newPlugged;
		}

		for (int j = 1; j <= 4; j++) {
			//Player joins the game, change Debug.Loging and activate script on Gameobject
			if (Input.GetKeyDown (xboxInputs[j-1].A)) {
				if (!pluggedControllersId.Contains (j)) {
					pluggedControllersId.Add (j);
					playersSelector [currentAccepted].transform.FindChild ("Background").GetComponent<Image> ().sprite = selected;
					playersSelector [currentAccepted].transform.FindChild ("Player").gameObject.SetActive(true);
					playersSelector [currentAccepted].transform.FindChild ("PlayerNumber").gameObject.SetActive(true);
					playersSelector [currentAccepted].transform.FindChild ("enabled").gameObject.SetActive (false);
					playersSelector [currentAccepted].transform.FindChild ("Press A").gameObject.SetActive (false);
					playersSelector [currentAccepted].transform.FindChild ("InstructionsPanel").gameObject.SetActive (true);
					selectedAudio.Play ();
					playersSelector [currentAccepted].GetComponent<PlayerSelectionController> ().enabled = true;
					playersSelector [currentAccepted].GetComponent<PlayerSelectionController> ().playerControllerId = j;
					currentAccepted++;
				}
			}
			if (Input.GetKeyDown (xboxInputs [j - 1].B) && !pluggedControllersId.Contains (j)) {
				SceneManager.LoadScene (1);
			}
		}
	}

}
