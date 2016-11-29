using UnityEngine;
using System.Collections;

public class Human {

	int joystickId;
	private int wins;
	private int deaths;
	private Color color;


	public Human(int joyId, Color c){
		joystickId = joyId;
		wins = 0;
		deaths = 0;
		color = c;
	}

	public Human(){
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

	public int getWins(){
		return wins;
	}

	public int getDeaths(){
		return deaths;
	}

	public void winRound () {
		wins++;
	}

	public void die(){
		deaths++;
	}

}

