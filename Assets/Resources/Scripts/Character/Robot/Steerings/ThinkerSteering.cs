using UnityEngine;
using System.Collections;

public class ThinkerSteering : SteeringAbstract {

	float range = 10;
	float thinkTime = 3f;

	#region implemented abstract members of SteeringAbstract

	protected override Vector3 NextPoint()
	{
		timer = Time.time + Random.Range (0f,thinkTime);
		Vector3 next = GameCreator.randomPosOnMap ();
		next.x += Random.Range (-range, range);
		isWaitingForNewPoint = false;
		return next;
	}
		
	#endregion
}
