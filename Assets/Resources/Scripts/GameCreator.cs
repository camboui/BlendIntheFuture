using UnityEngine;
using System.Collections;

public class GameCreator : MonoBehaviour {

	public GameObject prefab_player;
	// Use this for initialization
	void Start () {
		int i = 1;
		foreach (Player p in GameVariables.players) {
			GameObject newGO = Instantiate (prefab_player) as GameObject;
			newGO.GetComponentInChildren<PlayerController> ().playerId = p.getJoystickId ();
			newGO.GetComponentInChildren<SpriteRenderer> ().color = p.getColor ();
			newGO.transform.name = "Player " + i;
			i++;
		}
	}
	

}
