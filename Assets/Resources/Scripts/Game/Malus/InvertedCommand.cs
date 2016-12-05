using UnityEngine;
using System.Collections;

public class InvertedCommand : Malus_Abstract {


	public float time;

	private float currentTime;

	// Use this for initialization
	//void Start () {
	//	Debug.Log ("P" + humanController.human.getJoystickId() + " inverted command");
	//	currentTime = 0;
	//	humanController.modeDirection = -1;
	//}

	void OnEnable(){
		Debug.Log ("P" + humanController.human.getJoystickId() + " inverted command");
		currentTime = 0;
		humanController.modeDirection = -1;
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;
		if (currentTime > time) {
			humanController.modeDirection = 1;
			Debug.Log ("P" + humanController.human.getJoystickId () + " inverted command end");
			this.enabled = false;
		} else {
			humanController.modeDirection = -1;
		}
	}
}
