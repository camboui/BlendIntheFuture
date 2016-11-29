using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ModeSceneLoader : MonoBehaviour {

	public void loadScene(string sceneName){
		//Find Gameobject Parent Rules
		Transform modeGo = transform;
		while(!modeGo.tag.Equals("Mode"))
		{
			modeGo = modeGo.parent;
		}

		foreach (GameObject mode in GameVariables.modes) {
			if (mode.name.Equals (modeGo.name)) {
				GameVariables.selectedMode = mode;
				break;
			}
		}

		SceneManager.LoadScene (sceneName);
	}

}