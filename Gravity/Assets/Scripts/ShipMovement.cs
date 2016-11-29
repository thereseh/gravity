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
        
        /* Apply black hole gravity
        foreach (MatterDistort distorter in GameManager.matterDistortions)
        {
            if (distorter == null) continue;
            Vector3 difference = distorter.transform.position;
            if (difference.magnitude <= distorter.maxDistance)
            {
                // Seek black hole
                Vector3 desiredVelocity = difference * (1 - (difference.magnitude / distorter.maxDistance));
                print(desiredVelocity);
                Vector3 seekForce = desiredVelocity - velocity;
                ApplyForce(seekForce * userInputMultiplier);
            }
        }*/

        // Adjust position
        velocity += acceleration * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        acceleration = Vector3.zero;

        // Look in the direction of movement
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

    void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }
}
