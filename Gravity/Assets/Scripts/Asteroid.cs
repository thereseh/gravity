using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
	public Vector3 initialVelocity;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().velocity = initialVelocity;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0,0,0.5f);
	}
}
