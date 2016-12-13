using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartGame : MonoBehaviour
{
		public AudioSource click;
    public void StartTheGame()
    {
			
			click.Play();
				
      SceneManager.LoadScene("level_1");
				
    }
}
