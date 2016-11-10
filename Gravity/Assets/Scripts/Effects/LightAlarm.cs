using UnityEngine;
using System.Collections;

public class LightAlarm : MonoBehaviour
{
	public float maxIntensity;
	public float offDamping;
	public float switchDelay;
	float lastSwitch;
	bool enabled = true;

	void Start()
	{
		lastSwitch = Time.time;
	}

	void Update()
	{
		if (Time.time - lastSwitch >= switchDelay)
		{
			lastSwitch = Time.time;
			enabled = !enabled;
		}

		if (enabled)
		{
			GetComponent<Light>().intensity += (maxIntensity - GetComponent<Light>().intensity) * 0.1f;
		}
		else
		{
			GetComponent<Light>().intensity *= offDamping;
		}
	}
}
