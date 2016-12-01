using UnityEngine;
using System.Collections;

public abstract class SteeringAbstract : MonoBehaviour {

	protected float Speed = 1f;
	private Vector3 wayPoint;
	protected float timer;
	float delayTime=2f;
	protected bool isWaitingForNewPoint;
	private Vector3 leftOrientationScale;
	private Vector3 rightOrientationScale;

	// Use this for initialization
	protected void Start () {
		leftOrientationScale = transform.localScale;
		rightOrientationScale = transform.localScale;
		rightOrientationScale.x = rightOrientationScale.x * -1;

		wayPoint = NextPoint ();
		timer = Time.time + delayTime;
		if (Random.Range (0, 2) == 1)
			isWaitingForNewPoint = true;
		else
			isWaitingForNewPoint = false;
	}
	
	void FixedUpdate()
	{
		if (!isWaitingForNewPoint && Time.time >= timer)
			isWaitingForNewPoint = true;

		if (isWaitingForNewPoint) {
			if ((transform.position - wayPoint).magnitude > 3) {
				Vector3 nextPos = (wayPoint - transform.position).normalized * Speed * Time.deltaTime;	
				transform.position += nextPos;
				if (nextPos.x < 0)
					transform.localScale = rightOrientationScale;
				else
					transform.localScale = leftOrientationScale;
			} else {
				wayPoint = NextPoint ();
			}
		}
	}

	protected abstract Vector3 NextPoint ();

}
