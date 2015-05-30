using UnityEngine;
using System.Collections;

public class PlayerController : MovingObject
{
	public float jumpSpeed = 2;
	
	//Start overrides the Start function of MovingObject
	protected override void Start ()
	{
		//Physics2D.gravity = new Vector2 (0, -1f);
		
		//Call the Start function of the MovingObject base class.
		base.Start ();
	}

	public void Jump(){
		GetComponent<Rigidbody2D> ().velocity = Vector2.up * jumpSpeed;
	}

	public void Update ()
	{	
		var board = GameManager.instance.boardScript;
		board.CheckGameOver (transform.position.x);

	}
	

}