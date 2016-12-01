using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ModeSelectionController : MonoBehaviour {

	public Transform modeContainer;
	private List<XboxInput> xboxInputs;

	EventSystem eventSystem;
	void Start()
	{
		//Print all prefabs on screen
		eventSystem = GameObject.Find ("EventSystem").GetComponent<EventSystem> ();
		for (int i = 0; i < GameVariables.modes.Length; i++) {
			
			GameObject newGo = Instantiate (GameVariables.modes [i], modeContainer) as GameObject;
			newGo.transform.localScale = Vector3.one;
			newGo.name = GameVariables.modes [i].name;

			if (i == 0) {
				eventSystem.SetSelectedGameObject (newGo.transform.FindChild ("Graphic").gameObject);
			}
		}
			
		//Instantiate new controllers to use them in Update
		xboxInputs = new List<XboxInput> ();
		foreach(Human current in GameVariables.players){
			xboxInputs.Add (new XboxInput (current.getJoystickId()));
		}

	}

	void Update()
	{
		foreach(XboxInput current in xboxInputs){
			if (Input.GetKeyDown (current.B)) {
				SceneManager.LoadScene ("PlayerSelectionMenu");
			}
		}

	}

}
