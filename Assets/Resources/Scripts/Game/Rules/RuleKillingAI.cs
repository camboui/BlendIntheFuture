using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class RuleKillingAI : MonoBehaviour {

	public int max;

	private List<HumanController> humanControllers;
	private GameObject[] MalusGO;
	private List<Malus_Abstract> malus;

	// Use this for initialization
	void Start () {
		//Check rule Every 1 second. Let some time for draw 
		InvokeRepeating("checkRule", 1.0f, 1.0f);
		humanControllers = new List<HumanController> ();
		foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player")) {
			humanControllers.Add(p.GetComponent<HumanController>());
		}
		MalusGO = GameObject.FindGameObjectsWithTag("Malus");
		malus = new List<Malus_Abstract>();
		foreach (GameObject m in MalusGO) {
			malus.Add (m.GetComponent<Malus_Abstract> ());
		}
	}

	void checkRule()
	{
		foreach (HumanController current in humanControllers) {
			if (current.human.getNumberAIKilled() >= max) {
				Malus_Abstract malus = aleaMalus();
				malus.humanController = current; 
				malus.enabled = true;
				Instantiate (malus, current.transform);
				current.human.resetNumberAIKilled ();
			}
		}
	}

	private Malus_Abstract aleaMalus(){	
		Debug.Log ("malus count" + malus.Count);	
		return malus[Random.Range(0,malus.Count)];
	}
}
