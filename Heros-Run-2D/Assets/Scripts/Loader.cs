﻿using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	public GameObject gameManager;

	// Use this for initialization
	void Awake () {
		if (GameManager.instance == null)
			Instantiate (gameManager);
	}

	public void AddScore(){
		GameManager.instance.AddScore (1);
	}

	public void InitGame(){
		GameManager.instance.boardScript.SetupScene ();
	}

}
