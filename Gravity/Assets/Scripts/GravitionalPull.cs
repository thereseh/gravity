using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GravitionalPull : MonoBehaviour
{
    public bool active = false;
    
    GameManager gameManager;
    public float grav_const = 0.08f;
    public ShipMovement ship;

    Rigidbody self;

    void Start()
    {
        self = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("MainCamera").GetComponent<GameManager>();
        ship = GameObject.Find("PlayerShip").GetComponent<ShipMovement>();

    }

    void Update()
    {
        if (!active || GameManager.transportingThroughWormhole)
            return;

        transform.localScale -= Vector3.one * 0.025f;
        self.mass -= 0.5f;
        if (transform.localScale.x < 0.1f)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            GetComponent<DeletThis>().clearListOfBlackHoles();
        }
    }

    void FixedUpdate()
    {
        if (!active || GameManager.transportingThroughWormhole)
            return;
        
        Vector3 distance = gameObject.transform.position - GameManager.playerShip.transform.position;
        if (distance.magnitude > 0.5f)
        {
            float rad = gameObject.GetComponent<SphereCollider>().radius + gameObject.transform.localScale.x;

            float force = (grav_const * self.mass * 1) / distance.sqrMagnitude;
            GameManager.playerShip.GetComponent<Rigidbody>().AddForce(distance.normalized * force, ForceMode.Impulse);
        }

		for (int i = 0; i < gameManager.asteroids.Count; i++) {
            try
            {
                Vector3 dist = gameObject.transform.position - gameManager.asteroids[i].transform.position;

                if (dist.magnitude > 0.5f)
                {
                    float force2 = (grav_const * self.mass * 2f) / dist.sqrMagnitude;
                    gameManager.asteroids[i].GetComponent<Rigidbody>().AddForce(dist.normalized * (force2 / 10f), ForceMode.Impulse);
                }
            }
            catch { }
		}
	}
	
	void OnCollisionEnter(Collision obj)
    {
        /*if (obj.gameObject.tag == "Blackhole" && gameObject.tag != "PlaceHolder") {
			float masses = self.mass + obj.gameObject.GetComponent<Rigidbody> ().mass;
			Vector3 rad1 = gameObject.transform.localScale;
			Vector3 rad2 = obj.transform.localScale;
			Vector3 sumRadius = rad1 + rad2;
			Vector3 pos = Vector3.zero;
			if (rad1.sqrMagnitude > rad2.sqrMagnitude)
			{
				pos = gameObject.transform.position;
				gameManager.BlackHoleCollision (masses, sumRadius, pos);

			}

			Destroy(obj.gameObject);
			Destroy(gameObject);
		} else {*/
        self.isKinematic = true;
    }

    void OnCollisionExit()
    {
        self.isKinematic = false;
    }
}
