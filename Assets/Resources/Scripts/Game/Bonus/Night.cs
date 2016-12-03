using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Night : Bonus_Abstract {


	public float emergenceTime;
	public float nightTime;
	public float dissipationTime;

	public GameObject nightGO;

	private float opacity;
	private float currentTime;
	private SpriteRenderer nightSprite;


	// Use this for initialization
	void Start () {
		Debug.Log ("Night Start");
		opacity = 0;
		currentTime = 0;
		nightGO = GameObject.Find ("NightImage");
		nightSprite = nightGO.GetComponent<SpriteRenderer> ();
		nightSprite.color = new Color (nightSprite.color.r, nightSprite.color.g, nightSprite.color.b, opacity);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("Night Update");
		currentTime += Time.deltaTime;
		if (currentTime <= emergenceTime) {
			Debug.Log ("Emergence");
			opacity = currentTime / emergenceTime;
		}
		if (currentTime > emergenceTime && currentTime <= (emergenceTime + nightTime)) {
			opacity = 1;
			Debug.Log ("Dark");
		}
		if (currentTime > (emergenceTime + nightTime) && currentTime <= (emergenceTime + nightTime + dissipationTime)) {
			opacity = 1 - (currentTime - emergenceTime - nightTime) / dissipationTime;
			Debug.Log ("Dissipation");
		} 
		if (currentTime > (emergenceTime + nightTime + dissipationTime)) {
			opacity = 0;
			Debug.Log ("end");
			nightSprite.color = new Color (nightSprite.color.r, nightSprite.color.g, nightSprite.color.b, opacity);
			this.enabled = false;
		}
		nightSprite.color = new Color (nightSprite.color.r, nightSprite.color.g, nightSprite.color.b, opacity);
	}
}
