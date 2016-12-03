using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Knife : Weapon_Abstract {

	#region implemented abstract members of Weapon_Abstract
	public override void initialiseWeapon (float reloadTime, Transform rendererContainer)
	{
		base.setVariables (reloadTime, rendererContainer);
		//Can add stuff here
	}


	protected override void useEffect ()
	{
		foreach (GameObject weapon in weaponCopies) {
			weapon.SetActive (true);
		}
	}

	protected override void endUseEffect ()
	{
		foreach (GameObject weapon in weaponCopies) {
			weapon.SetActive (false);
		}
	}
		
	protected override void OnTriggerEnter2D(Collider2D other) {
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
	#endregion


}
