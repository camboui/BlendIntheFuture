using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour

{
	public int relatedController;
	private Vector3 movementVector;
	private float movementSpeed = 0.1f;

	private KeyCode A,B,X,Y,BStart,Select,LR,LT;

	void Start()
	{
		//See http://wiki.unity3d.com/index.php?title=Xbox360Controller for button number
		A = (KeyCode)System.Enum.Parse (typeof(KeyCode), "Joystick" + relatedController + "Button0");
		B = (KeyCode)System.Enum.Parse (typeof(KeyCode), "Joystick" + relatedController + "Button1");
		X = (KeyCode)System.Enum.Parse (typeof(KeyCode), "Joystick" + relatedController + "Button2");
		Y = (KeyCode)System.Enum.Parse (typeof(KeyCode), "Joystick" + relatedController + "Button3");
		LT = (KeyCode)System.Enum.Parse (typeof(KeyCode), "Joystick" + relatedController + "Button4");
		LR = (KeyCode)System.Enum.Parse (typeof(KeyCode), "Joystick" + relatedController + "Button5");
		Select = (KeyCode)System.Enum.Parse (typeof(KeyCode), "Joystick" + relatedController + "Button6");
		BStart = (KeyCode)System.Enum.Parse (typeof(KeyCode), "Joystick" + relatedController + "Button7");

	}



	void Update()
	{
		//X and Y axis are defined in Edit/Project Settings/Input
		movementVector.x = Input.GetAxis ("X_" + relatedController) * movementSpeed;
		movementVector.y = Input.GetAxis ("Y_" + relatedController) * movementSpeed;

		transform.position += movementVector;

		if (Input.GetKeyDown(A)) {
			print ("P"+relatedController+" : A");      
		}
		if (Input.GetKeyDown(B)) {
			print ("P"+relatedController+" : B");      
		}
		if (Input.GetKeyDown(X)) {
			print ("P"+relatedController+" : X");      
		}
		if (Input.GetKeyDown(Y)) {
			print ("P"+relatedController+" : Y");      
		}
		if (Input.GetKeyDown(LT)) {
			print ("P"+relatedController+" : LT");      
		}
		if (Input.GetKeyDown(LR)) {
			print ("P"+relatedController+" : LR");      
		}
		if (Input.GetKeyDown(Select)) {
			print ("P"+relatedController+" : Select");      
		}
		if (Input.GetKeyDown(BStart)) {
			print ("P"+relatedController+" : BStart");      
		}
		

	}

}
