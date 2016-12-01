
using UnityEngine;
using System.Collections;

public class WanderSteering : SteeringAbstract {

	float range = 1;

	#region implemented abstract members of SteeringAbstract

	protected override Vector3 NextPoint()
	{
		isWaitingForNewPoint = false;
		return new Vector3 (Random.Range (transform.position.x - range, transform.position.x + range), Random.Range (transform.position.y - range, transform.position.y + range), 0);
	}

	#endregion


}
