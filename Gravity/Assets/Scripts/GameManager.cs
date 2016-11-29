using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	static public GameObject playerShip;
	public GameObject blackhole;

	public List<GameObject> blackHoles;

	static public bool DebuggingMode = false;
	public bool debuggingMode = false;

	static public Vector3 SpawnPoint;

	public string level;
	public string levelName;
	private float radius = 0.5f;
	private Vector3 blackHolePos;
	private GameObject placeHolderBlackHole;
	bool cancel = false;

	static public bool SlowMo = false;

	void Start()
	{
		DebuggingMode = debuggingMode;
		playerShip = GameObject.Find("PlayerShip");
		SpawnPoint = playerShip.transform.position;
		GameObject.Find("CurrentLevelDisplay").GetComponent<Text>().text = "Level " + level + ": " + levelName;
	}

	// Every frame
	void Update()
	{
        // Toggle debugging mode in the editor
		DebuggingMode = debuggingMode;

		if (Input.GetMouseButton(0)) {
			blackHolePos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10.0f));
			radius += 0.01f;

			Destroy(placeHolderBlackHole);
			radius = Mathf.Clamp(radius, 0.5f, 5f);
			placeHolderBlackHole = Instantiate (blackhole, blackHolePos, Quaternion.identity) as GameObject;
			placeHolderBlackHole.tag = "PlaceHolder";
			placeHolderBlackHole.transform.localScale = placeHolderBlackHole.transform.localScale * radius;
		}

		radius = Mathf.Clamp(radius, 0.5f, 5f);

		if (Input.GetMouseButtonUp (0)) {
			if (placeHolderBlackHole != null)
			{
				Destroy(placeHolderBlackHole);
			}
			for(int i = 0; i < blackHoles.Count; i++)
			{
				Vector3 distance = blackHoles[i].transform.position - blackHolePos;
				float mag = distance.magnitude;
				print ("mag: " + mag);
				print ("x + rad: " + (blackHoles[i].transform.localScale.x + radius));
				if (mag <= (blackHoles[i].transform.localScale.x + radius)/2f)
				{
					cancel = true;
				}
			}

			if (!cancel)
            {
			    radius = Mathf.Clamp(radius, 0.5f, 5f);

			    blackHolePos.z = 0f;
			    GameObject copy = Instantiate (blackhole, blackHolePos, Quaternion.identity) as GameObject;
			    copy.transform.localScale = copy.transform.localScale * radius;
			    copy.GetComponent<Rigidbody>().mass = radius * 10f;
			    copy.GetComponent<Rigidbody>().drag = radius * 5f;
			    copy.GetComponent<GravitionalPull>().range = radius + 5f;
                blackHoles.Add(copy);
			}
			radius = 0.5f;
			cancel = false;
		}
	}
}
