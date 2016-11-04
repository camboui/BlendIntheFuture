using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameVariables : MonoBehaviour {
	
	public static List<Player> players;
	public static List<Color> availableColors;
	public static int maxPlayers = 4;
	public static int minPlayers = 1;

	private static int nbColors;

	void Start () {
		DontDestroyOnLoad (gameObject);
		players = new List<Player> ();

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
