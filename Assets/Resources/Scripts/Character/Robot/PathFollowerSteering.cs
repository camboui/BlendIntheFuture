using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFollowerSteering : MonoBehaviour {

	//TODO make classes for game data
	float Speed = 1f;
	Vector3 wayPoint;
	float timer;
	float delayTime=2f;
	bool ready;
	List<Vector3> path;
	int currentPoint = -1;

	void Start(){

		int nbPointInPath = Random.Range (3, 5);
		path = new List<Vector3>();
		for (int i = 0; i < nbPointInPath; i++) {
			path.Add (GameCreator.randomPosOnMap());
		}
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
			if ((transform.position - wayPoint).magnitude > 1) {
				Vector3 nextPos = (wayPoint - transform.position).normalized * Speed * Time.deltaTime;	
				transform.position += nextPos;
				if (nextPos.x < 0)
					transform.localScale = new Vector3(-1,1,1);
				else
					transform.localScale = new Vector3(1,1,1);
			} else {
				ready = false;
				timer = 0;
				NextPoint ();
			}
		}
	}

	void NextPoint()
	{ 
		currentPoint++;
		if (currentPoint == path.Count) {
			currentPoint = 0;
		}
		wayPoint = path [currentPoint];
	}


}

