using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ForceMapFitScreen : MonoBehaviour {

	SpriteRenderer sr;	
	private Transform groundPosition;

	void Start () { 

		sr = gameObject.GetComponent<SpriteRenderer> ();
		transform.localScale = new Vector3(1,1,1);

		float width = sr.sprite.bounds.size.x;
		float height = sr.sprite.bounds.size.y;

		float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

		Vector3 newSize = transform.localScale;
		newSize.x = worldScreenWidth / width;
		newSize.y = worldScreenHeight / height;
		transform.localScale = newSize;
	}
}
