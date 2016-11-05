using UnityEngine;
using System.Collections;

public class AI_Wandering : MonoBehaviour {

	//TODO make classes for game data
	float Speed = 1f;
	Vector3 wayPoint;
	float Range = 10;
	float timer;
	float delayTime=2f;
	bool ready;

	void Start(){
		Wander ();
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
				Wander ();
			}
		}
	}

	void Wander()
	{ 
		wayPoint = new Vector3 (Random.Range (transform.position.x - Range, transform.position.x + Range), Random.Range (transform.position.z - Range, transform.position.z + Range), 0);
	}


}
