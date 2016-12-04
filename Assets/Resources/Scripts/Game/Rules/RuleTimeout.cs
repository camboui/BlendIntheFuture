using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RuleTimeout : MonoBehaviour {

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
		if (UpdateTimer.time < 0) {
			foreach (Human current in GameVariables.players) {
				if (!current.isDead ()) {
					current.suicide ();
				}
			}
			SceneManager.LoadScene (nextSceneName);
		}
	}
}
