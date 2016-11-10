using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour
{
	Transform[] backgrounds = new Transform[4];

	void Start ()
	{
		backgrounds[0] = transform.Find("Background1");
		backgrounds[1] = transform.Find("Background2");
		backgrounds[2] = transform.Find("Background3");
		backgrounds[3] = transform.Find("Background4");
	}

	void Update ()
	{
		
	}
}
