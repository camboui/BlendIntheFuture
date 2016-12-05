using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class GameVariables : MonoBehaviour {

	public static List<Human> players;
	public static List<Color> availableColors;
	public static List<String> customScripts;
	public static List<String>steeringScripts;
	public static GameObject selectedMode;
	public static GameObject [] modes;
	public static List<GameObject> bonus;
	public static bool pausedGame;

	public static int nbRound = 5;
	public static int maxPlayers = 4;
	public static int minPlayers = 1;
	public static float charactersSpeed = 1.5f;

	private static int nbColors;
	private static int nbBonus;

	void Awake()
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
	}


	void Start () {
		DontDestroyOnLoad (gameObject);
		players = new List<Human> ();
		customScripts = new List<String> ();

		pausedGame = false;

		//Used to pause the game by disabling all hand-made script in GameLoop
		foreach (UnityEngine.Object o in  Resources.LoadAll ("Scripts")) {
			customScripts.Add (o.name);
		}

		availableColors = new List<Color> () {
			Color.magenta,
			Color.red,
			Color.yellow,
			Color.blue,
			Color.cyan,
			Color.gray,
			Color.green
		};
		nbColors = availableColors.Count;
		modes = Resources.LoadAll<GameObject> ("Prefabs/Modes");
		steeringScripts = new List<String> ();
		foreach (UnityEngine.Object o in Resources.LoadAll<UnityEngine.Object> ("Scripts/Character/Robot/Steerings")) {
			steeringScripts.Add (o.name);
		}

		bonus = new List<GameObject> ();
		foreach (GameObject go in Resources.LoadAll<GameObject> ("Prefabs/Game/Bonus")) {
			bonus.Add (go);
		};

		nbBonus = bonus.Count;
		//This is scene 0, load scene 1
		SceneManager.LoadScene (1);
	}

	//return next right color from the list according to current position
	public static Color getNextColorRight(Color current,Dictionary<int,Color> used)
	{
		int index = availableColors.FindIndex (o => o == current);
		do {
			index++;

			if (index >= nbColors)
				index = 0;
		} while(used.ContainsValue (availableColors [index]));
		return availableColors [index];
	}

	//return next left color from the list according to current position
	public static Color getNextColorLeft(Color current,Dictionary<int,Color> used)
	{
		int index = availableColors.FindIndex (o => o == current);
		do {
			index--;

			if (index < 0)
				index = nbColors - 1;
		} while(used.ContainsValue (availableColors [index]));
		return availableColors [index];
	}


	//return next right bonus from the list according to current position
	public static GameObject getNextBonusRight(GameObject current)
	{
		int index = bonus.FindIndex (o => o == current);
		index++;
		if (index >= nbBonus)
			index = 0;
		return bonus [index];
	}

	//return next left bonus from the list according to current position
	public static GameObject getNextBonusLeft(GameObject current)
	{
		int index = bonus.FindIndex (o => o == current);
		index--;

		if (index < 0)
			index = nbBonus - 1;
		return bonus [index];
	}

}

