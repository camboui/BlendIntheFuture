using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerSelectionController : MonoBehaviour {

	public static int nbReady;
	public int playerControllerId;

	private bool changedRecently;
	private bool colorChoosen;
	public bool isReady;

	private KeyCode A,B,X,Y,BStart,Select,LR,LT;
	private Color currentColor;
	private List<Image> imagesToColor;
	private Text text;
	private Text playText;

	void Start()
	{
		currentColor = GameVariables.availableColors [0];
		colorChoosen = false;
		isReady = false;
		changedRecently = false;

		imagesToColor = new List<Image> ();
		imagesToColor.Add( transform.FindChild ("Background").GetComponentInChildren<Image> ());
		imagesToColor.Add( transform.FindChild ("enabled").GetComponentInChildren<Image> ());
		colorImages ();

		playText = GameObject.Find ("Play").GetComponent<Text>();

		text = transform.FindChild ("Press A").GetComponent<Text> ();
		text.text = "Choose Color (A)";

		//See http://wiki.unity3d.com/index.php?title=Xbox360Controller for button number
		A = (KeyCode)System.Enum.Parse (typeof(KeyCode), "Joystick" + playerControllerId + "Button0");
		B = (KeyCode)System.Enum.Parse (typeof(KeyCode), "Joystick" + playerControllerId + "Button1");
		X = (KeyCode)System.Enum.Parse (typeof(KeyCode), "Joystick" + playerControllerId + "Button2");
		Y = (KeyCode)System.Enum.Parse (typeof(KeyCode), "Joystick" + playerControllerId + "Button3");
		LT = (KeyCode)System.Enum.Parse (typeof(KeyCode), "Joystick" + playerControllerId + "Button4");
		LR = (KeyCode)System.Enum.Parse (typeof(KeyCode), "Joystick" + playerControllerId + "Button5");
		Select = (KeyCode)System.Enum.Parse (typeof(KeyCode), "Joystick" + playerControllerId + "Button6");
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
		if (!colorChoosen) {
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
		//validate in 2 times
		if (Input.GetKeyDown (A)) {
			int currentReady = nbReady;
			if (!colorChoosen) {
				colorChoosen = true;
				text.text = "Ready ? (A)";
			} else if (!isReady) {
				isReady = true;
				text.text = "Ready !";
				nbReady++;
			} 
			if (currentReady<=1 && nbReady >= 2) {
				playText.enabled = true;
			}
			if (currentReady >= 2) {
				SceneManager.LoadScene ("GameLoop"); //TODO change this
			}
		}
		//invalidate in 2 times
		if (Input.GetKeyDown (B)) {
			if (isReady) {
				isReady = false;
				text.text = "Ready ? (A)";
				nbReady--;
			} else if (colorChoosen) {
				colorChoosen = false;
				text.text = "Choose Color (A)";
			}
			if (nbReady < 2) {
				playText.enabled = false;
			}
		}
	}

	void OnDestroy(){
		GameVariables.players.Add (new Player (playerControllerId,currentColor));
	}


}
