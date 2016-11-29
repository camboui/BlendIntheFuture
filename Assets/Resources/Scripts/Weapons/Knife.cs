using UnityEngine;
using System.Collections;
using System;

public class Knife : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D other) {
		print (other.transform.name + " ENTER");
		GameObject parentGO = other.transform.parent.parent.gameObject;
		if (parentGO != null) {
			if (parentGO.tag.Equals ("AI")) {
				Destroy (parentGO);
			} else if (parentGO.tag.Equals ("Player")) {
				Destroy (parentGO);
			}
		}
	}




}
