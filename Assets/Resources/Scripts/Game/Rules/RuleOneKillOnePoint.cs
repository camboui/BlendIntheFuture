using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RuleOneKillOnePoint : MonoBehaviour {

	public string nextSceneName;

	// Use this for initialization
	void Start () {
		if (nextSceneName == null || nextSceneName.Equals(""))
			nextSceneName = "EndGame";
		//Check rule Every 1 second. Let some time for draw 
		InvokeRepeating("checkEndGame", 1.0f, 1.0f);
	}

	void checkEndGame()
	{
		if (GameCreator.remainingPlayers.Count <= 1) {
			
			foreach (Human current in GameVariables.players) {
				for (int i = 0; i < current.getKilledThisRound ().Count; i++) {
					current.winRound ();
				}
			}
			Debug.Log ("GAME ENDED WITH " + GameCreator.remainingPlayers.Count + "players");
			SceneManager.LoadScene (nextSceneName);
		}
	}
}
