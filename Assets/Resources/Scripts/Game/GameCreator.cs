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
			newGO.GetComponentInChildren<HumanController> ().human = p;
			// DEBUG : SHOW COLOR
			//newGO.GetComponentInChildren<SpriteRenderer> ().color = p.getColor ();
			newGO.transform.name = "Player " + i;
			Vector3 position = randomPosOnMap (newGO.transform.FindChild ("GroundCheck").transform.localPosition);

			newGO.transform.position = position;
			GameObject bonus = Instantiate (p.getBonus (), GameObject.FindGameObjectWithTag ("Map").transform) as GameObject;
			bonus.name = "Bonus_JoystickId" + p.getJoystickId ();
			bonus.GetComponent<Bonus_Abstract> ().enabled = false;
			if (bonus.GetComponentInChildren<ParticleSystem> () != null){
				Debug.Log ("smokeball");
				bonus.GetComponentInChildren<ParticleSystem> ().Stop (); 
				bonus.GetComponent<SmokeBall> ().player = newGO;
			}
			i++;
		}
			
		int rand = Random.Range (0, GameVariables.steeringScripts.Count);
		Debug.Log ("Steering type = " + GameVariables.steeringScripts[rand]);
		for (int j = 0; j < nbIA; j++) {
			GameObject newGO = Instantiate (prefab_IA, parentIA) as GameObject;
			newGO.AddComponent(System.Type.GetType(GameVariables.steeringScripts[rand]));

			newGO.transform.name = "Player " + i;
			Vector3 position = randomPosOnMap (newGO.transform.FindChild ("GroundCheck").transform.localPosition);
			newGO.transform.position = position;

			newGO.transform.name = "IA_" + i;

			i++;
		}
			
		GameObject rules = Instantiate (GameVariables.selectedMode) as GameObject;
		rules.transform.FindChild ("Graphic").gameObject.SetActive (false);
		rules.transform.FindChild ("Rules").gameObject.SetActive (true);
	}

	public static Vector3 randomPosOnMap(Vector3 offset)
	{
		return new Vector3 (mapBounds.center.x+ Random.Range (-mapBounds.extents.x, mapBounds.extents.x)-offset.x, mapBounds.center.y + Random.Range (-mapBounds.extents.y, mapBounds.extents.y)-offset.y, 0);

	}

}

