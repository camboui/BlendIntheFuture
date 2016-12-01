using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Weapon_Abstract : MonoBehaviour {

	protected Transform rendererContainer;
	protected List<GameObject> weaponCopies;
	protected float reloadTime;
	private bool isReadyToUse;

	protected void setVariables(float reloadTime,Transform rendererContainer)
	{
		this.isReadyToUse = true;
		this.reloadTime = reloadTime;
		this.rendererContainer = rendererContainer;

		this.weaponCopies = new List<GameObject>();
		foreach (Transform child in rendererContainer) {
			weaponCopies.Add (child.FindChild ("Weapon").gameObject);
		}
	}

	public void use(){
		if (isReadyToUse) {
			isReadyToUse = false;
			useEffect ();
			StartCoroutine (startReloading());

		}
	}

	public abstract void initialiseWeapon (float reloadTime, Transform rendererContainer);

	protected abstract void useEffect ();

	protected abstract void endUseEffect ();

	protected abstract void OnTriggerEnter2D (Collider2D other);


	private IEnumerator startReloading()
	{
		yield return new WaitForSeconds (reloadTime);
		endUseEffect ();
		isReadyToUse = true;
	}
}
