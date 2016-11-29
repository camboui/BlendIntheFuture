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
	public List<GameObject> weapons;
	public Transform rendererContainer;

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
		pausedGame = false;
		xboxInput = new XboxInput (playerId);
		mapCollider = GameObject.FindGameObjectWithTag ("Map").GetComponentInChildren<Collider2D> ();
		groundPosition = transform.FindChild ("GroundCheck");
		rendererContainer = transform.FindChild ("Renderers");

		//to activate all weapons simultaneously
		foreach (Transform child in rendererContainer) {
			weapons.Add (child.FindChild ("Weapon").gameObject);
		}
	}

	//Script is disabled on start
	void OnEnable(){
		pausedGame = false;
	}

	IEnumerator deactivateWeapon() {
		yield return new WaitForSeconds (0.5f);
		foreach (GameObject weapon in weapons) {
			weapon.SetActive (false);
		}
	}

	void Update()
	{
		//X and Y axis are defined in Edit/Project Settings/Input
		movementVector.x = xboxInput.getXaxis () * movementSpeed * Time.deltaTime;
		if (movementVector.x != 0.0f && mapCollider.OverlapPoint ((Vector2)(groundPosition.position + movementVector))) {
			transform.position += movementVector;
			if (movementVector.x < 0)
				transform.localScale = new Vector3(-1,1,1);
			else
				transform.localScale = new Vector3(1,1,1);
			
			movementVector = Vector3.zero;
		}
		
		movementVector.y = xboxInput.getYaxis () * movementSpeed * Time.deltaTime;
		if (mapCollider.OverlapPoint ((Vector2)(groundPosition.position + movementVector))) {
			//	movementVector.z = movementVector.y;
			transform.position += movementVector;
			movementVector = Vector3.zero;
		}

		if (Input.GetKeyDown (xboxInput.A)) {
			if (weapons [0].activeSelf == false) {
				Debug.Log ("P" + playerId + " : A");
				foreach (GameObject weapon in weapons) {
					weapon.SetActive (true);
				}
				StartCoroutine ("deactivateWeapon");
			}
		}
		if (Input.GetKeyDown (xboxInput.B)) {
			Debug.Log ("P" + playerId + " : B");      
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
