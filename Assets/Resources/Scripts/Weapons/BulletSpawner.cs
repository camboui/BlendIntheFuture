using UnityEngine;
using System.Collections;

public class BulletSpawner : MonoBehaviour {
	
	private Transform bulletSpawn;
	public GameObject bulletPrefab;
	private Human humanOwner;

	void Start () {
		bulletSpawn = transform.FindChild ("BulletSpawn").transform;
		humanOwner = transform.GetComponentInParent<HumanController> ().human;
	}

	public void spawnBullet () {
		GameObject bullet = Instantiate(bulletPrefab) as GameObject;
		bullet.transform.position = bulletSpawn.position;
		bullet.GetComponentInChildren<Bullet> ().initialiseBullet (humanOwner, Mathf.Sign(transform.localScale.x)* 5.0f);
	}
}
