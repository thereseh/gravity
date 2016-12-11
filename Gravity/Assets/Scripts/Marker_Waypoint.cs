using UnityEngine;
using System.Collections;

public class Marker_Waypoint : MonoBehaviour {
	
	public float distFromShip = 3f;
	public GameObject wormhole;
	public GameObject ship;
	public Vector3 shipToWorm;
	public SpriteRenderer sprite;
	
	
	
	

	// Use this for initialization
	void Start () {
		wormhole = GameObject.FindWithTag("Wormhole");
		ship = GameObject.Find("PlayerShip");
		sprite = gameObject.GetComponent<SpriteRenderer>();
		//shipToWorm = wormhole.transform.position - ship.transform.position;
		shipToWorm = wormhole.transform.position - ship.transform.position;
		
		shipToWorm.Normalize();
		
		shipToWorm *= distFromShip;
		
		transform.position = ship.transform.position + shipToWorm;
		
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		float fade = 1;
		
		shipToWorm = wormhole.transform.position - ship.transform.position;
		
		if(shipToWorm.magnitude < 20)
		{
			  fade = shipToWorm.magnitude / 25;
		} else if(shipToWorm.magnitude < 3)
		{
			  fade = 0f;
		}
		
		shipToWorm.Normalize();
		
		shipToWorm *= distFromShip;
		
		transform.position = ship.transform.position + shipToWorm;
		
		float angle = Mathf.Atan2(shipToWorm.y, shipToWorm.x) * Mathf.Rad2Deg;
 		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
 		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 5f);
		
		sprite.color = new Color(1f,1f,1f, fade);
	
		
		
	
	}
}
