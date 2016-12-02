using UnityEngine;
using System.Collections;

public class DeathHandler : MonoBehaviour {

	private HumanController humanController;

	void Start(){
		humanController = transform.GetComponent<HumanController> ();
	}


	void OnDestroy(){
		foreach (Human current in GameCreator.remainingPlayers) {
			if (current.getJoystickId()== humanController.human.getJoystickId()) {
				GameCreator.remainingPlayers.Remove (current);
				break;
			}
		}
	}
}
