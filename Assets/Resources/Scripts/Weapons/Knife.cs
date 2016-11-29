using UnityEngine;
using System.Collections;
using System;

public class Knife : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		GameObject parentGO = other.transform.parent.parent.gameObject;
		if (parentGO != null) {
			print ("HIT : " + parentGO.name);
			if (parentGO.tag.Equals ("AI")) {
				Destroy (parentGO);
			} else if (parentGO.tag.Equals ("Player")) {
				Destroy (parentGO);
			}
		}
	}
}
