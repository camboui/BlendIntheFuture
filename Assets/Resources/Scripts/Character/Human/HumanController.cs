using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class HumanController : MonoBehaviour
{
	public float movementSpeed;
	public Human human;
	public Transform rendererContainer;
	public int modeDirection; // 1 or -1, normal 1, inverted -1

	private Vector3 movementVector;
	private Collider2D mapCollider;
	private Transform groundPosition;

	private Vector3 leftOrientationScale;
	private Vector3 rightOrientationScale;

	private Rigidbody2D rgdby;
	public List<Animator> animatorsBody;
	public List<Animator> animatorsArm;
	public List<Animator> animatorsArmKnife;

	private GameObject bonus;
	private bool bonusUsed;
	private XboxInput xboxInput;

	void Start()
	{
		// clear round values for this player
		human.startNewRound (); 
		modeDirection = 1;
		movementSpeed = GameVariables.charactersSpeed;
			
		//Set orientation
		leftOrientationScale = transform.localScale;
		rightOrientationScale = transform.localScale;
		rightOrientationScale.x = rightOrientationScale.x * -1;
		xboxInput = new XboxInput (human.getJoystickId());
		mapCollider = GameObject.FindGameObjectWithTag ("Map").GetComponentInChildren<Collider2D> ();
		groundPosition = transform.FindChild ("GroundCheck");
		rendererContainer = transform.FindChild ("Renderers");

		Debug.Log ("Bonus_JoystickId" + human.getJoystickId ());
		bonus = GameObject.Find("Bonus_JoystickId"+human.getJoystickId()).gameObject;
		bonusUsed = false;

		rgdby = gameObject.GetComponent<Rigidbody2D> ();
		animatorsBody = new List<Animator> ();
		animatorsArm = new List<Animator> ();
		animatorsArmKnife= new List<Animator> ();
		for (int i = 0; i < rendererContainer.childCount; i++) {
			animatorsBody.Add (rendererContainer.GetChild (i).GetComponent<Animator> ());
			animatorsArm.Add (rendererContainer.GetChild (i).FindChild("arm").GetComponent<Animator> ());
			animatorsArmKnife.Add (rendererContainer.GetChild (i).FindChild("armKnife").GetComponent<Animator> ());
		}


	}

	private void changeAllAnimatorsBool(List<Animator> animators, string boolname, bool value)
	{
		foreach (Animator anm in animators) {
			anm.SetBool (boolname,value);
		}
	}

	private void triggerAllAnimators(List<Animator> animators,string triggerName)
	{
		foreach (Animator anm in animators) {
			anm.SetTrigger (triggerName);
		}
	}
		

	void Update()
	{
		//Remove pause menu if existing
		if (GameVariables.pausedGame) {
			SceneManager.UnloadScene ("PauseMenu");
			GameVariables.pausedGame = false;
		}

		//X and Y axis are defined in Edit/Project Settings/Input
		movementVector.x = modeDirection * xboxInput.getXaxis () * movementSpeed * Time.deltaTime;
		if (movementVector.x!=0f && mapCollider.OverlapPoint ((Vector2)(groundPosition.position + new Vector3(movementVector.x,0,0))))  {
			rgdby.MovePosition (transform.position + movementVector);
			changeAllAnimatorsBool (animatorsBody,"isWalking", true);
			changeAllAnimatorsBool (animatorsArm,"isWalking", true);
			if (movementVector.x < 0)
				transform.localScale = rightOrientationScale;
			else
				transform.localScale = leftOrientationScale;
		}

		movementVector.y = modeDirection * xboxInput.getYaxis () * movementSpeed * Time.deltaTime;
		if (movementVector.y!=0f && mapCollider.OverlapPoint ((Vector2)(groundPosition.position + new Vector3(0,movementVector.y,0))))  {
			rgdby.MovePosition (transform.position + movementVector);
			changeAllAnimatorsBool (animatorsBody,"isWalking", true);
			changeAllAnimatorsBool (animatorsArm,"isWalking", true);
		} 

		if (movementVector.x == 0f && movementVector.y == 0f) {
			changeAllAnimatorsBool (animatorsBody,"isWalking", false);
			changeAllAnimatorsBool (animatorsArm,"isWalking", false);

		}

		

		if (Input.GetKeyDown (xboxInput.A)) {
			Debug.Log ("P" + human.getJoystickId() + " : A");
			triggerAllAnimators (animatorsArmKnife,"attackTrigger"); 
		}
		if (Input.GetKeyDown (xboxInput.B)) {
			Debug.Log ("P" + human.getJoystickId() + " : B"); 
			if (!bonusUsed) {
				Debug.Log ("Use Bonus");
				Bonus_Abstract[] scripts = bonus.GetComponents<Bonus_Abstract> ();
				foreach (Bonus_Abstract s in scripts) {
					Debug.Log (s);
					s.enabled = true;
				}
				bonusUsed = true;
			} else {
				Debug.Log ("Already Use Bonus");
			}
		}
		if (Input.GetKeyDown (xboxInput.X)) {
			Debug.Log ("P" + human.getJoystickId() + " : X"); 
			if(human.getAmmo()>0){
				human.removeAmmo ();
				triggerAllAnimators (animatorsArm,"shootTrigger");  //Trigger Animation which will call function from BulletSpawner.cs
			}
		}
		if (Input.GetKeyDown (xboxInput.Y)) {
			Debug.Log ("P" + human.getJoystickId() + " : Y");      
		}
		if (Input.GetKeyDown (xboxInput.LT)) {
			Debug.Log ("P" + human.getJoystickId() + " : LT");      
		}
		if (Input.GetKeyDown (xboxInput.LR)) {
			Debug.Log ("P" + human.getJoystickId() + " : LR");      
		}
		if (Input.GetKeyDown (xboxInput.Select)) {
			Debug.Log ("P" + human.getJoystickId() + " : Select");      
		}
		if (Input.GetKeyDown (xboxInput.BStart)) {
			Debug.Log ("P" + human.getJoystickId() + " : BStart");
			if (!GameVariables.pausedGame ) {
				SceneManager.LoadScene ("PauseMenu", LoadSceneMode.Additive);
			}
		}


	}

}
