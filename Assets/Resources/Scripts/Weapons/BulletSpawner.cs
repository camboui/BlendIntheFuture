using UnityEngine;
using System.Collections;

public class BulletSpawner : MonoBehaviour {

	public Transform playerParent;
	private Transform bulletSpawn;
	public GameObject bulletPrefab;
	private Human humanOwner;

	void Start () {
		bulletSpawn = transform.parent.FindChild ("BulletSpawn");
		humanOwner = transform.GetComponentInParent<HumanController> ().human;
		if (!transform.parent.name.Equals ("mainRenderer")) {
			Destroy (bulletSpawn.gameObject);
		}
	}

	public void spawnBullet () {
		if (bulletSpawn != null) {
			GameObject bullet = Instantiate (bulletPrefab) as GameObject;
			bullet.transform.position = bulletSpawn.position;
			bullet.GetComponentInChildren<Bullet> ().initialiseBullet (humanOwner, Mathf.Sign (playerParent.localScale.x) * 5.0f);
		}
	}
}
