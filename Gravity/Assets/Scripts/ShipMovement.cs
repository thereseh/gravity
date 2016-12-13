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
		
		public AudioSource thrustSound;
		public AudioSource spurtSound;
		public AudioSource crashSound;
		public AudioSource alarmSound;
		 

    void Start()
    {
        GetComponent<Rigidbody>().velocity = initialVelocity;
				//thrustSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!GameManager.transportingThroughWormhole)
        {
					if((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) && GameManager.fuel > 0)
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
						
						GameManager.fuel = GameManager.fuel - Time.deltaTime * 100; // change this vaule to raise cost of using arrow keys
						
						if(!thrustSound.isPlaying && GameManager.fuel > 100f)
						{
							thrustSound.Play();
						} 
						
						if(GameManager.fuel <= 100f)
							 {
								if(!spurtSound.isPlaying)
								{
								 	thrustSound.Stop();
								 	spurtSound.Play();
								}
								if(!alarmSound.isPlaying)
								{
									alarmSound.Play();
								}
							 }

						
						
						if(GameManager.fuel < 0)
						{
							GameManager.fuel = 0;
						}
					} else 
					{
						thrustSound.Stop();
						spurtSound.Stop();
					}
					
					/*if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
					{
						thrustSound.Stop();
					}  */

            // Adjust position
            GetComponent<Rigidbody>().velocity += acceleration * Time.deltaTime;
            if (GetComponent<Rigidbody>().velocity.magnitude > maxSpeed)
                GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxSpeed;
            acceleration = Vector3.zero;
        } else
				{
					thrustSound.Stop();
					spurtSound.Stop();
					alarmSound.Stop();
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
						crashSound.Play();
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
