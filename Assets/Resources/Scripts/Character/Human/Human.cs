using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Human {

	[SerializeField] private List<int> killedThisRound;
	[SerializeField] private int joystickId;
	[SerializeField] private int currentScore;
	[SerializeField] private int pointsToAdd;
	[SerializeField] private int pointsToRemove;
	[SerializeField] private int ammo;
	[SerializeField] private int deaths;
	[SerializeField] private bool isAlive;
	[SerializeField] private Color color;
	[SerializeField] private GameObject bonus;


	public Human(int joyId, Color c, GameObject b){
		killedThisRound = new List<int> ();
		joystickId = joyId;
		ammo = 2;
		currentScore = 0;
		deaths = 0;
		color = c;
		bonus = b;
		pointsToAdd = 0;
		pointsToRemove = 0;
		isAlive = true;
	}

	public Human(){
		killedThisRound = new List<int> ();
		joystickId = 0;
		currentScore = 0;
		deaths = 0;
		color = Color.blue;
		ammo = 2;
		pointsToAdd = 0;
		pointsToRemove = 0;
		isAlive = true;
	}

	public List<int> getKilledThisRound(){
		return killedThisRound;
	}

	public void startNewRound(){
		ammo = 2;
		pointsToRemove = 0;
		pointsToAdd = 0;
		killedThisRound.Clear ();
		isAlive = true;
	}

	public void rematchClear(){
		pointsToRemove = 0;
		ammo = 2;
		pointsToAdd = 0;
		currentScore = 0;
		deaths = 0;
		isAlive = true;
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

	public int getCurrentScore(){
		return currentScore;
	}

	public int getDeaths(){
		return deaths;
	}

	public bool isDead(){
		return !isAlive;
	}

	public void updateCurrentScore()
	{
		currentScore += pointsToAdd - pointsToRemove;
		if (currentScore < 0)
			currentScore = 0;
	}

	public int getNextUpdateScore()
	{
		return Mathf.Max(0, currentScore + pointsToAdd - pointsToRemove);
	}

	public GameObject getBonus(){
		return bonus;
	}

	public void winRound () {
		pointsToAdd++;
	}

	public void suicide () {
		pointsToRemove++;
		isAlive = false;
	}

	public void die(){
		deaths++;
		isAlive = false;
	}

	public int getPointsToAdd()
	{
		return pointsToAdd;
	}

	public int getPointsToRemove()
	{
		return pointsToRemove;
	}
}

