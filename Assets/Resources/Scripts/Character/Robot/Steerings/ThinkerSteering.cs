using UnityEngine;
using System.Collections;

public class ThinkerSteering : SteeringAbstract {

	float range = 10;
	float thinkTime=3f;

	#region implemented abstract members of SteeringAbstract

	protected override Vector3 NextPoint()
	{
		timer = Time.time + Random.Range (0f,thinkTime);
		isWaitingForNewPoint = false;
		return new Vector3 (Random.Range (transform.position.x - range, transform.position.x + range), Random.Range (transform.position.y - range, transform.position.y + range), 0);
	}
		
	#endregion
}
