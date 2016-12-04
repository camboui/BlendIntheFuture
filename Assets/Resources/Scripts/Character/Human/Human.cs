using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Human {

	[SerializeField] private List<int> killedThisRound;
	[SerializeField] private int joystickId;
	[SerializeField] private int wins;
	[SerializeField] private int ammo;
	[SerializeField] private int deaths;
	[SerializeField] private Color color;
	[SerializeField] private GameObject bonus;


	public Human(int joyId, Color c, GameObject b){
		killedThisRound = new List<int> ();
		joystickId = joyId;
		ammo = 2;
		wins = 0;
		deaths = 0;
		color = c;
		bonus = b;
	}

	public Human(){
		killedThisRound = new List<int> ();
		joystickId = 0;
		wins = 0;
		deaths = 0;
		color = Color.blue;
		ammo = 2;
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

	public int getAmmo()
	{
		return ammo;
	}

	public void removeAmmo()
	{
		ammo--;
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

	public GameObject getBonus(){
		return bonus;
	}

	public void winRound () {
		wins++;
	}

	public void die(){
		deaths++;
	}

}

