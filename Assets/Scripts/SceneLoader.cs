using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour{

	public void loadScene(string sceneName){

		if (SceneManager.GetSceneByName (sceneName)!=null) {
			Debug.Log ("Loading : " + sceneName);
			SceneManager.LoadScene (sceneName);
		} else {
			Debug.Log ("Can't load unexisting scene : '" + sceneName + "'");
		}
	}

	public void exit(){
		Application.Quit ();
	}
}
