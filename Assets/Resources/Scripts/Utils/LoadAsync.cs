using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadAsync : MonoBehaviour {

	public string scene;
	public Slider loadingBar;
	public GameObject loadingImage;

	private AsyncOperation async;

	// Use this for initialization
	void Awake () {
		Load ();
	}
	
	public void Load()
	{
		loadingImage.SetActive (true);
		StartCoroutine (LoadLevelWithBar (scene));
	}

	IEnumerator LoadLevelWithBar (string level)
	{
		async = SceneManager.LoadSceneAsync (level);
		while (!async.isDone)
		{
			loadingBar.value = async.progress; 
			yield return null;
		}
	}
}
