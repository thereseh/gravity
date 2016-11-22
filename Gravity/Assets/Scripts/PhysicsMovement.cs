using UnityEngine;
using System.Collections;

public class PhysicsMovement : MonoBehaviour
{
	[Range(0f, 1f)]
	public float friction;
	public float mass;
	public Vector3 initialImpulse = Vector3.zero;
	public float maxVelocity;
	public bool rotateChild;
	public bool lockZposition;
	[HideInInspector]
	public Vector3 velocity = Vector3.zero;
	[HideInInspector]
	public Vector3 acceleration = Vector3.zero;
	Vector3 prevVelocity = Vector3.zero;
	float originalZposition;

	void Start ()
	{
		AddForce(initialImpulse);
		originalZposition = transform.position.z;
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

		prevVelocity = velocity;
		velocity += acceleration;
		if (velocity.magnitude > maxVelocity)
			velocity = velocity.normalized * maxVelocity;
		acceleration = Vector3.zero;

		Friction();

		if (GameManager.SlowMo)
			transform.position += velocity * 0.25f;
		else
			transform.position += velocity;

		// Look in the direction of movement
		if (rotateChild)
		{
			Vector3 normalizedDirection = velocity.normalized;
			if (velocity == Vector3.zero)
				normalizedDirection = Vector3.one;
			float rotationAngle = Vector3.Angle(Vector3.right, normalizedDirection);
			if (velocity.y > 0)
				rotationAngle = 180 - rotationAngle + 180;
			if (velocity.magnitude == 0)
				rotationAngle = 0;

			transform.GetChild(0).LookAt(transform.GetChild(0).position - Vector3.forward);
			transform.GetChild(0).RotateAround(transform.GetChild(0).position, -Vector3.forward, rotationAngle);
		}

		// Stay at the same Z coordinate
		if (lockZposition)
		{
			transform.Translate(new Vector3(0, 0, -transform.position.z));
			transform.Translate(new Vector3(0, 0, originalZposition));
		}
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
				//print("Collision at " + pt.point + "  |  Separation: " + pt.separation);
			AddForce((transform.position - pt.point) * 1.5f * velocity.magnitude);
			PlayerHealth.TakeDamage(velocity.magnitude / maxVelocity * 35f);
		}
	}

	void OnGizmosDraw()
	{
		Gizmos.DrawLine(transform.position, transform.position + velocity);
	}
}
