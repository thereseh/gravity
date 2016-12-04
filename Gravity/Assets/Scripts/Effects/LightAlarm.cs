using UnityEngine;
using System.Collections;

public class LightAlarm : MonoBehaviour
{
	public float maxIntensity;
	public float offDamping;
	public float switchDelay;
    public bool lightOn = true;
    public float initialDelay = 0f;
    float lastSwitch;

	void Start()
	{
		lastSwitch = Time.time - initialDelay;
	}

	void Update()
	{
		if (Time.time - lastSwitch >= switchDelay)
		{
			lastSwitch = Time.time;
            lightOn = !lightOn;
		}

		if (lightOn)
		{
			GetComponent<Light>().intensity += (maxIntensity - GetComponent<Light>().intensity) * 0.1f;
		}
		else
		{
			GetComponent<Light>().intensity *= offDamping;
		}
	}
}
