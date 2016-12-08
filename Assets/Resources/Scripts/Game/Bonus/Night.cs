using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Night : Bonus_Abstract {


	public float emergenceTime;
	public float nightTime;
	public float dissipationTime;

	public AudioClip night;

	public GameObject nightGO;

	private float opacity;
	private float currentTime;
	private SpriteRenderer nightSprite;


	// Use this for initialization
	void Start () {
		opacity = 0;
		currentTime = 0;
		nightGO = GameObject.Find ("NightImage");
		nightSprite = nightGO.GetComponent<SpriteRenderer> ();
		nightSprite.color = new Color (nightSprite.color.r, nightSprite.color.g, nightSprite.color.b, opacity);
		SoundManager.instance.RandomizeSfx (night);
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;
		if (currentTime <= emergenceTime) {
			opacity = currentTime / emergenceTime;
		}
		if (currentTime > emergenceTime && currentTime <= (emergenceTime + nightTime)) {
			opacity = 1;
		}
		if (currentTime > (emergenceTime + nightTime) && currentTime <= (emergenceTime + nightTime + dissipationTime)) {
			opacity = 1 - (currentTime - emergenceTime - nightTime) / dissipationTime;
		} 
		if (currentTime > (emergenceTime + nightTime + dissipationTime)) {
			opacity = 0;
			nightSprite.color = new Color (nightSprite.color.r, nightSprite.color.g, nightSprite.color.b, opacity);
			this.enabled = false;
		}
		nightSprite.color = new Color (nightSprite.color.r, nightSprite.color.g, nightSprite.color.b, opacity);
	}
}
