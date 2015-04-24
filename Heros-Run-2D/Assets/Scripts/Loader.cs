using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		if (GameManager.instance == null)
			Instantiate (gameManager);
	}

}
