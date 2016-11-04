using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class GameVariables : MonoBehaviour {
	
	public static List<Player> players;
	public static List<Color> availableColors;
	public static List<String> customScripts;

	public static int maxPlayers = 4;
	public static int minPlayers = 1;


	private static int nbColors;

	void Start () {
		DontDestroyOnLoad (gameObject);
		players = new List<Player> ();
		customScripts = new List<String> ();

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

		//This is scene 0, load scene 1
		SceneManager.LoadScene (1);
	}

	//return next right color from the list according to current position
	public static Color getNextColorRight(Color current)
	{
		int index = availableColors.FindIndex (o => o == current);
		index++;

		if (index >= nbColors)
			index = 0;

		return availableColors [index];
	}

	//return next left color from the list according to current position
	public static Color getNextColorLeft(Color current)
	{
		int index = availableColors.FindIndex (o => o == current);
		index--;

		if (index < 0)
			index = nbColors - 1;

		return availableColors [index];
	}


}
