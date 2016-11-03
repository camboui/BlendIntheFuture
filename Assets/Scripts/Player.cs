using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	int joystickId;
	private int wins;
	private int deaths;
	private Color color;

	void Start () {
		joystickId = 0;
		wins = 0;
		deaths = 0;
		color = Color.blue;
	}

}
