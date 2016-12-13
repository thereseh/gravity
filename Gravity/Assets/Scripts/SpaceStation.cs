using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SpaceStation : MonoBehaviour
{
	public float mag;
	public float rad;

    GameObject ui_display;

    bool reachedEnd = false;

    void Start()
    {
        ui_display = GameObject.Find("UI_WinGame");
    }
	
	void Update()
    {
		transform.Rotate (0,0.3f,0);

		Vector3 diff = transform.position - GameManager.playerShip.transform.position;
		mag = diff.magnitude;
        if (mag <= rad)
        {
            GameManager.playerShip.GetComponent<Rigidbody>().velocity = diff.normalized * 5f * (mag / rad);

            // Fade in game over UI
            Color c = ui_display.GetComponent<Image>().color;
            ui_display.GetComponent<Image>().color = new Color(
                c.r, c.g, c.b, 1f - (mag / rad)
            );
            c = ui_display.transform.Find("UI_WinGame_Black").GetComponent<Image>().color;
            ui_display.transform.Find("UI_WinGame_Black").GetComponent<Image>().color = new Color(
                c.r, c.g, c.b, 1f - (mag / rad)
            );
            c = ui_display.transform.Find("UI_WinGame_Text").GetComponent<Text>().color;
            ui_display.transform.Find("UI_WinGame_Text").GetComponent<Text>().color = new Color(
                c.r, c.g, c.b, 1f - (mag / rad)
            );
            c = ui_display.transform.Find("UI_WinGame_Menu").GetComponent<Image>().color;
            ui_display.transform.Find("UI_WinGame_Menu").GetComponent<Image>().color = new Color(
                c.r, c.g, c.b, 1f / 255f
            );
            ui_display.transform.Find("UI_WinGame_Menu").GetComponent<Button>().interactable = true;
            c = ui_display.transform.Find("UI_WinGame_Menu").GetChild(0).GetComponent<Text>().color;
            ui_display.transform.Find("UI_WinGame_Menu").GetChild(0).GetComponent<Text>().color = new Color(
                c.r, c.g, c.b, 1f - (mag / rad)
            );
            
            if (!reachedEnd)
            {
                reachedEnd = true;

                // Remove black holes
                List<GameObject> bhBu = new List<GameObject>();
                foreach (GameObject blackHole in GameManager.blackHoles)
                    bhBu.Add(blackHole);
                foreach (GameObject bh in bhBu)
                {
                    try
                    {
                        bh.SetActive(false);
                        Destroy(bh);
                        GameManager.blackHoles.Remove(bh);
                    }
                    catch { }
                }
            }
        }
		//if (mag <= 6f)
        //    GoToMainMenu();
	}

	public void GoToMainMenu()
	{
		// Reset variables
		GameManager.SlowMo = false;
		PlayerHealth.health = 100f;
		PlayerHealth.TakeDamage(0f);
		GameManager.fuel = 200f;
		
		// Remove black holes
		List<GameObject> bhBu = new List<GameObject>();
		foreach (GameObject blackHole in GameManager.blackHoles)
			bhBu.Add(blackHole);
		foreach (GameObject bh in bhBu)
			GameManager.blackHoles.Remove(bh);

		SceneManager.LoadScene(0);
	}
}
