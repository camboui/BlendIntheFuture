using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour

{
	public int relatedController;
	private Vector3 movementVector;
	private float movementSpeed = 0.1f;


	void Start()
	{
		 
	}



	void Update()
	{
		movementVector.x = Input.GetAxis("X_"+relatedController) * movementSpeed;
		movementVector.y = Input.GetAxis("Y_"+relatedController) * movementSpeed;

		if(Input.GetButtonDown ("A_"+relatedController))
			print ("P"+relatedController+" : A");
		if(Input.GetButtonDown ("B_"+relatedController))
			print ("P"+relatedController+" : B");

		if(Input.GetButtonDown ("Start_"+relatedController))
			print ("P"+relatedController+" : Start");

		transform.position += movementVector;
	}

}
