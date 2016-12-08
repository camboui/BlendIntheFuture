using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SmokeBall : Bonus_Abstract {

	public GameObject player;
	public AudioClip smoke;

	// Use this for initialization
	void Start () {
		gameObject.GetComponentInChildren<ParticleSystem> ().transform.position = 
			new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z-1);
		SoundManager.instance.RandomizeSfx (smoke);
		gameObject.GetComponentInChildren<ParticleSystem> ().Play (); 
	}

	// Update is called once per frame
	void Update () {
		
	}
}
