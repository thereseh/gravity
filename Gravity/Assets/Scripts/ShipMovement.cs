using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour
{
    public Vector3 initialVelocity;
    public float mass;
    Vector3 acceleration = Vector3.zero;
    Vector3 velocity = Vector3.zero;
    //CharacterController controller;

	void Start()
    {
        //controller = GetComponent<CharacterController>();
        GetComponent<Rigidbody>().velocity = initialVelocity;
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

        // Adjust position
        //velocity += acceleration * Time.deltaTime;
        //controller.Move(velocity * Time.deltaTime);
        GetComponent<Rigidbody>().velocity += acceleration * Time.deltaTime;
        acceleration = Vector3.zero;

        // Look in the direction of movement
        //Vector3 normalizedDirection = velocity.normalized;
        Vector3 normalizedDirection = GetComponent<Rigidbody>().velocity.normalized;
        if (GetComponent<Rigidbody>().velocity == Vector3.zero)
            normalizedDirection = Vector3.one;
        float rotationAngle = Vector3.Angle(Vector3.right, normalizedDirection);
        if (GetComponent<Rigidbody>().velocity.y > 0)
            rotationAngle = 180 - rotationAngle + 180;
        if (GetComponent<Rigidbody>().velocity.magnitude == 0)
            rotationAngle = 0;

        transform.GetChild(0).LookAt(transform.GetChild(0).position - Vector3.forward);
        transform.GetChild(0).RotateAround(transform.GetChild(0).position, -Vector3.forward, rotationAngle);
    }

    void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }
}
