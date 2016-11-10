using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	static public List<MatterDistort> matterDistortions = new List<MatterDistort>(); // Black Holes and Dark Matters
	static public GameObject playerShip;

	static public bool DebuggingMode = false;
	public bool debuggingMode = false;

	void Start()
	{
		DebuggingMode = debuggingMode;
		matterDistortions.Add(GameObject.Find("BlackHole").GetComponent<MatterDistort>());
		playerShip = GameObject.Find("PlayerShip");
	}

	// Every frame
	void Update()
	{
		DebuggingMode = debuggingMode;
		foreach (MatterDistort distorter in matterDistortions)
		{
			/*float xSquared = (distorter.position.x - playerShip.transform.position.x) * (distorter.position.x - playerShip.transform.position.x);
			float ySquared = (distorter.position.y - playerShip.transform.position.y) * (distorter.position.y - playerShip.transform.position.y);
			float distance = Mathf.Sqrt(xSquared + ySquared);
			if (distance > 0)
			{
				float angle = Mathf.Atan((distorter.position.y - playerShip.transform.position.y) / (distorter.position.x - playerShip.transform.position.x));
				float moveX = distance * Mathf.Cos(angle);
				float moveY = distance * Mathf.Sin(angle);
				playerShip.transform.Translate(new Vector3(moveX * Time.deltaTime, moveY * Time.deltaTime, 0));
			}*/
		}
	}
}
