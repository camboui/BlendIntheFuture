using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class HumanController : MonoBehaviour
{
	public float movementSpeed;
	public Human human;
	public Transform rendererContainer;

	private static bool pausedGame;

	private Vector3 movementVector;
	private Collider2D mapCollider;
	private Transform groundPosition;

	private Vector3 leftOrientationScale;
	private Vector3 rightOrientationScale;
	private Knife knifeWeapon;
	private Rigidbody2D rgdby;
	private Animator[] animators;
	private GameObject bonus;
	private bool bonusUsed;
	private XboxInput xboxInput;

	void Start()
	{
		// clear round values for this player
		human.startNewRound (); 

		movementSpeed = GameVariables.charactersSpeed;
			
		//Set orientation
		leftOrientationScale = transform.localScale;
		rightOrientationScale = transform.localScale;
		rightOrientationScale.x = rightOrientationScale.x * -1;
		pausedGame = false;
		xboxInput = new XboxInput (human.getJoystickId());
		mapCollider = GameObject.FindGameObjectWithTag ("Map").GetComponentInChildren<Collider2D> ();
		groundPosition = transform.FindChild ("GroundCheck");
		rendererContainer = transform.FindChild ("Renderers");

		knifeWeapon= transform.GetComponentInChildren<Knife>(true);
		knifeWeapon.initialiseWeapon (0.5f, rendererContainer);

	
		bonus = human.getBonus ();
		bonusUsed = false;

		rgdby = gameObject.GetComponent<Rigidbody2D> ();
		animators = rendererContainer.GetComponentsInChildren <Animator>();

	}

	private void changeAllAnimatorsBool(string boolname, bool value)
	{
		foreach (Animator anm in animators) {
			anm.SetBool (boolname,value);
		}
	}

	private void triggerAllAnimators(string triggerName)
	{
		foreach (Animator anm in animators) {
			anm.SetTrigger (triggerName);
		}
	}


	//Script is disabled on start
	void OnEnable(){
		pausedGame = false;
	}
		

	void Update()
	{
		//X and Y axis are defined in Edit/Project Settings/Input
		movementVector.x = xboxInput.getXaxis () * movementSpeed * Time.deltaTime;
		if (movementVector.x!=0f && mapCollider.OverlapPoint ((Vector2)(groundPosition.position + new Vector3(movementVector.x,0,0))))  {
			rgdby.MovePosition (transform.position + movementVector);
			changeAllAnimatorsBool ("isWalking", true);
			if (movementVector.x < 0)
				transform.localScale = rightOrientationScale;
			else
				transform.localScale = leftOrientationScale;
		}

		movementVector.y = xboxInput.getYaxis () * movementSpeed * Time.deltaTime;
		if (movementVector.y!=0f && mapCollider.OverlapPoint ((Vector2)(groundPosition.position + new Vector3(0,movementVector.y,0))))  {
			rgdby.MovePosition (transform.position + movementVector);
			changeAllAnimatorsBool ("isWalking", true);
		} 

		if (movementVector.x == 0f && movementVector.y == 0f) {
			changeAllAnimatorsBool ("isWalking", false);
		}

		

		if (Input.GetKeyDown (xboxInput.A)) {
			Debug.Log ("P" + human.getJoystickId() + " : A");
			knifeWeapon.use ();
		}
		if (Input.GetKeyDown (xboxInput.B)) {
			Debug.Log ("P" + human.getJoystickId() + " : B"); 
			if (!bonusUsed){
				MonoBehaviour[] scripts = bonus.GetComponents<MonoBehaviour>();
				foreach(MonoBehaviour s in scripts){
					s.enabled = true;
				}
				bonusUsed = true;
			}
		}
		if (Input.GetKeyDown (xboxInput.X)) {
			Debug.Log ("P" + human.getJoystickId() + " : X"); 
			triggerAllAnimators ("shootTrigger");  //Trigger Animation which will call function from BulletSpawner.cs
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
			if (!pausedGame) {
				SceneManager.LoadScene ("PauseMenu", LoadSceneMode.Additive);
				pausedGame = true;
			}
		}
	}

}
