using UnityEngine;
using System.Collections;

public abstract class SteeringAbstract : MonoBehaviour {

	public float Speed;
	private Vector3 wayPoint;
	protected float timer;
	float delayTime=2f;
	protected bool isWaitingForNewPoint;
	private Vector3 leftOrientationScale;
	private Vector3 rightOrientationScale;
	protected Animator[] animators;
	private Rigidbody2D rgdby;

	// Use this for initialization
	protected void Start () {
		leftOrientationScale = transform.localScale;
		rightOrientationScale = transform.localScale;
		rightOrientationScale.x = rightOrientationScale.x * -1;

		Speed = GameVariables.charactersSpeed;
		rgdby = gameObject.GetComponent<Rigidbody2D> ();

		wayPoint = NextPoint ();
		timer = Time.time + delayTime;
		if (Random.Range (0, 2) == 1)
			isWaitingForNewPoint = true;
		else
			isWaitingForNewPoint = false;

		animators = transform.FindChild("Renderers").GetComponentsInChildren <Animator>();
	}
	
	void Update()
	{
		if (!isWaitingForNewPoint && Time.time >= timer) {
			isWaitingForNewPoint = true;
		}

		if (isWaitingForNewPoint) {
			changeAllAnimatorsBool("isWalking", true);
			if ((transform.position - wayPoint).magnitude > 3) {
				Vector3 nextPos = (wayPoint - transform.position).normalized * Speed * Time.deltaTime;	
				rgdby.MovePosition (transform.position + nextPos);
				if (nextPos.x < 0)
					transform.localScale = rightOrientationScale;
				else
					transform.localScale = leftOrientationScale;
			} else {
				changeAllAnimatorsBool("isWalking", false);
				wayPoint = NextPoint ();
			}
		}
	}


	private void changeAllAnimatorsBool(string boolname, bool value)
	{
		foreach (Animator anm in animators) {
			anm.SetBool (boolname,value);
		}
	}


	protected abstract Vector3 NextPoint ();

}
