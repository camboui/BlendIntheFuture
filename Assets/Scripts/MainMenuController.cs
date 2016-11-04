using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public Text[] selectables;
	public int currentSelection;
	public bool[] changedRecently;

	void Start () {
		selectables = transform.GetComponentsInChildren <Text> ();
		changedRecently = new bool[4];
		currentSelection = 0;
		for (int i = 0; i < 4; i++) {
			changedRecently [i] = false;
		}
		selectables [currentSelection].color = Color.red;
	}

	void Update(){


		for (int j = 1; j <= 4; j++) {

			float joyStickY = Input.GetAxis ("Y_" + j);

			if (joyStickY < 0.5f && joyStickY > -0.5f)
				changedRecently [j - 1] = false;

			//change color for player
			if (joyStickY == 1 && !changedRecently [j - 1]) {
				selectables [currentSelection].color = Color.white;

				currentSelection--;
				if (currentSelection == -1)
					currentSelection = selectables.Length - 1;

				selectables [currentSelection].color = Color.red;

				changedRecently [j - 1] = true;
			} else if (joyStickY == -1 && !changedRecently [j - 1]) {
				 
				selectables [currentSelection].color = Color.white;

				currentSelection++;
				if (currentSelection == selectables.Length)
					currentSelection = 0;

				selectables [currentSelection].color = Color.red;

				changedRecently [j - 1] = true;
			}

			//A
			if (Input.GetKeyDown ("joystick " + j + " button 0")) {
				if (selectables [currentSelection].text.Equals ("Play")) {
					SceneManager.LoadScene ("PlayerSelectionMenu");
				} else if (selectables [currentSelection].transform.name.Equals ("Credits")) {
					//SceneManager.LoadScene ("Credits");
				} else if (selectables [currentSelection].transform.name.Equals ("Quit")) {
					Application.Quit ();
				}
			}

		}
	
	}
	

}
