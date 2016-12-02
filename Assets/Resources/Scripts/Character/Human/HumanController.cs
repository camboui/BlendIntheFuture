using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class HumanController : MonoBehaviour
{
	private static bool pausedGame;
	public int playerId;
	private Vector3 movementVector;
	public float movementSpeed = 1f;
	private Collider2D mapCollider;
	private Transform groundPosition;
	public Transform rendererContainer;
	private Vector3 leftOrientationScale;
	private Vector3 rightOrientationScale;
	private Knife knifeWeapon;
	private Rigidbody2D rgdby;
	private Animator[] animators;

	private XboxInput xboxInput;
	void Start()
	{
		// clear round values for this player
		foreach(Human current in GameVariables.players){
			if (current.getJoystickId () == playerId) {
				current.startNewRound (); 
				break;
			}
		}
		//Set orientation
		leftOrientationScale = transform.localScale;
		rightOrientationScale = transform.localScale;
		rightOrientationScale.x = rightOrientationScale.x * -1;
		pausedGame = false;
		xboxInput = new XboxInput (playerId);
		mapCollider = GameObject.FindGameObjectWithTag ("Map").GetComponentInChildren<Collider2D> ();
		groundPosition = transform.FindChild ("GroundCheck");
		rendererContainer = transform.FindChild ("Renderers");

		knifeWeapon= gameObject.AddComponent<Knife>();
		knifeWeapon.initialiseWeapon (0.5f, rendererContainer);

		rgdby = gameObject.GetComponent<Rigidbody2D> ();
		animators = rendererContainer.GetComponentsInChildren <Animator>();
	}

	private void changeAllAnimatorsBool(string boolname, bool value)
	{
		foreach (Animator anm in animators) {
			anm.SetBool (boolname,value);
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
			Debug.Log ("P" + playerId + " : A");
			knifeWeapon.use ();
		}
		if (Input.GetKeyDown (xboxInput.B)) {
			//changeAllAnimatorsBool ("isShooting", true);   
		}
		if (Input.GetKeyDown (xboxInput.X)) {
			Debug.Log ("P" + playerId + " : X");      
		}
		if (Input.GetKeyDown (xboxInput.Y)) {
			Debug.Log ("P" + playerId + " : Y");      
		}
		if (Input.GetKeyDown (xboxInput.LT)) {
			Debug.Log ("P" + playerId + " : LT");      
		}
		if (Input.GetKeyDown (xboxInput.LR)) {
			Debug.Log ("P" + playerId + " : LR");      
		}
		if (Input.GetKeyDown (xboxInput.Select)) {
			Debug.Log ("P" + playerId + " : Select");      
		}
		if (Input.GetKeyDown (xboxInput.BStart)) {
			Debug.Log ("P" + playerId + " : BStart");
			if (!pausedGame) {
				SceneManager.LoadScene ("PauseMenu", LoadSceneMode.Additive);
				pausedGame = true;
			}
		}
	}
}
