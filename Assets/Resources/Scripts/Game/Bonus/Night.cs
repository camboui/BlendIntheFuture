﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Night : Bonus_Abstract {


	public float emergenceTime;
	public float nightTime;
	public float dissipationTime;

	public Image nightImage;

	private float opacity;
	private float currentTime;
	private Color nightColor;



	// Use this for initialization
	void Start () {
		opacity = 255;
		currentTime = 0;
		nightColor = nightImage.GetComponent<SpriteRenderer>().color;
		nightColor.a = opacity;
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;
		if (currentTime <= emergenceTime) {
			opacity = 255*(1 - (currentTime / emergenceTime));
		}
		if (currentTime > emergenceTime && currentTime <= (emergenceTime + nightTime)) {
			opacity = 0;
		}
		if (currentTime > (emergenceTime + nightTime) && currentTime <= (emergenceTime + nightTime + dissipationTime)) {
			opacity = 255*(currentTime - emergenceTime - nightTime) / dissipationTime;
		} else {
			opacity = 1;
		}
		nightColor.a = opacity;
	}
}