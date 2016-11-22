using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour
{
    public float mass;
    Vector3 acceleration = Vector3.zero;
    Vector3 velocity = Vector3.zero;
    CharacterController controller;

	void Start()
    {
        controller = GetComponent<CharacterController>();
	}
	
	void Update()
    {
        // User input
        float userInputMultiplier = 10f;
        if (Input.GetKey(KeyCode.D))
            ApplyForce(Vector3.right * userInputMultiplier);
        if (Input.GetKey(KeyCode.A))
            ApplyForce(-Vector3.right * userInputMultiplier);
        if (Input.GetKey(KeyCode.W))
            ApplyForce(Vector3.up * userInputMultiplier);
        if (Input.GetKey(KeyCode.S))
            ApplyForce(-Vector3.up * userInputMultiplier);

        // Apply black hole gravity
        foreach (MatterDistort distorter in GameManager.matterDistortions)
        {
            Vector3 difference = distorter.transform.position;
            if (difference.magnitude <= distorter.maxDistance)
            {
                Vector3 desiredVelocity = difference * (1 - (difference.magnitude / distorter.maxDistance));
                Vector3 seekForce = desiredVelocity - velocity;
            }
        }


        // Adjust position
        velocity += acceleration * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        acceleration = Vector3.zero;
	}

    void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }
}
