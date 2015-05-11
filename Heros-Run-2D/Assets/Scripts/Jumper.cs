using UnityEngine;
using System.Collections.Generic;

public class Jumper : MonoBehaviour
{
	public float scrollSpeed;
	public float tileSizeZ;
	public bool loop;
	
	private Vector3 startPosition;
	
	void Start ()
	{
		startPosition = transform.position;
		//anim.SetBool("Ground", false);
		GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
	}
	
	public void Reset(){
		startPosition = transform.position;
	}
	
	void Jump ()
	{


	}
}