using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateTimer : MonoBehaviour {

	public int minuteStart;
	public int secondeStart;

	// Use this for initialization
	private Text t ;
	public static float time;
	void Start () {
		t = transform.GetComponent<Text> ();
		time = minuteStart*60 + secondeStart;
	}

	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;

		float minutes = Mathf.Floor(time / 60);
		float seconds = time % 60;
		t.text = string.Format ("{0:00} : {1:00}", minutes, seconds);
	}
}

