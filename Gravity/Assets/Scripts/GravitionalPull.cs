using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GravitionalPull : MonoBehaviour {
	public float range = 10f;

	Rigidbody self;

	// Use this for initialization
	void Start () {
		self = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		Collider[] cols = Physics.OverlapSphere (transform.position, range);
		List<Rigidbody> rbs = new List<Rigidbody> ();

		foreach (Collider c in cols) {
			Rigidbody rb = c.attachedRigidbody;
			if (rb != null && rb != self && !rbs.Contains(rb))
			{
				rbs.Add (rb);
				Vector3 distance = gameObject.transform.position - c.transform.position;
				rb.AddForce(distance / distance.sqrMagnitude * self.mass);
			}
		}
	}

	void OnCollisionEnter()
	{
		print ("On Enter");
		self.isKinematic = true;
	}

	void OnCollisionExit ()
	{
		print ("On Exit");
		self.isKinematic = false;
	}
	

	void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
