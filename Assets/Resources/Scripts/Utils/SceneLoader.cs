using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	public void loadScene(string sceneName){
		SceneManager.LoadScene (sceneName);
	}

	public void loadScene(int sceneIndex){
		SceneManager.LoadScene (sceneIndex);
	}

	public void quit(){
		Application.Quit();
	}
}



