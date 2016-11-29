using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ModeSelectionController : MonoBehaviour {

	public Transform modeContainer;

	EventSystem eventSystem;
	void Start()
	{
		eventSystem = GameObject.Find ("EventSystem").GetComponent<EventSystem> ();
		for (int i = 0; i < GameVariables.modes.Length; i++) {
			
			GameObject newGo = Instantiate (GameVariables.modes [i], modeContainer) as GameObject;
			newGo.transform.localScale = Vector3.one;
			newGo.name = GameVariables.modes [i].name;

			if (i == 0) {
				eventSystem.SetSelectedGameObject (newGo.transform.FindChild ("Graphic").gameObject);
			}
		}

	}


}
