using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerSelectionController : MonoBehaviour {

	public int playerControllerId;

	private bool changedRecently;
	private bool colorChoosen;
	private bool isReady;
	private KeyCode A,B,X,Y,BStart,Select,LR,LT;
	private Color currentColor;
	private Image img;

	void Start()
	{
		currentColor = GameVariables.availableColors [0];
		colorChoosen = false;
		isReady = false;
		changedRecently = false;

		img = transform.GetComponentInChildren<Image> ();
		img.color = currentColor;

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

	void Update () {

		float joyStickX = Input.GetAxis ("X_" + playerControllerId);

		//prevent from infinite change
		if(joyStickX < 0.5f && joyStickX >-0.5f)
			changedRecently = false;
		
		//change color for player
		if (joyStickX == 1 && !changedRecently) {
			currentColor = GameVariables.getNextColorRight (currentColor);
			img.color = currentColor;
			changedRecently = true;
		} else if (joyStickX ==-1 && !changedRecently) {
			currentColor = GameVariables.getNextColorLeft (currentColor);
			img.color = currentColor;
			changedRecently = true;
		}
		//validate in 2 times
		if (Input.GetKeyDown (A)) {
			if (!colorChoosen) {
				colorChoosen = true;
			} else if (!isReady) {
				isReady = true;
			} 
		}
		//invalidate in 2 times
		if (Input.GetKeyDown (B)) {
			if (isReady) {
				isReady = false;
			} else if (colorChoosen) {
				colorChoosen = false;
			}
		}
	}

	void OnDestroy(){
		GameVariables.players.Add (new Player (playerControllerId,currentColor));
	}


}
