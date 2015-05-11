using UnityEngine;
using System.Collections.Generic;

public class BGScroller : MonoBehaviour
{
	public float scrollSpeed;
	public float tileSizeZ;
	public bool loop;

	private Vector3 startPosition;
	
	void Start ()
	{
		startPosition = transform.position;
	}

	public void Reset(){
		startPosition = transform.position;
	}

	void Update ()
	{
		if (loop) {
			float newPosition = Mathf.Repeat (Time.time * scrollSpeed, tileSizeZ);

			transform.position = startPosition + Vector3.left * newPosition;
		} else {
			float newPosition = Time.time * scrollSpeed;
			if(newPosition> tileSizeZ){
				gameObject.SetActive(false);
				Destroy(this);
			}
			transform.position = startPosition + Vector3.left * newPosition;
		}
	}
}