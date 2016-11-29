﻿using UnityEngine;
using System.Collections;

public class ThinkerSteering : MonoBehaviour {

	//TODO make classes for game data
	float Speed = 1f;
	Vector3 wayPoint;
	float Range = 10;
	float timer;
	float delayTime=2f;
	bool ready;

	void Start(){
		NextPoint ();
		timer = Time.time + delayTime;
		if (Random.Range (0, 2) == 1)
			ready = true;
		else
			ready = false;
	}

	void FixedUpdate()
	{
		if (!ready && Time.time >= timer)
			ready = true;

		if (ready) {
			if ((transform.position - wayPoint).magnitude > 3) {
				Vector3 nextPos = (wayPoint - transform.position).normalized * Speed * Time.deltaTime;	
				transform.position += nextPos;
				if (nextPos.x < 0)
					transform.localScale = new Vector3(-1,1,1);
				else
					transform.localScale = new Vector3(1,1,1);
			} else {
				ready = false;
				timer = Time.time+Random.Range (0f,3f);
				NextPoint ();
			}
		}
	}

	void NextPoint()
	{
		//TODO change this to make AI move only on map
		wayPoint = new Vector3 (Random.Range (transform.position.x - Range, transform.position.x + Range), Random.Range (transform.position.y - Range, transform.position.y + Range), 0);
	}


}
