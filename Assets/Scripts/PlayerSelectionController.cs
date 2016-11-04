using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class PlayerSelectionController : MonoBehaviour {

	public static int nbReady;
	public int playerControllerId;

	private bool changedRecently;

	private KeyCode A,B,BStart;
	private Color currentColor;
	private List<Image> imagesToColor;
	private Text text;
	private Text playText;

	private int currentState;
	private int maxState;
	private List<String> textState;

	void Start()
	{
		currentColor = GameVariables.availableColors [0];
		changedRecently = false;

		imagesToColor = new List<Image> ();
		imagesToColor.Add( transform.FindChild ("Background").GetComponentInChildren<Image> ());
		imagesToColor.Add( transform.FindChild ("enabled").GetComponentInChildren<Image> ());
		colorImages ();

		playText = GameObject.Find ("Play").GetComponent<Text>();

		currentState = 0;
		text = transform.FindChild ("Press A").GetComponent<Text> ();
		textState = new List<string> (){ "Choose Color (A)","Ready ? (A)","Ready !"};
		maxState = textState.Count;
		text.text = textState [currentState];

		//See http://wiki.unity3d.com/index.php?title=Xbox360Controller for button number
		A = (KeyCode)System.Enum.Parse (typeof(KeyCode), "Joystick" + playerControllerId + "Button0");
		B = (KeyCode)System.Enum.Parse (typeof(KeyCode), "Joystick" + playerControllerId + "Button1");
		BStart = (KeyCode)System.Enum.Parse (typeof(KeyCode), "Joystick" + playerControllerId + "Button7");
	
	}

	void colorImages(){
		foreach (Image img in imagesToColor) {
			img.color = currentColor;
		}
	}

	void Update () {

		float joyStickX = Input.GetAxis ("X_" + playerControllerId);

		//prevent from infinite change
		if (currentState == 0) {
			if (joyStickX < 0.5f && joyStickX > -0.5f)
				changedRecently = false;
		
			//change color for player
			if (joyStickX == 1 && !changedRecently) {
				currentColor = GameVariables.getNextColorRight (currentColor);
				colorImages ();
				changedRecently = true;
			} else if (joyStickX == -1 && !changedRecently) {
				currentColor = GameVariables.getNextColorLeft (currentColor);
				colorImages ();
				changedRecently = true;
			}
		}
		//Go to next state if possible
		if (Input.GetKeyDown (A) || Input.GetKeyDown (BStart)) {
			int currentReady = nbReady;
			if (currentState < maxState) {
				currentState++;

				if (currentState == maxState-1)
					nbReady++;
				if (currentState < maxState)
					text.text = textState [currentState];
			}
			if (currentReady <= 1 && nbReady >= GameVariables.minPlayers) {
				playText.enabled = true;
			}
			if (currentReady >= 2) {
				SceneManager.LoadScene ("GameLoop"); //TODO change this
			}
		}
		//Go to previous state if possible
		else if (Input.GetKeyDown (B)) {
			//Player wants to go back to previous menu
			if (currentState == 0) { 
				SceneManager.LoadScene (1);
			}
			if (currentState > 0) {
				if (currentState == maxState-1)
					nbReady--;
				
				currentState--;
				text.text = textState [currentState];
			}
			if (nbReady < GameVariables.minPlayers) {
				playText.enabled = false;
			}
		}
	}

	void OnDestroy(){
		GameVariables.players.Add (new Player (playerControllerId,currentColor));
	}


}
