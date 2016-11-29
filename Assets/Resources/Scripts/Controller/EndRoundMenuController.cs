using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndRoundMenuController : MonoBehaviour {


	public Transform playerScoreContainer;
	public GameObject pointsContainer;
	public GameObject onePoint;

	void Start () {
		foreach (Human current in GameVariables.players) {
			GameObject newPointsContainer = Instantiate (pointsContainer,playerScoreContainer) as GameObject;
			newPointsContainer.transform.localScale = Vector3.one;
			newPointsContainer.GetComponent<Text> ().text = "P" + current.getJoystickId ();
			newPointsContainer.GetComponent<Text> ().color = current.getColor ();

			int currentScore = current.getWins ();
			for (int i = 0; i < GameVariables.nbRound; i++) {
				GameObject newPoint = Instantiate (onePoint,newPointsContainer.transform) as GameObject;
				newPoint.transform.localScale = Vector3.one;

				if (currentScore > 0) {
					newPoint.GetComponent<Image> ().color = Color.yellow;
					currentScore--;
				}
			}
		}
	}

	public void startNextRound(){
		SceneManager.LoadScene ("GameLoop");
	}

	public void goToMainMenu(){
		SceneManager.LoadScene ("MainMenu");
	}
}
