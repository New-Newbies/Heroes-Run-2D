using UnityEngine;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

	private Transform boardHolder;

	public float scrollSpeed = 0.1f;
	public GameObject backgroundObject;

	private GameObject[] backgroundTiles;

	private void BoardSetup(){
		boardHolder = new GameObject ("Board").transform;

		backgroundTiles = new GameObject[20];

		for (var i=0; i<20; ++i) {
			CreateBackground (i, new Vector3 ((i-10)*4.25f, 0, 0f));
		}

	}

	public void SetupScene(){
		BoardSetup ();
	}

	private void CreateBackground(int i, Vector3 v){
		var instance = 
			Instantiate(backgroundObject, v, 
			            Quaternion.identity) as GameObject;
		instance.transform.SetParent(boardHolder);
		backgroundTiles [i] = instance;
	}
}
