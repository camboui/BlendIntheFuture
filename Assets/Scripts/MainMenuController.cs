using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public Text[] selectables;
	public int currentSelection; //current text selected
	public bool[] changedRecently; //if player pushed axis button recently, prevent from spamming
	private List<XboxInput> xboxInputs;

	void Start () {	
		xboxInputs = new List<XboxInput> ();

		//Get all usable texts
		selectables = transform.GetComponentsInChildren <Text> ();
		changedRecently = new bool[4];
		currentSelection = 0;

		//Initialise 
		for (int i = 0; i < 4; i++) {
			changedRecently [i] = false;
			xboxInputs.Add(new XboxInput(i+1));
		}
		selectables [currentSelection].color = Color.red;
	}

	void Update(){
		//Foreach controller, handle actions
		for (int j = 0; j < 4; j++) {

			float joyStickY = xboxInputs [j].getYaxis ();

			//if joystick is back approximately in the middle of its axis, it can be used again
			if (joyStickY < 0.5f && joyStickY > -0.5f)
				changedRecently [j] = false;

			//Up direction : change selected text
			if (joyStickY == 1 && !changedRecently [j]) {
				selectables [currentSelection].color = Color.white;

				currentSelection--;
				if (currentSelection == -1)
					currentSelection = selectables.Length - 1;

				selectables [currentSelection].color = Color.red;

				changedRecently [j] = true;
			}
			//Down direction : change selected text
			else if (joyStickY == -1 && !changedRecently [j]) {
				 
				selectables [currentSelection].color = Color.white;

				currentSelection++;
				if (currentSelection == selectables.Length)
					currentSelection = 0;

				selectables [currentSelection].color = Color.red;

				changedRecently [j] = true;
			}

			//Handle button actions
			if (Input.GetKeyDown (xboxInputs [j].A)) {
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
