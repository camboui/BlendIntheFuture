using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeathHandler : MonoBehaviour {

	public Transform bodiesContainer;
	private HumanController humanController;
	public List<Animator> animatorsBody;
	public List<GameObject> characterRenderers;

	void Start(){
		animatorsBody = new List<Animator> ();

		bodiesContainer = GameObject.Find ("BodiesContainer").transform;
		Transform rendererContainer = transform.FindChild ("Renderers");

		for (int i = 0; i < rendererContainer.childCount; i++) {
			animatorsBody.Add (rendererContainer.GetChild (i).GetComponent<Animator> ());
		}
		humanController = transform.GetComponent<HumanController> ();
		characterRenderers = new List<GameObject> ();
	
		for(int i=0;i<rendererContainer.childCount;i++)
		{
			characterRenderers.Add (rendererContainer.GetChild (i).gameObject);
		}

	}


	void OnDestroy(){
		//If it's a player, remove him from the current round
		if (transform.tag.Equals ("Player")) {
			foreach (Human current in GameCreator.remainingPlayers) {
				if (current.getJoystickId () == humanController.human.getJoystickId ()) {
					GameCreator.remainingPlayers.Remove (current);
					break;
				}
			}
		}

		//Get just renderers, remove its children and launch animation
		foreach (GameObject renderer in characterRenderers) {
			for (int i = 0; i < renderer.transform.childCount; i++) {
				Destroy (renderer.transform.GetChild (i).gameObject);
			}

			renderer.transform.parent = bodiesContainer;
			if (transform.tag.Equals ("Player")) {
				renderer.GetComponent<SpriteRenderer> ().color = humanController.human.getColor ();
			}
		}

		triggerAllAnimators (animatorsBody, "dieTrigger");


	}


	private void triggerAllAnimators(List<Animator> animators,string triggerName)
	{
		foreach (Animator anm in animators) {
			anm.SetTrigger (triggerName);
		}
	}
}
