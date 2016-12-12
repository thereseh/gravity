using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	static public float health = 100f;
	bool dead = false;

	void Update()
	{
		// Start smoking
		ParticleSystem smoking = transform.GetChild(0).Find("SmokingParticleSystem").GetComponent<ParticleSystem>();
		if (health <= 50 && !smoking.isPlaying)
			smoking.Play();
	
		// No more health
		if (health <= 0 && !dead)
			DestroyShip();
		
		//Out of fuel
		if(GameManager.fuel <= 0f && !dead)
		{
			//Invoke("DestroyShip", 4);
			DestroyShip();
		}
	}

	static public void TakeDamage(float amount)
	{
		health -= amount;
		GameObject.Find("HealthForegroundMask").GetComponent<Image>().fillAmount = health / 100f;
	}

	void DestroyShip()
	{
		dead = true;
		health = 0f;
		//Camera.main.gameObject.GetComponent<UnityStandardAssets.ImageEffects.Grayscale>().enabled = true;
		//Camera.main.gameObject.GetComponent<UnityStandardAssets.ImageEffects.MotionBlur>().enabled = true;
		GameManager.SlowMo = true;
		Invoke("Respawn", 3);
	}

	void Respawn()
	{
		dead = false;
		health = 100f;
		GameManager.fuel = 200f;
		//Camera.main.gameObject.GetComponent<UnityStandardAssets.ImageEffects.Grayscale>().enabled = false;
		//Camera.main.gameObject.GetComponent<UnityStandardAssets.ImageEffects.MotionBlur>().enabled = false;
		transform.position = GameManager.SpawnPoint;
		GetComponent<ShipMovement>().acceleration = Vector3.zero;
        GetComponent<Rigidbody>().velocity = GetComponent<ShipMovement>().initialVelocity;
		transform.GetChild(0).Find("SmokingParticleSystem").GetComponent<ParticleSystem>().Stop();
		TakeDamage(0f);
		transform.GetChild(0).rotation = Quaternion.Euler(new Vector3(0, 180, 0));
		GameManager.SlowMo = false;
	}
}
