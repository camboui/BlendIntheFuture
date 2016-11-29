
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
	Transform thisTransform;

	void Start(){
		thisTransform = transform;
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
			if ((thisTransform.position - wayPoint).magnitude > 0.1) {
				Vector3 nextPos = (wayPoint - thisTransform.position).normalized * Speed * Time.deltaTime;	
				if (nextPos.x < 0)
					transform.localScale = new Vector3(-1,1,1);
				else
					transform.localScale = new Vector3(1,1,1);
				thisTransform.position += nextPos;
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
		wayPoint = new Vector3 (Random.Range (thisTransform.position.x - Range, thisTransform.position.x + Range), Random.Range (thisTransform.position.y - Range, thisTransform.position.y + Range), 0);
	}


}
