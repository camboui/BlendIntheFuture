using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Knife : MonoBehaviour {

	public Human humanOwner;

	void Start()
	{
		this.humanOwner =transform.parent.parent.parent.parent.GetComponent<HumanController> ().human;
	}
		
	protected void OnTriggerEnter2D(Collider2D other) {
		GameObject parentGO = other.transform.parent.parent.gameObject;
		if (parentGO != null ) {
			print ("HIT : " + parentGO.name);
			if (parentGO.tag.Equals ("AI")) {
				Destroy (parentGO);
			} else if (parentGO.tag.Equals ("Player")) {
				humanOwner.getKilledThisRound ().Add (parentGO.GetComponent<HumanController> ().human.getJoystickId ());
				Destroy (parentGO);
				Destroy (gameObject);
			}
		}
	}



}
