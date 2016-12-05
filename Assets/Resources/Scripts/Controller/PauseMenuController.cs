using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour {

	private List<MonoBehaviour> disabledScripts;
	private float previousTimeScale;

	void Awake () {
		//Pause the game by disabling all custom scripts
		previousTimeScale=Time.timeScale;
		Time.timeScale = 0;
		disabledScripts = new List<MonoBehaviour> ();
		MonoBehaviour thisScript = transform.GetComponent<MonoBehaviour> ();

		MonoBehaviour[] scripts = GameObject.FindObjectsOfType<MonoBehaviour> ();
		foreach (MonoBehaviour p in scripts) {
			if (GameVariables.customScripts.Contains (p.GetType ().ToString ()) && !p.Equals (thisScript) && p.enabled) {
				disabledScripts.Add (p);
				p.enabled = false;
			}
		}
	}

	public void resumeGame(){
		//set back timeScale
		Time.timeScale = previousTimeScale;
		//re-enable all
		foreach (MonoBehaviour p in disabledScripts) {
			p.enabled = true;
		}
		GameVariables.pausedGame = true;
	}

	public void quit(string sceneName){
		Time.timeScale = previousTimeScale;
		SceneManager.LoadScene (sceneName);
	}
}

