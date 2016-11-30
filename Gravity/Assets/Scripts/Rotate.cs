using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
    public bool useRigidBody = false;
    public Vector3 angularVelocity = Vector3.zero;

    void Start()
    {
        if (useRigidBody)
            GetComponent<Rigidbody>().angularVelocity = angularVelocity;
    }

	void Update()
    {
	    if (!useRigidBody)
            transform.Rotate(angularVelocity * Time.deltaTime);
	}
}
