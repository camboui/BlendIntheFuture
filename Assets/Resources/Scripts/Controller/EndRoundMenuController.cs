using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndRoundMenuController : MonoBehaviour {

	private float timeBetweenPoints = 0.4f;
	public Transform playerScoreContainer;
	public GameObject pointsContainer;
	public GameObject onePoint;
	public bool wasLastRound;
	public Image [,] printedPoints;
	public Image [,] backGroundImage;
	public Text[] skullNb;
	public Sprite imageWonPoint;
	public Sprite imageNoPoint;
	public Sprite ImageWinningBackground;


	public AudioClip addPointSound;
	public AudioClip removePointSound;

	void Start () {
		
		printedPoints = new Image[GameVariables.players.Count, GameVariables.nbRound];
		backGroundImage = new Image[GameVariables.players.Count, 2];
		skullNb = new Text[GameVariables.players.Count];

		wasLastRound = false;
		int playerNb = 1;

		foreach (Human current in GameVariables.players) {
			//Add player score Container
			GameObject newPointsContainer = Instantiate (pointsContainer, playerScoreContainer) as GameObject;
			newPointsContainer.transform.localScale = Vector3.one;


			Text playerText = newPointsContainer.transform.FindChild ("PlayerSide/Text").GetComponent<Text> ();
			playerText.text = "player " + playerNb;
			playerText.color = current.getColor ();

			//Update texts according to score
			int currentScore = current.getCurrentScore ();
			skullNb [playerNb - 1] = newPointsContainer.transform.FindChild ("PointSide/Skull/Text").GetComponent<Text> ();
			skullNb [playerNb - 1].text = "x" + currentScore;

			backGroundImage [playerNb - 1, 0] = newPointsContainer.transform.FindChild ("PlayerSide").GetComponent<Image> ();
			backGroundImage [playerNb - 1, 1] = newPointsContainer.transform.FindChild ("PointSide").GetComponent<Image> ();
			//Check if it should be a new match
			if (current.getNextUpdateScore() >= GameVariables.nbRound)
				wasLastRound = true;

			//print each point for each player
			for (int i = 0; i < GameVariables.nbRound; i++) {
				GameObject newPoint = Instantiate (onePoint, newPointsContainer.transform.FindChild ("PointSide/PointContainer").transform) as GameObject;
				newPoint.transform.localScale = Vector3.one;

				//Change the sprite to won point
				if (i < currentScore) {
					newPoint.GetComponent<Image> ().sprite = imageWonPoint;
				}
				printedPoints [playerNb - 1, i] = newPoint.GetComponent<Image> ();

			}
			playerNb++;
		}

		StartCoroutine (updateScores ());

		if (wasLastRound) {
			GameObject.Find ("Text Next Match").GetComponent<Text> ().text = "Rematch";
			//Make special anim
		}

		//Find higher score
		int bestScore = 0;
		foreach (Human current in GameVariables.players) {
			if (current.getNextUpdateScore() > bestScore)
				bestScore = current.getNextUpdateScore();
		}

		//HighLight Higher score
		for (int i = 0; i < GameVariables.players.Count; i++) {
			if (GameVariables.players [i].getNextUpdateScore () == bestScore) {
				backGroundImage [i, 0].sprite = ImageWinningBackground;
				backGroundImage [i, 1].sprite = ImageWinningBackground;
			}
				
		}
	}

	private IEnumerator updateScores()
	{
		int i = 0;
		foreach (Human current in GameVariables.players) {
			//Add won points
			for (int j = current.getCurrentScore (); j < current.getCurrentScore () + current.getPointsToAdd (); j++) {
				if (j < GameVariables.nbRound) {
					yield return new WaitForSeconds (timeBetweenPoints);
					SoundManager.instance.RandomizeSfx (addPointSound);
					printedPoints [i, j].sprite = imageWonPoint;
					skullNb [i].text = "x" + (j + 1);
				}
			}

			//remove lost points
			int printedScore = Mathf.Min (current.getCurrentScore () + current.getPointsToAdd (), GameVariables.nbRound) - 1;
			for (int j = printedScore; j > printedScore - current.getPointsToRemove (); j--) {
				if (j >= 0) {
					yield return new WaitForSeconds (timeBetweenPoints); 
					SoundManager.instance.RandomizeSfx (removePointSound);
					printedPoints [i, j].sprite = imageNoPoint;
					skullNb [i].text = "x" + j;
				}
			}
			current.updateCurrentScore ();
			i++;
		}
	}

	public void startNextRound(){
		if(!wasLastRound)
			SceneManager.LoadScene ("GameLoop");
		else
			SceneManager.LoadScene ("ModeSelectionMenu");
		
	}

	public void goToMainMenu(){
		SceneManager.LoadScene ("ModeSelectionMenu");
	}
}
