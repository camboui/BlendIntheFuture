using UnityEngine;
using System.Collections;

public class Player {

	int joystickId;
	private int wins;
	private int deaths;
	private Color color;


	public Player(int joyId, Color c){
		joystickId = joyId;
		wins = 0;
		deaths = 0;
		color = c;
	}

	public Player(){
		joystickId = 0;
		wins = 0;
		deaths = 0;
		color = Color.blue;
	}

	public int getJoystickId(){
		return joystickId;
	}

	public Color getColor(){
		return color;
	}

}
