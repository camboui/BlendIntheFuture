using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFollowerSteering : SteeringAbstract {

	private List<Vector3> path;
	private int currentPoint = -1;

	new void Start(){
		int nbPointInPath = Random.Range (3, 5);
		path = new List<Vector3>();
		for (int i = 0; i < nbPointInPath; i++) {
			path.Add (GameCreator.randomPosOnMap());
		}
		base.Start ();
	}

	#region implemented abstract members of SteeringAbstract

	protected override Vector3 NextPoint()
	{
		isWaitingForNewPoint = false;
		currentPoint++;
		if (currentPoint == path.Count) {
			currentPoint = 0;
		}
		return path [currentPoint];
	}

	#endregion

}

