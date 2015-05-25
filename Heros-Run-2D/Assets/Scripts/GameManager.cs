using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

	public BoardManager boardScript;	
	public static GameManager instance = null;

	private int score = 0;
	private Text scoreText;
	private Image StartButton;

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

	public void AddScore(int scoreDelta){
		score += scoreDelta;
		scoreText.text = score.ToString ("d8");
	}

	private void InitGame(){
		var button = GameObject.Find ("ButtonJump").GetComponent<Button> ();
		scoreText = GameObject.Find ("Score").GetComponent<Text> ();

		button.onClick.AddListener (() => {
			AddScore(1);
		});
	}
	
}
