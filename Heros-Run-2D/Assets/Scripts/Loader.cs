using UnityEngine;
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
		Screen.orientation = ScreenOrientation.Landscape;
		GameManager.instance.boardScript.SetupScene ();
	}

	public void TriggerPlaying(){
		Playing = !Playing;
	}

	private bool playing=true;
	private bool Playing {
		get{
			return playing;
		}
		set {
			if(value && !playing)
				GameManager.instance.boardScript.Continue();

			if(!value && playing)
				GameManager.instance.boardScript.Pause();

			playing = value;
		}
	}

}
