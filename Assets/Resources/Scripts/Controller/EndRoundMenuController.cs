using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndRoundMenuController : MonoBehaviour {


	public Transform playerScoreContainer;
	public GameObject pointsContainer;
	public GameObject onePoint;
	public bool wasLastRound;

	void Start () {
		wasLastRound = false;
		int playerNb=1;
		foreach (Human current in GameVariables.players) {
			GameObject newPointsContainer = Instantiate (pointsContainer,playerScoreContainer) as GameObject;
			newPointsContainer.transform.localScale = Vector3.one;
			newPointsContainer.GetComponent<Text> ().text = "P" + playerNb;
			newPointsContainer.GetComponent<Text> ().color = current.getColor ();

			playerNb++;
			int currentScore = current.getWins ();
			if (currentScore >= GameVariables.nbRound)
				wasLastRound = true;
			for (int i = 0; i < GameVariables.nbRound; i++) {
				GameObject newPoint = Instantiate (onePoint,newPointsContainer.transform) as GameObject;
				newPoint.transform.localScale = Vector3.one;

				if (currentScore > 0) {
					newPoint.GetComponent<Image> ().color = Color.yellow;
					currentScore--;
				}

			}
		}

		if (wasLastRound) {
			GameObject.Find ("Text Next Match").GetComponent<Text> ().text = "Rematch";
			//Make special anim
		}
	}

	public void startNextRound(){
		if(!wasLastRound)
			SceneManager.LoadScene ("GameLoop");
		else
			SceneManager.LoadScene ("ModeSelectionMenu");
		
	}

	public void goToMainMenu(){
		SceneManager.LoadScene ("MainMenu");
	}
}
