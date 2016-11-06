using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateTimer : MonoBehaviour {

	// Use this for initialization
	private Text t ;
	private float time;
	void Start () {
		t = transform.GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;

		float minutes = time / 60;
		float seconds = time % 60;
		t.text = string.Format ("{0:00} : {1:00}", minutes, seconds);
	}
}

