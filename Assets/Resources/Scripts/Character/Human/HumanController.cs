using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class HumanController : MonoBehaviour
{
	private static bool pausedGame;
	public int playerId;
	private Vector3 movementVector;
	public float movementSpeed = 1f;
	private Collider2D mapCollider;
	private Transform groundPosition;

	private XboxInput xboxInput;
	void Start()
	{
		pausedGame = false;
		xboxInput = new XboxInput(playerId);
		mapCollider = GameObject.FindGameObjectWithTag ("Map").GetComponentInChildren<Collider2D> ();
		groundPosition = transform.FindChild ("GoundCheck");
	}

	//Script is disabled on start
	void OnEnable(){
		pausedGame = false;
	}

	void Update()
	{
		//X and Y axis are defined in Edit/Project Settings/Input
		movementVector.x = xboxInput.getXaxis() * movementSpeed * Time.deltaTime;
		if (mapCollider.OverlapPoint ((Vector2)(groundPosition.position + movementVector))){
			transform.position += movementVector;
			movementVector = Vector3.zero;
		}
		
		movementVector.y = xboxInput.getYaxis () * movementSpeed * Time.deltaTime;
		if (mapCollider.OverlapPoint ((Vector2)(groundPosition.position + movementVector))){
		//	movementVector.z = movementVector.y;
			transform.position += movementVector;
			movementVector = Vector3.zero;
		}

		if (Input.GetKeyDown(xboxInput.A)) {
			Debug.Log ("P"+playerId+" : A");      
		}
		if (Input.GetKeyDown(xboxInput.B)) {
			Debug.Log ("P"+playerId+" : B");      
		}
		if (Input.GetKeyDown(xboxInput.X)) {
			Debug.Log ("P"+playerId+" : X");      
		}
		if (Input.GetKeyDown(xboxInput.Y)) {
			Debug.Log ("P"+playerId+" : Y");      
		}
		if (Input.GetKeyDown(xboxInput.LT)) {
			Debug.Log ("P"+playerId+" : LT");      
		}
		if (Input.GetKeyDown(xboxInput.LR)) {
			Debug.Log ("P"+playerId+" : LR");      
		}
		if (Input.GetKeyDown(xboxInput.Select)) {
			Debug.Log ("P"+playerId+" : Select");      
		}
		if (Input.GetKeyDown(xboxInput.BStart)) {
			Debug.Log ("P"+playerId+" : BStart");
			if (!pausedGame) {
				SceneManager.LoadScene ("PauseMenu", LoadSceneMode.Additive);
				pausedGame = true;
			}
		}


	}

}
