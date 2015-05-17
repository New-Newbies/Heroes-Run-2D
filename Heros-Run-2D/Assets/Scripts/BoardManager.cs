using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	private Transform boardHolder;

	public float scrollSpeed = 0.1f;
	public Text score;
	public GameObject backgroundObject;
	public GameObject cloud;
	public GameObject fireHydrant;
	public GameObject Trashcan;
	public Vector2 offsetRate = new Vector2 (2 / 3.0f, 2 / 3.0f);

	private GameObject bg1;
	private GameObject bg2;
	private GameObject bg3;

	Vector3 delta;

	private float w;
	private float h;

	Vector2 scale;

	private void BoardSetup(){
		boardHolder = new GameObject ("Board").transform;

		bg1 = CreateObject (backgroundObject, new Vector3 (0, 0, 0f));
		bg2 = CreateObject (backgroundObject, new Vector3 (0, 0, 0f));
		bg3 = CreateObject (backgroundObject, new Vector3 (0, 0, 0f));

		SetPos ();

		for (int i=0; i<10; ++i) {
			var c = CreateObject (cloud, new Vector3 (Random.Range(-delta.x, delta.x),
			                                          Random.Range (delta.y / 2f - 2.2f, delta.y / 2f - 0.2f), 0f));
			var sc = c.GetComponent<BGScroller> ();
			sc.tileSizeZ = delta.x + c.transform.position.x;
			c.SetActive (true);
			sc.SetResetFunc (o => {
				o.transform.position = new Vector3 (Random.Range(delta.x, delta.x*0.1f),
				                                    Random.Range (delta.y / 2f - 2.2f, delta.y / 2f - 0.2f), 0f);
				sc.tileSizeZ = delta.x + c.transform.position.x;
			});
		}

	}

	Vector3 GetRandomPosition(){
		return new Vector3 (Random.Range (1, 10), Random.Range (1, 10), 0f);
	}

	void SetPos(){
		w = Screen.width;
		h = Screen.height;
		var p0 = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));
		var p1 = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 0));
		delta = p1 - p0;
		scale = new Vector2 (delta.x/4.25f, delta.y/2.83f);

		ResetBackgroundObject (bg1, -delta.x/2, delta.x, scale);
		ResetBackgroundObject (bg2, delta.x/2, delta.x, scale);
		ResetBackgroundObject (bg3, delta.x + delta.x/2, delta.x, scale);
	}

	private void ResetBackgroundObject(
			GameObject bg, float xOff, float tileSize, Vector2 scale){
		bg.transform.position = new Vector3 (xOff, 0, 0f);
		bg.transform.localScale = scale;
		var sc = bg.GetComponent<BGScroller> ();
		sc.tileSizeZ = tileSize;
		sc.Reset ();
	}

	void Update(){
		if (Mathf.Abs(Screen.width - w)<float.Epsilon && Mathf.Abs(Screen.height - h)<float.Epsilon)
			return;
		SetPos ();
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
