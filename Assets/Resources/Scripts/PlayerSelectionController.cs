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

	private Color currentColor;
	private List<Image> imagesToColor;
	private Text text;
	private Text playText;

	private int currentState;
	private int maxState;
	private List<String> textState;

	private XboxInput xboxInput;

	void Start()
	{
		currentColor = GameVariables.availableColors [0];
		changedRecently = false;

		//All images which need to be recolored according to player selection
		imagesToColor = new List<Image> ();
		imagesToColor.Add( transform.FindChild ("Background").GetComponentInChildren<Image> ());
		imagesToColor.Add( transform.FindChild ("enabled").GetComponentInChildren<Image> ());
		colorImages ();

		playText = GameObject.Find ("Play").GetComponent<Text>();

		//Different states of validation 
		currentState = 0;
		text = transform.FindChild ("Press A").GetComponent<Text> ();
		textState = new List<string> (){ "Choose Color (A)","Ready ? (A)","Ready !"};
		maxState = textState.Count;
		text.text = textState [currentState];

		xboxInput = new XboxInput (playerControllerId);
	}

	void colorImages(){
		foreach (Image img in imagesToColor) {
			img.color = currentColor;
		}
	}

	void Update () {

		float joyStickX = xboxInput.getXaxis ();

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
		if (Input.GetKeyDown (xboxInput.A) || Input.GetKeyDown (xboxInput.BStart)) {
			int currentReady = nbReady;
			//go to next state and update Debug.Loging
			if (currentState < maxState) {
				currentState++;

				if (currentState == maxState-1)
					nbReady++;
				if (currentState < maxState)
					text.text = textState [currentState];
			}
			//if there are enough player, show "Play" text
			if (currentReady <= 1 && nbReady >= GameVariables.minPlayers) {
				playText.enabled = true;
			}
			if (currentState == maxState && currentReady >= GameVariables.minPlayers) {
				SceneManager.LoadScene ("GameLoop"); 
			}
		}
		else if (Input.GetKeyDown (xboxInput.B)) {
			//Player wants to go back to previous menu
			if (currentState == 0) { 
				SceneManager.LoadScene (1);
			}
			//go to previous state and update Debug.Loging
			if (currentState > 0) {
				if (currentState == maxState-1)
					nbReady--;
				
				currentState--;
				text.text = textState [currentState];
			}
			//if there are not enough player, hide "Play" text
			if (nbReady < GameVariables.minPlayers) {
				playText.enabled = false;
			}
		}
	}

	void OnDestroy(){
		//when leaving the scene, add a player to game static variables
		if (currentState != 0 && currentState >= maxState-1) {
			GameVariables.players.Add (new Player (playerControllerId, currentColor));
			print ("YOLO");
		}
	}


}
