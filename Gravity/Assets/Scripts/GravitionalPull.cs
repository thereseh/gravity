using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GravitionalPull : MonoBehaviour
{
    public bool active = false;

	public float range;
	GameManager gameManager;

	Rigidbody self;
    
	void Start()
    {
		self = GetComponent<Rigidbody>();
		gameManager = GameObject.Find("MainCamera").GetComponent<GameManager>();
	}

	void Update()
	{
        if (!active)
            return;

		Collider[] cols = Physics.OverlapSphere(transform.position, range);

		foreach (Collider c in cols)
        {
			Rigidbody rb = c.attachedRigidbody;
			if (rb != null && rb != self && c.gameObject.name == GameManager.playerShip.name)
			{
				Vector3 distance = gameObject.transform.position - c.transform.position;
				rb.AddForce(distance / distance.sqrMagnitude * self.mass);
			}
		}
	}

	void OnCollisionEnter(Collision obj)
	{
		/*if (obj.gameObject.tag == "Blackhole" && gameObject.tag != "PlaceHolder") {
			float masses = self.mass + obj.gameObject.GetComponent<Rigidbody> ().mass;
			Vector3 rad1 = gameObject.transform.localScale;
			Vector3 rad2 = obj.transform.localScale;
			Vector3 sumRadius = rad1 + rad2;
			Vector3 pos = Vector3.zero;
			if (rad1.sqrMagnitude > rad2.sqrMagnitude)
			{
				pos = gameObject.transform.position;
				gameManager.BlackHoleCollision (masses, sumRadius, pos);

			}

			Destroy(obj.gameObject);
			Destroy(gameObject);
		} else {*/
			self.isKinematic = true;
	}

	void OnCollisionExit ()
	{
		self.isKinematic = false;
	}
	

	void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
