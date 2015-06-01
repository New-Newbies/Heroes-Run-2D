using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour {

	public float scrollSpeed=0.5f;

	private Vector2 startTextureOffset;
	private float startTime;
	private Renderer renderer;

	// Use this for initialization
	void Start () {
		renderer = GetComponent<Renderer> ();
		startTime = Time.time;
		startTextureOffset = renderer.material.mainTextureOffset;
	}

	public void Disable(){
		enabled = false;
	}
	public void Enable(){
		if (!enabled) {
			Start ();
			enabled = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!enabled)
			return;

		Vector2 offset = new Vector2 ((Time.time - startTime) * scrollSpeed, 0);

		renderer.material.mainTextureOffset = startTextureOffset + offset;
	}
}