
using UnityEngine;
using System.Collections;

public class WanderSteering : MonoBehaviour {

	//TODO make classes for game data
	float Speed = 1f;
	Vector3 wayPoint;
	float Range = 1;
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
			if ((transform.position - wayPoint).magnitude > 0.1) {
				transform.position += (wayPoint - transform.position).normalized * Speed * Time.deltaTime;	
			} else {
				ready = false;
				timer = 0;
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
