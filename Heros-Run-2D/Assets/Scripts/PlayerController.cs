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

	public void TestJump(){
		GetComponent<Rigidbody2D> ().velocity = Vector2.up * jumpSpeed;
	}
	
	
	public void Update ()
	{	
		int horizontal = 0;     //Used to store the horizontal move direction.
		int vertical = 0;       //Used to store the vertical move direction.
		
		
		//Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
		horizontal = (int) (Input.GetAxisRaw ("Horizontal"));
		
		//Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
		vertical = (int) (Input.GetAxisRaw ("Vertical"));
		
		//Check if moving horizontally, if so set vertical to zero.
		if(horizontal != 0)
		{
			vertical = 0;
		}

	}
	

}