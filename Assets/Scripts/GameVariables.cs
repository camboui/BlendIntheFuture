using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameVariables : MonoBehaviour {
	
	public static List<Player> players;
	public static List<Color> availableColors;
	private static int nbColors;

	void Start () {
		DontDestroyOnLoad (gameObject);
		players = new List<Player> ();
		availableColors= new List<Color> (){Color.blue,Color.cyan,Color.gray,Color.green,Color.magenta,Color.red,Color.white,Color.yellow};
		nbColors = availableColors.Count;
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
