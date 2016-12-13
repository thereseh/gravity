using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SpaceStation : MonoBehaviour {
	public float mag;
	public float rad;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0,0.3f,0);

		Vector3 diff = transform.position - GameManager.playerShip.transform.position;
		mag = diff.magnitude;
		if (mag <= rad) {
			GameManager.playerShip.GetComponent<Rigidbody>().velocity = diff.normalized * 10 * (mag / rad);
		}
		if (mag <= 6f) {
			WinLevel();
		}
		
	}


	void WinLevel()
	{
		// Reset variables
		GameManager.SlowMo = false;
		PlayerHealth.health = 100f;
		PlayerHealth.TakeDamage(0f);
		GameManager.fuel = 200f;
		
		// Remove black holes
		List<GameObject> bhBu = new List<GameObject>();
		foreach (GameObject blackHole in GameManager.blackHoles)
			bhBu.Add(blackHole);
		foreach (GameObject bh in bhBu)
			GameManager.blackHoles.Remove(bh);

		SceneManager.LoadScene(0);
	}
}
