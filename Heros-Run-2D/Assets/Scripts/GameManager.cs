using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public BoardManager boardScript;	
	public static GameManager instance = null;

	private int score = 0;

	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		
		DontDestroyOnLoad (gameObject);

		boardScript = GetComponent<BoardManager> ();
		InitGame ();
	}

	public void OnJump(){
		++score;
	}

	private void InitGame(){
		var button = GameObject.Find ("ButtonJump").GetComponent<Button> ();

		boardScript.SetupScene ();
	}
	
}
