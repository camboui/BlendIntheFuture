using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DescriptionMenuController : MonoBehaviour {
	
	public GameObject prefab_ready;
	public GameObject parent;

	private List<Text> associatedPlayerText;
	private List<bool> arePlayersReady;
	private List<XboxInput> xboxInputs;
	private int nbPlayers;
	private int nbReadyPlayers;

	void Start () { 
		xboxInputs = new List<XboxInput> ();
		arePlayersReady = new List<bool> ();
		associatedPlayerText = new List<Text> ();
		nbPlayers = GameVariables.players.Count;
		nbReadyPlayers = 0;

		for (int i = 0; i < nbPlayers; i++) {
			arePlayersReady.Add (false);
			xboxInputs.Add (new XboxInput (GameVariables.players [i].getJoystickId()));

			GameObject newGO = Instantiate (prefab_ready,parent.transform) as GameObject;
			newGO.transform.localScale = new Vector3 (1, 1, 0);
	
			Text temp = newGO.GetComponent<Text> ();
			temp.color = GameVariables.players [i].getColor ();
			associatedPlayerText.Add (temp);
		}
	}

	//get number of connected controllers
	private int getNumberOfDevices(){
		int i = 0;
		foreach (string p in Input.GetJoystickNames ()) {
			if (p != "")// because GetJoystickNames gets buggy empty strings
				i++;
		}
		return i;
	}


	void Update()
	{
		for (int j = 0; j < nbPlayers; j++) {
			//Player joins the game, change Debug.Loging and activate script on Gameobject
			if (Input.GetKeyDown (xboxInputs[j].A)) {
				arePlayersReady [j] = true;
				associatedPlayerText [j].text = "Ready";
				nbReadyPlayers++;
				if (nbReadyPlayers == nbPlayers)
					SceneManager.LoadScene ("GameLoop");
			}
		}
	}
}
