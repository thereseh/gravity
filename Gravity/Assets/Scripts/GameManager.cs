using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	static public GameObject playerShip;
    static public List<GameObject> blackHoles = new List<GameObject>();
	public List<GameObject> asteroids;

    static public bool DebuggingMode = false;
    public bool debuggingMode = false;

    static public Vector3 SpawnPoint;

    public GameObject blackholePrefab;

	public string level;
	public string levelName;

    private float placeholderRadius = 3f;
	private GameObject placeHolderBlackHole;
    private Vector3 defaultPlaceholderScale;

    static public bool SlowMo = false;

    static public bool transportingThroughWormhole = false;

    static public float timeStartedLevel = 0f;

    void Start()
	{
        timeStartedLevel = Time.time;
        transportingThroughWormhole = false;
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

        // Placing new matter distortions
        PlaceBlackHoles();
    }

    void PlaceBlackHoles()
    {
        // Mouse button gets pressed, start making new black hole
		if (Input.GetMouseButtonDown(0) && placeHolderBlackHole == null)
        {
			Vector3 newBlackHolePos = Camera.main.ScreenToWorldPoint(new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                15.0f
            ));
            newBlackHolePos = new Vector3(
                newBlackHolePos.x,
                newBlackHolePos.y,
                0f // Keep on the default X-Y plane
            );

            placeHolderBlackHole = Instantiate(blackholePrefab, newBlackHolePos, Quaternion.identity) as GameObject;
            defaultPlaceholderScale = placeHolderBlackHole.transform.localScale;
            placeHolderBlackHole.transform.localScale = defaultPlaceholderScale * placeholderRadius;
        }

        // Increase size of new black hole while mouse button is held down
        else if (Input.GetMouseButton(0) && placeHolderBlackHole != null)
        {
            /*placeholderRadius += 0.01f;
            placeholderRadius = Mathf.Clamp(placeholderRadius, 0.5f, 5f);
            placeHolderBlackHole.transform.localScale = defaultPlaceholderScale * placeholderRadius;
            */
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                placeholderRadius += 0.3f;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                placeholderRadius -= 0.3f;
            }
            placeholderRadius = Mathf.Clamp(placeholderRadius, 0.5f, 5f);
            placeHolderBlackHole.transform.localScale = defaultPlaceholderScale * placeholderRadius;

            Vector3 newBlackHolePos = Camera.main.ScreenToWorldPoint(new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                15.0f
            ));
            newBlackHolePos = new Vector3(
                newBlackHolePos.x,
                newBlackHolePos.y,
                0f // Keep on the default X-Y plane
            );
            placeHolderBlackHole.transform.position = newBlackHolePos;
        }

        // Let go of mouse button to place new black hole
        else if (Input.GetMouseButtonUp(0) && placeHolderBlackHole != null)
        {
            bool tooCloseToAnotherBlackhole = false;

			for (int i = 0; i < blackHoles.Count; i++)
			{
				Vector3 distance = blackHoles[i].transform.position - placeHolderBlackHole.transform.position;
				float mag = distance.magnitude;
				//print ("mag: " + mag);
				//print ("x + rad: " + (blackHoles[i].transform.localScale.x + radius));
				if (mag <= (blackHoles[i].transform.localScale.x + placeholderRadius) / 2f)
                    tooCloseToAnotherBlackhole = true;
			}

			if (!tooCloseToAnotherBlackhole)
            {
                // Remove black holes
                List<GameObject> bhBu = new List<GameObject>();
                foreach (GameObject blackHole in blackHoles)
                    bhBu.Add(blackHole);
                foreach (GameObject bh in bhBu)
                {
                    bh.SetActive(false);
                    Destroy(bh);
                    blackHoles.Remove(bh);
                }

                // Create new black hole
                placeHolderBlackHole.transform.position = new Vector3(
                    placeHolderBlackHole.transform.position.x,
                    placeHolderBlackHole.transform.position.y,
                    0f // Keep on the default X-Y plane
                );
                GameObject copy = Instantiate(blackholePrefab, placeHolderBlackHole.transform.position, Quaternion.identity) as GameObject;
			    copy.transform.localScale = defaultPlaceholderScale * placeholderRadius;
			    copy.GetComponent<Rigidbody>().mass = placeholderRadius * 15f;
			    copy.GetComponent<Rigidbody>().drag = 0f;
                copy.GetComponent<GravitionalPull>().active = true;
                copy.GetComponent<SphereCollider>().isTrigger = false;
                copy.tag = "Blackhole";
                blackHoles.Add(copy);
			}
            placeholderRadius = 3f;

            Destroy(placeHolderBlackHole);
            placeHolderBlackHole = null;
        }
	}
}
