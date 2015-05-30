using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	private Transform boardHolder;
	private bool gameStarted = false;

	public float scrollSpeed = 0.1f;
	public Text score;
	public GameObject backgroundObject;
	public GameObject cloud;
	public GameObject fireHydrant;
	public GameObject Trashcan;
	public Vector2 offsetRate = new Vector2 (2 / 3.0f, 2 / 3.0f);
	public float obstacleFreq = 1 / 2;

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
			var c = CreateObject (cloud, new Vector3 (Random.Range(-delta.x/2, delta.x/2),
			                                          Random.Range (delta.y / 2f - 2.2f, delta.y / 2f - 0.2f), 0f));
			var sc = c.GetComponent<BGScroller> ();
			sc.tileSizeZ = delta.x + c.transform.position.x;
			c.SetActive (true);
			sc.SetResetFunc (o => {
				o.transform.position = new Vector3 (Random.Range(delta.x, delta.x + delta.x*0.1f),
				                                    Random.Range (delta.y / 2f - 2.2f, delta.y / 2f - 0.2f), 0f);
				var sc1 = c.GetComponent<BGScroller> ();
				sc1.tileSizeZ = delta.x + c.transform.position.x;
				sc1.Reset();
			});
		}
		gameStarted = true;
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

	public void CheckGameOver(double positionX){
		if (positionX < -delta.x/2) 
			GameObject.Find ("GameOver").SetActive (true);
	}

	private float nextOb = 0;
	void Update(){
		if (!gameStarted)
			return;

		if (Mathf.Abs(Screen.width - w)>=float.Epsilon && Mathf.Abs(Screen.height - h)>=float.Epsilon)
			SetPos ();

		if (Time.time <= nextOb)
			return;

		var obj = Random.Range (0, 2) == 0 ? Trashcan : fireHydrant;

		var c = CreateObject (obj, new Vector3 (Random.Range(2*delta.x, 3*delta.x),
		                                          Random.Range (delta.y / 2f - 6.5f, delta.y / 2f - 5.5f), 0f));
		var sc = c.GetComponent<BGScroller> ();
		sc.tileSizeZ = delta.x + c.transform.position.x;
		if(obj==Trashcan)
			sc.transform.localScale = new Vector2 (0.2f, 0.2f);
		c.SetActive (true);
		sc.SetResetFunc (o => {
			o.SetActive(false);
			Destroy(o);
		});

		nextOb = Time.time + 1/obstacleFreq;
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
