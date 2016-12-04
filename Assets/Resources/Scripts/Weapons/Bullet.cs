using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	private float bulletSpeed;
	public Human humanOwner;


	public void initialiseBullet(Human humanOwner,float bulletSpeed)
	{
		this.humanOwner = humanOwner;
		this.bulletSpeed = bulletSpeed;
		transform.parent.parent.GetComponent<Rigidbody2D> ().velocity = new Vector2 (bulletSpeed, 0);
	}

	protected void OnTriggerEnter2D(Collider2D other) {

		GameObject parentGO = findParent(other.transform);
		if (parentGO != null ) {
			print ("BULLET HIT : " + parentGO.name);
			if (parentGO.tag.Equals ("AI")) {
				Destroy (parentGO);
				Destroy (transform.parent.parent.gameObject);
			} else if (parentGO.tag.Equals ("Player")) {
				humanOwner.getKilledThisRound ().Add (parentGO.GetComponent<HumanController> ().human.getJoystickId ());
				Destroy (parentGO);
				Destroy (transform.parent.parent.gameObject);
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
