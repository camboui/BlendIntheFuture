using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class PlayerSelectionController : MonoBehaviour {

	public static int nbReady;
	private static Dictionary<int,Color> usedColors = new Dictionary<int,Color>();

	public AudioClip choice;
	public AudioClip validate;
	public AudioClip close;

	public int playerControllerId;

	public int nbPlayers;
	private bool changedRecently;
	private Color currentColor;
	private List<Image> imagesToColor;
	private GameObject bonusGO;
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
		imagesToColor.Add( transform.FindChild ("ColorSelection").GetComponentInChildren<Image> ());
		//imagesToColor.Add( transform.FindChild ("enabled").GetComponentInChildren<Image> ());
		bonusGO = transform.FindChild ("Bonus").gameObject;
		bonusImage = bonusGO.GetComponentInChildren<Image> ();
		colorImages ();
		bonusImages ();
		playText = GameObject.Find ("Play").GetComponent<Text>();

		if (nbReady < GameObject.Find ("CheckNewControllers").transform.GetComponent<CheckNewControllers> ().GetNbPlayers ()) {
			playText.enabled = false;
		}

		//Different states of validation 
		currentState = 0;
		text = transform.FindChild ("InstructionsPanel/Instructions").GetComponent<Text> ();
		textState = new List<string> (){ "Choose Color","Choose Bonus","Ready ?","Ready !"};
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
		Debug.Log ("Remove Color");
		bonusImage.color = currentBonus.GetComponent<Image> ().color;
		bonusImage.sprite = currentBonus.GetComponent<Image> ().sprite;
	}

	void Update () {

		float joyStickX = xboxInput.getXaxis ();

		//Color choice
		//prevent from infinite change
		if (currentState == 0) {
			if (joyStickX < 0.5f && joyStickX > -0.5f)
				changedRecently = false;

			//change color for player
			if (joyStickX == 1 && !changedRecently) {
				SoundManager.instance.RandomizeSfx (choice);
				currentColor = GameVariables.getNextColorRight (currentColor,usedColors);
				colorImages ();
				changedRecently = true;
			} else if (joyStickX == -1 && !changedRecently) {
				SoundManager.instance.RandomizeSfx (choice);
				currentColor = GameVariables.getNextColorLeft (currentColor,usedColors);
				colorImages ();
				changedRecently = true;
			}

			if (usedColors.ContainsValue(currentColor)) {
				currentColor = GameVariables.getNextColorRight (currentColor,usedColors);
				colorImages ();
			}
		}

		//Bonus Choice
		if (currentState == 1) {
			transform.FindChild ("Player").gameObject.SetActive(false);
			bonusGO.SetActive(true);
			if (joyStickX < 0.5f && joyStickX > -0.5f)
				changedRecently = false;

			//change color for player
			if (joyStickX == 1 && !changedRecently) {
				SoundManager.instance.RandomizeSfx (choice);
				currentBonus = GameVariables.getNextBonusRight (currentBonus);
				bonusImages ();
				changedRecently = true;
			} else if (joyStickX == -1 && !changedRecently) {
				SoundManager.instance.RandomizeSfx (choice);
				currentBonus = GameVariables.getNextBonusLeft (currentBonus);
				bonusImages ();
				changedRecently = true;
			}
		} else {
			bonusGO.SetActive(false);
		}
		if (currentState == 2 && !changedRecently) {
			transform.FindChild ("Both/Bonus").transform.GetComponent<Image>().sprite = currentBonus.GetComponent<Image> ().sprite;
			transform.FindChild ("Both").gameObject.SetActive (true);
		}
		if (currentState == (maxState -1) && !changedRecently) {
			transform.FindChild ("InstructionsPanel/Validate").gameObject.SetActive (false);
		}
		if (Input.GetKeyDown (xboxInput.A) || Input.GetKeyDown (xboxInput.BStart)) {
			SoundManager.instance.RandomizeSfx (validate);
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
			if (nbReady >= GameVariables.minPlayers && nbReady >= GameObject.Find ("CheckNewControllers").transform.GetComponent<CheckNewControllers> ().GetNbPlayers ()) {
				playText.enabled = true;
			}
			if (currentState == maxState && currentReady >= GameVariables.minPlayers && currentReady >= GameObject.Find ("CheckNewControllers").transform.GetComponent<CheckNewControllers> ().GetNbPlayers () ) {
				SceneManager.LoadScene ("ModeSelectionMenu"); 
			}

		}
		else if (Input.GetKeyDown (xboxInput.B)) {
			//Player wants to go back to previous menu
			if (currentState == 0) { 
				SoundManager.instance.RandomizeSfx (close);
				SceneManager.LoadScene (1);
			}
			//go to previous state and update Debug.Loging
			if (currentState > 0) {
				if (currentState == maxState - 1) {
					nbReady--;
				}

				currentState--;
				switch (currentState) {
				case 0:
					transform.FindChild ("Player").gameObject.SetActive(true);
					transform.FindChild ("InstructionsPanel/Validate").gameObject.SetActive (true);
					usedColors.Remove (playerControllerId);
					break;
				case 1:
					transform.FindChild ("InstructionsPanel/Validate").gameObject.SetActive (true);
					transform.FindChild ("Both").gameObject.SetActive (false);
					break;
				case 2:
					transform.FindChild ("InstructionsPanel/Validate").gameObject.SetActive (true);
					transform.FindChild ("Both").gameObject.SetActive (true);
					break;
				default:
					break;
				}
				text.text = textState [currentState];
			}
			//if there are not enough player, hide "Play" text
			if (nbReady < GameObject.Find ("CheckNewControllers").transform.GetComponent<CheckNewControllers> ().GetNbPlayers ()) {
				playText.enabled = false;
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