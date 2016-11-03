using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour

{
	public int playerId;
	private Vector3 movementVector;
	private float movementSpeed = 0.1f;

	private KeyCode A,B,X,Y,BStart,Select,LR,LT;

	void Start()
	{
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



	void Update()
	{
		//X and Y axis are defined in Edit/Project Settings/Input
		movementVector.x = Input.GetAxis ("X_" + playerId) * movementSpeed;
		movementVector.y = Input.GetAxis ("Y_" + playerId) * movementSpeed;

		transform.position += movementVector;
		if (Input.GetKeyDown(A)) {
			print ("P"+playerId+" : A");      
		}
		if (Input.GetKeyDown(B)) {
			print ("P"+playerId+" : B");      
		}
		if (Input.GetKeyDown(X)) {
			print ("P"+playerId+" : X");      
		}
		if (Input.GetKeyDown(Y)) {
			print ("P"+playerId+" : Y");      
		}
		if (Input.GetKeyDown(LT)) {
			print ("P"+playerId+" : LT");      
		}
		if (Input.GetKeyDown(LR)) {
			print ("P"+playerId+" : LR");      
		}
		if (Input.GetKeyDown(Select)) {
			print ("P"+playerId+" : Select");      
		}
		if (Input.GetKeyDown(BStart)) {
			print ("P"+playerId+" : BStart");      
		}
		

	}

}
