using UnityEngine;
using System.Collections.Generic;

public class ScrollingObject : MonoBehaviour
{
	
	public float scrollSpeed;
	public float boundry;
	public int score = 1;

	private Vector3 startPosition;
	private float startTime;
	
	private bool enabled = true;

	void Start ()
	{
		startPosition = transform.position;
		startTime = Time.time;
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
	void Update ()
	{
		if (!enabled)
			return;
		
		float newPosition = (Time.time - startTime) * scrollSpeed;

		transform.position = startPosition + Vector3.left * newPosition;

		if (transform.position.x < boundry) {
			GameManager.instance.boardScript.ObjectDestroyed(gameObject);
			GameManager.instance.AddScore(score);
		}
	}
}
