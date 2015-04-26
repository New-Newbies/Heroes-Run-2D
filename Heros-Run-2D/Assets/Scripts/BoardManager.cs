using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {

	private Transform boardHolder;

	public GameObject backgroundObject;

	private void BoardSetup(){
		boardHolder = new GameObject ("Board").transform;

		GameObject instance = 
			Instantiate(backgroundObject, new Vector3(0,0,0f), 
			            Quaternion.identity) as GameObject;
		instance.transform.SetParent(boardHolder);
	}

	public void SetupScene(){
		BoardSetup ();
	}
}
