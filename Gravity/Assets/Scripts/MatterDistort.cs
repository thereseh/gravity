using UnityEngine;
using System.Collections;

public class MatterDistort : MonoBehaviour
{
	public float mass;
	public Vector3 position;
	float eventHorizon = 5f;

	void Start()
	{
		// Set this object's position
		position = transform.position;

		// Calculate the event horizon
	}

	void Move(Vector3 mv)
	{
		transform.position += mv;
		position = transform.position;
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(position, eventHorizon);
	}
}
