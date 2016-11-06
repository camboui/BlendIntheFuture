using UnityEngine;
using System.Collections;

public class XboxInput  {

	private int id;
	public KeyCode A,B,X,Y,BStart,Select,LR,LT;

	public XboxInput(int playerId){
		id = playerId;
		//See http://wiki.unity3d.com/index.php?title=Xbox360Controller for button number
		A = (KeyCode)System.Enum.Parse (typeof(KeyCode), "Joystick" + playerId + "Button0");
		B = (KeyCode)System.Enum.Parse (typeof(KeyCode), "Joystick" + playerId + "Button1");
		X = (KeyCode)System.Enum.Parse (typeof(KeyCode), "Joystick" + playerId + "Button2");
		Y = (KeyCode)System.Enum.Parse (typeof(KeyCode), "Joystick" + playerId + "Button3");
		LT = (KeyCode)System.Enum.Parse (typeof(KeyCode), "Joystick" + playerId + "Button4");
		LR = (KeyCode)System.Enum.Parse (typeof(KeyCode), "Joystick" + playerId + "Button5");
		Select = (KeyCode)System.Enum.Parse (typeof(KeyCode), "Joystick" + playerId + "Button6");
		BStart = (KeyCode)System.Enum.Parse (typeof(KeyCode), "Joystick" + playerId + "Button7");
	}

	public float getXaxis(){
		return Input.GetAxis ("X_" + id);
	}

	public float getYaxis(){
		return Input.GetAxis ("Y_" + id);
	}

}

