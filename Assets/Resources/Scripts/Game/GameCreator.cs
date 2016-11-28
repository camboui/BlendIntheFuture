using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor;

public class GameCreator : MonoBehaviour {

	public GameObject prefab_player;
	public GameObject prefab_IA;
	public Transform parentIA;
	private int nbIA=5;//TODO make classes for game data
	Vector3 topLeftCameraPoint;
	private static Bounds mapBounds;

	void Start () {
		int i = 1;

		mapBounds = GameObject.FindGameObjectWithTag ("Map").GetComponentInChildren<Collider2D> ().bounds;

		float height = Camera.main.orthographicSize * 2;
		float width = height * Camera.main.aspect; 
		topLeftCameraPoint = new Vector3 (-width / 2, -height / 2, 0) +  Camera.main.transform.position;

		//Instantiate players
		foreach (Human p in GameVariables.players) {
			GameObject newGO = Instantiate (prefab_player) as GameObject;
			newGO.GetComponentInChildren<HumanController> ().playerId = p.getJoystickId ();
			newGO.transform.position = randomPosOnMap ();
			newGO.transform.localScale = Vector3.one;
			// DEBUG : SHOW COLOR
			//newGO.GetComponentInChildren<SpriteRenderer> ().color = p.getColor ();
			newGO.transform.name = "Player " + i;
			i++;
		}

		//Instantiate AIs
		List<MonoBehaviour> steerings = new List<MonoBehaviour>();
		steerings.Add (new PathFollowerSteering ());
		steerings.Add (new ThinkerSteering());
		steerings.Add (new WanderSteering());
		int rand = Random.Range (0, steerings.Count);
		Debug.Log ("steerings.Count = " + steerings.Count);
		Debug.Log ("rand = " + rand);
		for (int j = 0; j < nbIA; j++) {
			GameObject newGO = Instantiate (prefab_IA, parentIA) as GameObject;
			newGO.AddComponent (steerings [rand].GetType());
			newGO.transform.position = randomPosOnMap ();
			newGO.transform.name = "IA_" + i;
			newGO.transform.localScale = Vector3.one;
			i++;
		}
	}

	public static Vector3 randomPosOnMap()
	{
		return new Vector3 (mapBounds.center.x+ Random.Range (-mapBounds.extents.x, mapBounds.extents.x), mapBounds.center.y + Random.Range (-mapBounds.extents.y, mapBounds.extents.y), 0);

	}

}

