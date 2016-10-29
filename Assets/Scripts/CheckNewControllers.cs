using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CheckNewControllers : MonoBehaviour {

	public GameObject prefab_PlayerChoice;

	private GameObject canvas;
	private int nb_controller;

	// Use this for initialization
	void Start () {
		nb_controller = 0;
		canvas = GameObject.Find ("Canvas");

	}

	void Update()
	{
		string[] inputs = Input.GetJoystickNames ();
		if(inputs.Length !=nb_controller){
			nb_controller++;
			foreach (string p in Input.GetJoystickNames()) {
				Debug.Log ("Input change : " + p);
				GameObject temp = Instantiate (prefab_PlayerChoice) as GameObject;
				temp.transform.SetParent (canvas.transform);
			}
		}
	}

	

}
