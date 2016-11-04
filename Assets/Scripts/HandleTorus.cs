using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandleTorus : MonoBehaviour {

	public SpriteRenderer mainRenderer;
	public GameObject rendererParent;

	private List<SpriteRenderer> rendererCopies;

	// Use this for initialization
	void Start () {
		rendererCopies = new List<SpriteRenderer> ();
		//calculate camera view size
		float height = Camera.main.orthographicSize * 2;
		float width = height * Camera.main.aspect; 

		if (rendererParent == null)// Inspector priority
			rendererParent = transform.FindChild ("Renderers").gameObject;

		//Find object to be copied
		GameObject playerRendererObject = rendererParent.transform.FindChild ("main").gameObject;

		//Get renderer for smooter Update function
		if (mainRenderer == null)// Inspector priority
			mainRenderer = playerRendererObject.GetComponent<SpriteRenderer> ();

		//add 8 copies of existing renderer
		for (int i = -1; i <= 1; i++) {
			for (int j = -1; j <= 1; j++) {
				if (i != 0 || j != 0) {
					GameObject newGO = Instantiate (playerRendererObject) as GameObject;
					newGO.transform.SetParent (playerRendererObject.transform.parent);
					newGO.transform.localScale = playerRendererObject.transform.localScale;
					newGO.transform.position = playerRendererObject.transform.position + new Vector3 (i * width, j * height, 0);
					rendererCopies.Add (newGO.GetComponent<SpriteRenderer> ());
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {

		if (!mainRenderer.isVisible) {
			//if mainRenderer is out of sight, switch his place with the current renderer
			for (int i = 0; i < rendererCopies.Count; i++) {
				if (rendererCopies [i].isVisible) {
					rendererParent.transform.position = rendererCopies [i].transform.position;
					break;
				}
			}

		}

	}
}
