using UnityEngine;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

	private Transform boardHolder;

	public float scrollSpeed = 0.1f;
	public GameObject backgroundObject;
	public GameObject moon;
	public Vector2 moonPosition;

	private void BoardSetup(){
		boardHolder = new GameObject ("Board").transform;

		var w = Screen.width;
		var h = Screen.height;
		//for (var i=0; i<20; ++i) {
		var obj = CreateObject (backgroundObject, new Vector3 (0*4.25f, 0, 0f));
		obj.transform.localScale = new Vector2 ((w+253)/425f, (h+130)/283f);
			
		//}

	}

	public void SetupScene(){
		BoardSetup ();
	}
	private GameObject CreateObject(GameObject obj, Vector3 v){
		var instance = 
			Instantiate(obj, v, 
			            Quaternion.identity) as GameObject;
		instance.transform.SetParent(boardHolder);
		return instance;
	}
}
