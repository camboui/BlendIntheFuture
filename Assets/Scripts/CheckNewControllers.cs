using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CheckNewControllers : MonoBehaviour {

	public GameObject prefab_PlayerChoice;


	private GameObject prefabParent;
	private int nbControllerPrinted;
	private List<GameObject> players;
	private List<int> pluggedControllersId;

	// Use this for initialization
	void Start () {
		players = new List<GameObject> ();
		nbControllerPrinted = 0;
		prefabParent = GameObject.Find ("Canvas");
		pluggedControllersId = new List<int> ();
	}

	//Instantiate prefab according to its given index
	private void createPrefab(int index, int joystickId){
		GameObject newGO = Instantiate (prefab_PlayerChoice) as GameObject;
		newGO.transform.name = "Player " + (index + 1);
		newGO.transform.SetParent (prefabParent.transform);

		newGO.GetComponentInChildren<PlayerSelectionController> ().playerControllerId = joystickId;

		newGO.GetComponentInChildren<Text> ().text = "Player " + (index + 1);
		newGO.transform.localPosition = new Vector2 ((index - 1) * prefab_PlayerChoice.GetComponent<RectTransform> ().sizeDelta.x, 0);
		players.Add (newGO);
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

	//get number of connected controllers
	private void updateDevicesID(){
		for (int j = 1; j <= 4; j++) {
			if (Input.GetKeyDown ("joystick " + j + " button 0")) {
				if (!pluggedControllersId.Contains (j)) {
					pluggedControllersId.Add (j);
				}
			}
		}
	}


	void Update()
	{
		//Know which one pushed A
		updateDevicesID();

		int currentNbController = pluggedControllersId.Count;

		if (currentNbController != nbControllerPrinted) {
			//add missing
			if (currentNbController > nbControllerPrinted) {
				for (int i = 0; i < currentNbController - nbControllerPrinted; i++) {
					int currentIndex = players.Count + i;
					createPrefab (currentIndex,pluggedControllersId[currentIndex]);
				}
			} else {
				//remove extra
				for (int i = 0; i < nbControllerPrinted - currentNbController; i++) {
					GameObject.DestroyImmediate (players [players.Count]);
					players.RemoveAt (players.Count - 1);
				}
			}
			nbControllerPrinted = currentNbController;
		}

			
	}
		
}
