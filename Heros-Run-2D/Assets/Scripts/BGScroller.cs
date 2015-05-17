using UnityEngine;
using System.Collections.Generic;

public class BGScroller : MonoBehaviour
{
	public float scrollSpeed;
	public float tileSizeZ;
	public bool loop;

	public delegate void Action<T>(T t);

	private Vector3 startPosition;
	private float startTime;

	private Action<GameObject> resetFunc = t => {};

	public void SetResetFunc(Action<GameObject> a){
		resetFunc = a;
	}
	
	void Start ()
	{
		startPosition = transform.position;
		startTime = Time.time;
	}

	public void Reset(){
		startPosition = transform.position;
	}

	void Update ()
	{
		if (tileSizeZ == 0)
			return;
		if (loop) {
			float newPosition = Mathf.Repeat (Time.time * scrollSpeed, tileSizeZ);

			transform.position = startPosition + Vector3.left * newPosition;
		} else {
			float newPosition = (Time.time - startTime) * scrollSpeed;
			if(newPosition> tileSizeZ){
				resetFunc(gameObject);
				Start ();
			}
			transform.position = startPosition + Vector3.left * newPosition;
		}
	}
}