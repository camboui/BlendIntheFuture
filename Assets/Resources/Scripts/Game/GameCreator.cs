using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor;

public class GameCreator : MonoBehaviour {
	
	public static List<Human> remainingPlayers;
	public GameObject prefab_player;
	public GameObject prefab_IA;
	public Transform parentIA;
	private int nbIA=30;//TODO make classes for game data
	private static Bounds mapBounds;

	void Start () {
		int i = 1;
		remainingPlayers = new List<Human> ();
		remainingPlayers.AddRange(GameVariables.players);
		mapBounds = GameObject.FindGameObjectWithTag ("Map").GetComponentInChildren<Collider2D> ().bounds;

		//Instantiate players
		foreach (Human p in GameVariables.players) {
			GameObject newGO = Instantiate (prefab_player) as GameObject;
			newGO.GetComponentInChildren<HumanController> ().playerId = p.getJoystickId ();
			// DEBUG : SHOW COLOR
			//newGO.GetComponentInChildren<SpriteRenderer> ().color = p.getColor ();
			newGO.transform.name = "Player " + i;
			newGO.transform.position = randomPosOnMap ();
			newGO.transform.position -= newGO.transform.FindChild ("GroundCheck").transform.localPosition;
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
			newGO.transform.position -= newGO.transform.FindChild ("GroundCheck").transform.localPosition;
			newGO.transform.name = "IA_" + i;

			i++;
		}
			
		GameObject rules = Instantiate (GameVariables.selectedMode) as GameObject;
		rules.transform.FindChild ("Graphic").gameObject.SetActive (false);
		rules.transform.FindChild ("Rules").gameObject.SetActive (true);
	}

	public static Vector3 randomPosOnMap()
	{
		return new Vector3 (mapBounds.center.x+ Random.Range (-mapBounds.extents.x, mapBounds.extents.x), mapBounds.center.y + Random.Range (-mapBounds.extents.y, mapBounds.extents.y), 0);

	}

}

