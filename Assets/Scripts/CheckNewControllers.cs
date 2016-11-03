using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CheckNewControllers : MonoBehaviour {

	public GameObject prefab_PlayerChoice;


	private GameObject prefabParent;
	private int nbController;
	private List<GameObject> players;

	// Use this for initialization
	void Start () {
		players = new List<GameObject> ();
		nbController = getNumberOfDevices ();
		prefabParent = GameObject.Find ("Canvas");

		for (int i = 1; i <= nbController; i++) {
			createPrefab (i);
		}
	}

	//Instantiate prefab according to its given index
	private void createPrefab(int index){
		GameObject newGO = Instantiate (prefab_PlayerChoice) as GameObject;
		newGO.transform.name = "Player " + index;
		newGO.transform.SetParent (prefabParent.transform);

		newGO.GetComponentInChildren<PlayerSelectionController> ().playerId = index;

		newGO.GetComponentInChildren<Text> ().text = "Player " + index;
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
	private List<int> getDevicesID(){
		List<int> res = new List<int> ();
		for (int j = 1; j <= 4; j++) {
			if (Input.GetKeyDown ("joystick " + j + " button 0")) {
				if(res.Contains(j))
					res.Add (j);
			}
		}
		return res;
	}


	void Update()
	{
		//Know which one pushed A


		int currentNbController = getNumberOfDevices ();
		if (currentNbController != nbController) {
			//add missing
			if (currentNbController > nbController) {
				for (int i = 1; i <= currentNbController - nbController; i++) {
					int currentIndex = players.Count + i;
					createPrefab (currentIndex);
				}
			} else {
				//remove extra
				for (int i = 0; i < nbController - currentNbController; i++) {
					GameObject.DestroyImmediate (players [players.Count - 1]);
					players.RemoveAt (players.Count - 1);
				}
			}
			nbController = currentNbController;
		}
			
	}
		
}
