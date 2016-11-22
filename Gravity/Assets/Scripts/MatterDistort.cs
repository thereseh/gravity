using UnityEngine;
using System.Collections;

public class MatterDistort : MonoBehaviour
{
    public float maxDistance = 5f;
	public float mass;
	public Vector3 position;

	void Start()
	{
		// Set this object's position
		position = transform.position;
	}

	void Move(Vector3 mv)
	{
		transform.position += mv;
		position = transform.position;
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(position, maxDistance);
	}
}
