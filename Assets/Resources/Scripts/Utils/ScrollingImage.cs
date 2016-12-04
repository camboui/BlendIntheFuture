using UnityEngine;
using System.Collections;

public class ScrollingImage : MonoBehaviour {

	public float speed;
	private Material myRend;
	private Vector2 offset;

	void Start()
	{
		speed = 0.0001f;
		myRend = GetComponent<Renderer> ().material;
		offset = new Vector2 (0, 0);
	}

	void Update ()
	{
		offset -= new Vector2 (speed,0);
		myRend.mainTextureOffset = offset;
	}
}
