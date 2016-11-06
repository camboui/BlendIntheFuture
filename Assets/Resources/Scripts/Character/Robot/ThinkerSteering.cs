using UnityEngine;
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
				transform.position += (wayPoint - transform.position).normalized * Speed * Time.deltaTime;	
			} else {
				ready = false;
				timer = Time.time+Random.Range (1,3);
				NextPoint ();
			}
		}
	}

	void NextPoint()
	{ 
		wayPoint = new Vector3 (Random.Range (transform.position.x - Range, transform.position.x + Range), Random.Range (transform.position.y - Range, transform.position.y + Range), 0);
	}


}
