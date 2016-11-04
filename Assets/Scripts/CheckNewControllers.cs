using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class CheckNewControllers : MonoBehaviour {

	public GameObject prefab_PlayerChoice;
	private int currentPlugged;
	private int currentAccepted;
	private List<GameObject> playersSelector;
	private List<int> pluggedControllersId;

	private RectTransform canvasRect;
	// Use this for initialization
	void Start () {
		//Clear global variables when entering back in the scene
		GameVariables.players.Clear ();


		playersSelector = new List<GameObject> ();
		for (int i = 1; i <= 4; i++) {
			playersSelector.Add (GameObject.Find("Player " + i));
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
		int newPlugged = getNumberOfDevices ();

		if (newPlugged != currentPlugged) {
			if (newPlugged < currentPlugged) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
			} else {
				for (int j = currentPlugged + 1; j <= newPlugged - currentPlugged + 1; j++) {
					
					playersSelector [j - 1].transform.FindChild ("enabled").gameObject.SetActive (true);
					playersSelector [j - 1].transform.FindChild ("disabled").gameObject.SetActive (false);
				}
			}
			currentPlugged = newPlugged;
		}

		for (int j = 1; j <= 4; j++) {
			//A
			if (Input.GetKeyDown ("joystick " + j + " button 0")) {
				if (!pluggedControllersId.Contains (j)) {
					pluggedControllersId.Add (j);
					playersSelector [currentAccepted].GetComponent<PlayerSelectionController> ().enabled = true;
					playersSelector [currentAccepted].GetComponent<PlayerSelectionController> ().playerControllerId = j;
					currentAccepted++;
				}
			}

		}
	}
		
}
