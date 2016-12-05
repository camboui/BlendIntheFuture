using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Knife : MonoBehaviour {

	public Human humanOwner;

	void Start()
	{
		this.humanOwner =transform.parent.parent.parent.parent.GetComponent<HumanController> ().human;
		if (!transform.parent.parent.name.Equals ("mainRenderer")) {
			Destroy (gameObject);
		}
	}
		
	protected void OnTriggerEnter2D(Collider2D other) {
		GameObject parentGO =  findParent(other.transform);
		if (parentGO != null ) {
			print ("HIT : " + parentGO.name);
			if (parentGO.tag.Equals ("AI")) {
				humanOwner.AIKilled ();
				Destroy (parentGO);
			} else if (parentGO.tag.Equals ("Player")) {
				humanOwner.getKilledThisRound ().Add (parentGO.GetComponent<HumanController> ().human.getJoystickId ());
				Destroy (parentGO);
			}
		}
	}

	private GameObject findParent(Transform current)
	{
		if (current == null)
			return null;

		if (current.tag.Equals ("AI") || current.tag.Equals ("Player"))
			return current.gameObject;

		return findParent (current.parent);
	}

}
