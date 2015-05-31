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
	public float cloudFreq = 1 / 2;

	private List<GameObject> obstacles = new List<GameObject>();
	private List<GameObject> clouds = new List<GameObject> ();

	private GameObject background;

	Vector3 delta;

	private float w;
	private float h;

	Vector2 scale;

	private void BoardSetup(){
		scale = new Vector2 (1, 1);
		boardHolder = new GameObject ("Board").transform;
		background = CreateObject(backgroundObject, new Vector3 (0, 0, 0f));

		for (int i=0; i<10; ++i) {
			CreateObject(cloud, new Vector3(Random.Range(-10,10),Random.Range(3,5),-0.2f));
		}

		StartCoroutine (GenerateCloud());
		StartCoroutine (GenerateObstacle());

		gameStarted = true;
	}

	private float nextCl;
	private float nextOb;
	public IEnumerator<bool> GenerateCloud(){
		for (;;) {
			if (Time.time < nextCl){
				yield return false;
				continue;
			}

			nextCl = Time.time + 1/cloudFreq;

			var c = CreateObject (cloud, new Vector3 (Random.Range (10, 30), Random.Range (3, 5), -0.2f));
			clouds.Add (c);
			c.SetActive (true);
			yield return true;
		}
	}

	public IEnumerator<bool> GenerateObstacle(){
		for (;;) {
			if (Time.time <= nextOb){
				yield return false;
				continue;
			}
		
			nextOb = Time.time + 1 / obstacleFreq;

			var obj = Random.Range (0, 2) == 0 ? Trashcan : fireHydrant;
		
			var c = CreateObject (obj, new Vector3 (Random.Range (10.0f, 30.0f), Random.Range (-1.0f, 1.0f), -0.3f));
			obstacles.Add (c);
			c.SetActive (true);
		
			yield return true;
		}
	}

	public void CheckGameOver(double positionX){
		if (positionX < -10) 
			Application.Quit ();
	}

	public void SetupScene(){
		BoardSetup ();
	}
	private GameObject CreateObject(GameObject obj, Vector3 v){
		var instance = 
			Instantiate(obj, v, 
			            Quaternion.identity) as GameObject;
		instance.transform.SetParent(boardHolder);
		instance.transform.localScale = new Vector3 (instance.transform.localScale.x * scale.x,
		                                            instance.transform.localScale.y * scale.y,
		                                             instance.transform.localScale.z);
		return instance;
	}
	public void Pause(){
		foreach (var sc in obstacles) {
			sc.GetComponent<ScrollingObject>().Disable();
		}
		foreach (var cl in clouds) {
			cl.GetComponent<ScrollingObject>().Disable();
		}
		background.GetComponent<BGScroller> ().Disable ();
	}
	public void Continue(){
		foreach (var sc in obstacles) {
			sc.GetComponent<ScrollingObject>().Disable();
		}
		foreach (var cl in clouds) {
			cl.GetComponent<ScrollingObject>().Disable();
		}
		background.GetComponent<BGScroller>().Enable ();
	}
	public void ObjectDestroyed(GameObject obj){
		if (obstacles.Find (o => o == obj))
			obstacles.Remove (obj);
		if (clouds.Find (o => o == obj))
			clouds.Remove (obj);

		obj.SetActive (false);
		GameObject.Destroy(obj);
	}
}
