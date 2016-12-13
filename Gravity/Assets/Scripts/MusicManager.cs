using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {
	
	private static MusicManager instance = null;
	
	public static MusicManager Instance {
		get {return instance;}
	}
	
	void Awake()
	{
		if (instance != null && instance != this)
		{
			GetComponent<AudioSource>().Stop();
			if(instance.GetComponent<AudioSource>().clip != GetComponent<AudioSource>().clip)
			{
				instance.GetComponent<AudioSource>().clip = GetComponent<AudioSource>().clip;
				instance.GetComponent<AudioSource>().volume = GetComponent<AudioSource>().volume;
				instance.GetComponent<AudioSource>().Play();
			}
			
			Destroy(this.gameObject);
			return;
		}
		
		instance = this;
		GetComponent<AudioSource>().Play();
		DontDestroyOnLoad(this.gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
}
