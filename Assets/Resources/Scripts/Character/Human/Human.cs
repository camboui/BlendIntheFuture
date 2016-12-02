using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Human {

	[SerializeField] private List<int> killedThisRound;
	[SerializeField] private int joystickId;
	[SerializeField] private int wins;
	[SerializeField] private int deaths;
	[SerializeField] private Color color;


	public Human(int joyId, Color c){
		killedThisRound = new List<int> ();
		joystickId = joyId;
		wins = 0;
		deaths = 0;
		color = c;
	}

	public Human(){
		killedThisRound = new List<int> ();
		joystickId = 0;
		wins = 0;
		deaths = 0;
		color = Color.blue;
	}

	public List<int> getKilledThisRound(){
		return killedThisRound;
	}

	public void startNewRound(){
		killedThisRound.Clear ();
	}

	public void rematchClear(){
		wins = 0;
		deaths = 0;
		killedThisRound.Clear ();
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

