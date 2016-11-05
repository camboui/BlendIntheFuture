using UnityEngine;
using System.Collections;

public class GameCreator : MonoBehaviour {

	public GameObject prefab_player;
	public GameObject prefab_IA;
	public Transform parentIA;
	private int nbIA=70;//TODO make classes for game data

	Vector3 topLeftCameraPoint;


	void Start () {
		int i = 1;

		float height = Camera.main.orthographicSize * 2;
		float width = height * Camera.main.aspect; 
		topLeftCameraPoint = new Vector3 (-width / 2, -height / 2, 0) +  Camera.main.transform.position;

		//Instantiate players
		foreach (Player p in GameVariables.players) {
			GameObject newGO = Instantiate (prefab_player) as GameObject;
			newGO.GetComponentInChildren<PlayerController> ().playerId = p.getJoystickId ();
			newGO.transform.position = new Vector3 (topLeftCameraPoint.x + Random.Range (0, width), topLeftCameraPoint.y + Random.Range (0, height), 0);
			// DEBUG : SHOW COLOR
			//newGO.GetComponentInChildren<SpriteRenderer> ().color = p.getColor ();
			newGO.transform.name = "Player " + i;
			i++;
		}

		//Instantiate AIs
		for (int j = 0; j < nbIA; j++) {
			GameObject newGO = Instantiate (prefab_IA, parentIA) as GameObject;
			newGO.transform.position = new Vector3 (topLeftCameraPoint.x + Random.Range (0, width), topLeftCameraPoint.y + Random.Range (0, height), 0);
			newGO.transform.name = "IA_" + i;
			i++;
		}
	}
		
}
