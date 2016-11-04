using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour

{
	public int playerId;
	private Vector3 movementVector;
	private float movementSpeed = 0.1f;

	private XboxInput xboxInput;
	void Start()
	{
		xboxInput = new XboxInput(playerId);
	}
		

	void Update()
	{
		//X and Y axis are defined in Edit/Project Settings/Input
		movementVector.x = xboxInput.getXaxis() * movementSpeed;
		movementVector.y = xboxInput.getYaxis() * movementSpeed;

		transform.position += movementVector;
		if (Input.GetKeyDown(xboxInput.A)) {
			print ("P"+playerId+" : A");      
		}
		if (Input.GetKeyDown(xboxInput.B)) {
			print ("P"+playerId+" : B");      
		}
		if (Input.GetKeyDown(xboxInput.X)) {
			print ("P"+playerId+" : X");      
		}
		if (Input.GetKeyDown(xboxInput.Y)) {
			print ("P"+playerId+" : Y");      
		}
		if (Input.GetKeyDown(xboxInput.LT)) {
			print ("P"+playerId+" : LT");      
		}
		if (Input.GetKeyDown(xboxInput.LR)) {
			print ("P"+playerId+" : LR");      
		}
		if (Input.GetKeyDown(xboxInput.Select)) {
			print ("P"+playerId+" : Select");      
		}
		if (Input.GetKeyDown(xboxInput.BStart)) {
			print ("P"+playerId+" : BStart");      
		}
		

	}

}
