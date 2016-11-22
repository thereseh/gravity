using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	static public List<MatterDistort> matterDistortions = new List<MatterDistort>(); // Black Holes and Dark Matters
	static public GameObject playerShip;
	public GameObject blackhole;
	public List<GameObject> blackHoles;
	static public bool DebuggingMode = false;
	public bool debuggingMode = false;

	static public Vector3 SpawnPoint;

	public string level;
	public string levelName;
	private float radius = 1f;
	private Vector3 blackHolePos;
	private GameObject placeHolderBlackHole;

	static public bool SlowMo = false;

	void Start()
	{
		DebuggingMode = debuggingMode;
	//	matterDistortions.Add(GameObject.Find("BlackHole").GetComponent<MatterDistort>());
		playerShip = GameObject.Find("PlayerShip");
		SpawnPoint = playerShip.transform.position;
		blackHoles = new List<GameObject> ();
		GameObject.Find("CurrentLevelDisplay").GetComponent<Text>().text = "Level " + level + ": " + levelName;
	}

	// Every frame
	void Update()
	{

		DebuggingMode = debuggingMode;
		foreach (MatterDistort distorter in matterDistortions)
		{
			/*
			 * float xSquared = (distorter.position.x - playerShip.transform.position.x) * (distorter.position.x - playerShip.transform.position.x);
			float ySquared = (distorter.position.y - playerShip.transform.position.y) * (distorter.position.y - playerShip.transform.position.y);
			float distance = Mathf.Sqrt(xSquared + ySquared);
			if (distance > 0)
			{
				float angle = Mathf.Atan((distorter.position.y - playerShip.transform.position.y) / (distorter.position.x - playerShip.transform.position.x));
				float moveX = distance * Mathf.Cos(angle);
				float moveY = distance * Mathf.Sin(angle);
				playerShip.transform.Translate(new Vector3(moveX * Time.deltaTime, moveY * Time.deltaTime, 0));
			}
			*/
		}

		if (Input.GetMouseButton(0)) {
			print (Input.mousePosition);
			blackHolePos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10.0f));
			radius += 0.01f;
			print (blackHolePos);
			print (blackHolePos.x);
			print (blackHolePos.y);	

			Destroy(placeHolderBlackHole);
			radius = Mathf.Clamp(radius, 1f, 5f);

			placeHolderBlackHole = Instantiate (blackhole, blackHolePos, Quaternion.identity) as GameObject;
			placeHolderBlackHole.transform.localScale = placeHolderBlackHole.transform.localScale * radius;
		}

		radius = Mathf.Clamp(radius, 1f, 5f);


		if (Input.GetMouseButtonUp (0)) {
			Destroy(placeHolderBlackHole);
			radius = Mathf.Clamp(radius, 1f, 5f);

			blackHolePos.z = 0f;
			GameObject copy = Instantiate (blackhole, blackHolePos, Quaternion.identity) as GameObject;
			copy.transform.localScale = copy.transform.localScale * radius;
			copy.GetComponent<Rigidbody>().mass = radius * 5;
			copy.GetComponent<Rigidbody>().drag = radius * 3;
			copy.GetComponent<GravitionalPull>().range = radius + 3f;
			blackHoles.Add(copy);
			radius = 1;

		}



	}

	public void BlackHoleCollision(float mass, Vector3 radius, Vector3 pos)
	{
		GameObject bHole = Instantiate (blackhole, pos, Quaternion.identity) as GameObject;
		bHole.transform.localScale = radius;
		bHole.GetComponent<Rigidbody> ().mass = mass;
		bHole.GetComponent<Rigidbody> ().drag = mass;
		bHole.GetComponent<GravitionalPull> ().range = radius.x + 3f;
	}
}
