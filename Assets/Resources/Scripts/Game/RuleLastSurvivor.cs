using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RuleLastSurvivor : MonoBehaviour {

	public string nextSceneName;
	
	// Use this for initialization
	void Start () {
		if (nextSceneName == null)
			nextSceneName = "EndGame";
		//Check rule Every 1 second. Let some time for draw 
		InvokeRepeating("checkEndGame", 1.0f, 1.0f);
	}

	void checkEndGame()
	{
		if (GameCreator.remainingPlayers.Count <= 1) {


			if (GameCreator.remainingPlayers.Count == 1) {
				foreach (Human current in GameVariables.players) {
					if (current.getJoystickId () == GameCreator.remainingPlayers[0].getJoystickId()) {
						current.winRound ();
					}
				}
			}
			Debug.Log ("GAME ENDED WITH " + GameCreator.remainingPlayers.Count + "players");
			SceneManager.LoadScene (nextSceneName);
		}
	}

}
