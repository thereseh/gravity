using UnityEngine;
using System.Collections;

public class PhysicsMovement : MonoBehaviour
{
	[Range(0f, 1f)]
	public float friction;
	public float mass;
	public Vector3 initialImpulse = Vector3.zero;
	public bool rotateChild;
	[HideInInspector]
	public Vector3 velocity = Vector3.zero;
	[HideInInspector]
	public Vector3 acceleration = Vector3.zero;

	void Start ()
	{
		AddForce(initialImpulse);
	}

	void Update ()
	{
		if (GameManager.DebuggingMode)
		{
			if (Input.GetKey(KeyCode.UpArrow))
				AddForce(new Vector3(0, 0.0025f, 0));
			if (Input.GetKey(KeyCode.DownArrow))
				AddForce(-new Vector3(0, 0.0025f, 0));
			if (Input.GetKey(KeyCode.LeftArrow))
				AddForce(-new Vector3(0.0025f, 0, 0));
			if (Input.GetKey(KeyCode.RightArrow))
				AddForce(new Vector3(0.0025f, 0, 0));
		}

		velocity += acceleration;
		acceleration = Vector3.zero;

		Friction();

		transform.position += velocity;

		Vector3 normalizedDirection = velocity.normalized;
		transform.GetChild(0).rotation = Quaternion.Euler(normalizedDirection * 360f + new Vector3(-90f, 0, 0));
	}

	public void AddForce(Vector3 force)
	{
		acceleration += force / mass;
	}

	void Friction()
	{
		float damping = 1f - friction;
		velocity *= damping;
	}

	void OnCollisionEnter(Collision coll)
	{
		foreach (ContactPoint pt in coll.contacts)
		{
			if (GameManager.DebuggingMode)
				print("Collision at " + pt.point + "  |  Separation: " + pt.separation);
			AddForce((transform.position - pt.point) * 3f * velocity.magnitude);
		}
	}
}
