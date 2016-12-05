using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class SteeringAbstract : MonoBehaviour {

	[SerializeField]
	protected Vector3 groundOffset;
	public float Speed;
	private Vector3 wayPoint;
	protected float timer;
	float delayTime=2f;
	protected bool isWaitingForNewPoint;
	private Vector3 leftOrientationScale;
	private Vector3 rightOrientationScale;
	public List<Animator> animatorsBody;
	public List<Animator> animatorsArm;
	private Rigidbody2D rgdby;

	// Use this for initialization
	protected void Start () {
		leftOrientationScale = transform.localScale;
		rightOrientationScale = transform.localScale;
		rightOrientationScale.x = rightOrientationScale.x * -1;

		Speed = GameVariables.charactersSpeed;
		rgdby = gameObject.GetComponent<Rigidbody2D> ();

		Transform rendererContainer = transform.FindChild("Renderers");
		groundOffset= transform.FindChild ("GroundCheck").transform.localPosition;

		animatorsBody = new List<Animator> ();
		animatorsArm = new List<Animator> ();

		for (int i = 0; i < rendererContainer.childCount; i++) {
			animatorsBody.Add (rendererContainer.GetChild (i).GetComponent<Animator> ());
			animatorsArm.Add (rendererContainer.GetChild (i).FindChild("arm").GetComponent<Animator> ());
		}

		wayPoint = NextPoint ();
		timer = Time.time + delayTime;
		if (Random.Range (0, 2) == 1)
			isWaitingForNewPoint = true;
		else
			isWaitingForNewPoint = false;
	}
	
	void Update()
	{
		if (!isWaitingForNewPoint && Time.time >= timer) {
			isWaitingForNewPoint = true;
		}

		if (isWaitingForNewPoint) {
			changeAllAnimatorsBool(animatorsBody,"isWalking", true);
			changeAllAnimatorsBool(animatorsArm,"isWalking", true);
			if ((transform.position - wayPoint).magnitude > 3) {
				Vector3 nextPos = (wayPoint - transform.position).normalized * Speed * Time.deltaTime;	
				rgdby.MovePosition (transform.position + nextPos);
				if (nextPos.x < 0)
					transform.localScale = rightOrientationScale;
				else
					transform.localScale = leftOrientationScale;
			} else {
				wayPoint = NextPoint ();
				if (Time.time < timer) {
					changeAllAnimatorsBool (animatorsBody, "isWalking", false);
					changeAllAnimatorsBool (animatorsArm, "isWalking", false);
				}
			}
		}
	}


	protected void changeAllAnimatorsBool(List<Animator> animators, string boolname, bool value)
	{
		foreach (Animator anm in animators) {
			anm.SetBool (boolname,value);
		}
	}
		

	protected abstract Vector3 NextPoint ();

}
