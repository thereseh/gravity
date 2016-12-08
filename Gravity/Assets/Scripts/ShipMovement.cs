using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour
{
    public Vector3 initialVelocity;
    public float mass;
    public float maxSpeed = 6f;
    [HideInInspector]
    public Vector3 acceleration = Vector3.zero;
    Transform facingBlackHole;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = initialVelocity;
    }

    void Update()
    {
        if (!GameManager.transportingThroughWormhole)
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
            GetComponent<Rigidbody>().velocity += acceleration * Time.deltaTime;
            if (GetComponent<Rigidbody>().velocity.magnitude > maxSpeed)
                GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxSpeed;
            acceleration = Vector3.zero;
        }

        // Look in the direction of movement
        Vector3 v = GetComponent<Rigidbody>().velocity;
        Vector3 normalizedDirection = v.normalized;
        if (v == Vector3.zero)
            normalizedDirection = Vector3.one;
        float rotationAngle = Vector3.Angle(Vector3.right, normalizedDirection);
        if (v.y > 0)
            rotationAngle = 180 - rotationAngle + 180;
        if (v.magnitude == 0)
            rotationAngle = 0;

        if (facingBlackHole != null)
        {
            transform.GetChild(0).LookAt(facingBlackHole.position);
            transform.GetChild(0).RotateAround(transform.GetChild(0).position, -Vector3.forward, 270);
            transform.GetChild(0).RotateAround(transform.GetChild(0).position, Vector3.right, 90);
        }
        else
        {
            transform.GetChild(0).LookAt(transform.GetChild(0).position + Vector3.up);
            transform.GetChild(0).RotateAround(transform.GetChild(0).position, -Vector3.forward, rotationAngle);
        }
    }

    void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    void OnCollisionEnter(Collision col)
    {
        if (!GameManager.transportingThroughWormhole && col.gameObject.tag != "PlaceHolder" && col.gameObject.tag != "Blackhole")
        {
            print("Collided with " + col.gameObject.name);
            PlayerHealth.TakeDamage(25f);
        }
        else if (col.gameObject.tag == "Blackhole")
        {
            facingBlackHole = col.transform;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Blackhole")
        {
            facingBlackHole = null;
        }
    }
}
