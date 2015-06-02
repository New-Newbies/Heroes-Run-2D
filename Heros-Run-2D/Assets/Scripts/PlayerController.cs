using UnityEngine;
using System.Collections;

public class PlayerController : MovingObject
{
	public float jumpSpeed = 2;

	private Rigidbody2D rigibody;
	private Animator anim;
	protected override void Start ()
	{
		rigibody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		base.Start ();
	}

	public void Jump(){
		rigibody.velocity = Vector2.up * jumpSpeed;
		anim.speed = 0;
	}

	public void Update ()
	{
		var board = GameManager.instance.boardScript;
		if (board.IsPaused ())
			return;
		board.CheckPosition (transform.position);

		if (anim.speed < Mathf.Epsilon && Mathf.Abs (rigibody.velocity.y) < Mathf.Epsilon) {
			anim.speed = 1;
			gameObject.transform.rotation=Quaternion.identity;
		}
	}
	

}