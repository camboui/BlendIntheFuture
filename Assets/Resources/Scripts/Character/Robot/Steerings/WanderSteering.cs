
using UnityEngine;
using System.Collections;

public class WanderSteering : SteeringAbstract {

	float range = 10;

	#region implemented abstract members of SteeringAbstract

	protected override Vector3 NextPoint()
	{
		Vector3 next = GameCreator.randomPosOnMap ();
		next.x += Random.Range (-range, range);
		isWaitingForNewPoint = false;
		return next;
	}

	#endregion


}
