using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class PlayerSelectionController : MonoBehaviour {

	public static int nbReady;
	private static Dictionary<int,Color> usedColors = new Dictionary<int,Color>();

	public int playerControllerId;

	private bool changedRecently;
	private Color currentColor;
	private List<Image> imagesToColor;
	private Image bonusImage;
	private Text text;
	private Text playText;
	private int currentState;
	private GameObject currentBonus;
	private int maxState;
	private List<String> textState;

	private XboxInput xboxInput;

	void Start()
	{
		currentColor = GameVariables.availableColors [0];
		currentBonus = GameVariables.bonus [0];
		changedRecently = false;

		//All images which need to be recolored according to player selection
		imagesToColor = new List<Image> ();
		imagesToColor.Add( transform.FindChild ("Background").GetComponentInChildren<Image> ());
		imagesToColor.Add( transform.FindChild ("enabled").GetComponentInChildren<Image> ());
		bonusImage = transform.FindChild ("Bonus").GetComponentInChildren<Image> ();
		colorImages ();
		bonusImages ();
		playText = GameObject.Find ("Play").GetComponent<Text>();

		//Different states of validation 
		currentState = 0;
		text = transform.FindChild ("Press A").GetComponent<Text> ();
		textState = new List<string> (){ "Choose Color (A)","Choose Bonus (A)","Ready ? (A)","Ready !"};
		maxState = textState.Count;
		text.text = textState [currentState];

		xboxInput = new XboxInput (playerControllerId);
	}

	void colorImages(){
		foreach (Image img in imagesToColor) {
			img.color = currentColor;
		}
	}

	void bonusImages(){
		bonusImage = currentBonus.GetComponent<Image> ();
	}

	void Update () {

		float joyStickX = xboxInput.getXaxis ();

		//prevent from infinite change
		if (currentState == 0) {
			if (joyStickX < 0.5f && joyStickX > -0.5f)
				changedRecently = false;

			//change color for player
			if (joyStickX == 1 && !changedRecently) {
				currentColor = GameVariables.getNextColorRight (currentColor,usedColors);
				colorImages ();
				changedRecently = true;
			} else if (joyStickX == -1 && !changedRecently) {
				currentColor = GameVariables.getNextColorLeft (currentColor,usedColors);
				colorImages ();
				changedRecently = true;
			}

			if (usedColors.ContainsValue(currentColor)) {
				currentColor = GameVariables.getNextColorRight (currentColor,usedColors);
				colorImages ();
			}
		}
		if (currentState == 1) {
			if (joyStickX < 0.5f && joyStickX > -0.5f)
				changedRecently = false;

			//change color for player
			if (joyStickX == 1 && !changedRecently) {
				currentBonus = GameVariables.getNextBonusRight (currentBonus);
				bonusImages ();
				changedRecently = true;
			} else if (joyStickX == -1 && !changedRecently) {
				currentBonus = GameVariables.getNextBonusLeft (currentBonus);
				bonusImages ();
				changedRecently = true;
			}
		}
		if (Input.GetKeyDown (xboxInput.A) || Input.GetKeyDown (xboxInput.BStart)) {
			int currentReady = nbReady;
			//go to next state and update Debug.Loging
			if (currentState < maxState) {
				currentState++;

				if(!usedColors.ContainsValue(currentColor))
					usedColors.Add (playerControllerId, currentColor);

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
				SceneManager.LoadScene ("ModeSelectionMenu"); 
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
				usedColors.Remove (playerControllerId);
			}
		}
	}

	void OnDestroy(){
		//when leaving the scene, add a player to game static variables
		usedColors.Clear();
		if (currentState != 0 && currentState >= maxState-1) {
			GameVariables.players.Add (new Human (playerControllerId, currentColor, currentBonus));
		}
	}


}

