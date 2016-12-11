using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class WormholeScript : MonoBehaviour
{
    public float distToCollide = 2f;
    float enteredWormholeTime = 0f;
    Image wormholeSinkUI;
    bool showedWinText = false;
	  

    void Start()
    {
        wormholeSinkUI = GameObject.Find("Wormhole_Sink").GetComponent<Image>();
    }

    void Update ()
    {
        if (!GameManager.transportingThroughWormhole && (transform.position - GameManager.playerShip.transform.position).magnitude <= distToCollide)
            TransportThroughWormhole();

        if (GameManager.transportingThroughWormhole)
        {
            Vector3 diff = transform.position - GameManager.playerShip.transform.position;
            GameManager.playerShip.GetComponent<Rigidbody>().velocity = diff.normalized * 10 * (diff.magnitude / distToCollide);
            GameManager.playerShip.transform.localScale *= 0.992f;
            Camera.main.fieldOfView += (1 - Camera.main.fieldOfView) * 0.025f;
            Color c = wormholeSinkUI.color;
            wormholeSinkUI.color = new Color(
                c.r, c.g, c.b, c.a + 2f * Time.deltaTime
            );
            c = wormholeSinkUI.transform.Find("Wormhole_Sink_FadeOut").GetComponent<Image>().color;
            wormholeSinkUI.transform.Find("Wormhole_Sink_FadeOut").GetComponent<Image>().color = new Color(
                c.r, c.g, c.b, c.a + 0.5f * Time.deltaTime
            );

            if (Time.time - enteredWormholeTime >= 1.5f)
            {
                if (!showedWinText)
                {
                    showedWinText = true;
                    float timeSinceStart = Time.time - GameManager.timeStartedLevel;
                    int minutes = Mathf.FloorToInt(timeSinceStart / 60f);
                    int seconds = Mathf.FloorToInt(timeSinceStart - Mathf.FloorToInt(timeSinceStart / 60f) * 60);
                    wormholeSinkUI.transform.Find("Wormhole_Sink_Text").GetComponent<Text>().text = "Finished Level " + GameObject.Find("MainCamera").GetComponent<GameManager>().level + ": " + GameObject.Find("MainCamera").GetComponent<GameManager>().levelName + @"
<size=22>Time: " + minutes.ToString() + " minutes " + seconds.ToString() + " seconds</size>";
                }
                ShowWinText();
            }
            if (Time.time - enteredWormholeTime >= 5f)
                WinLevel();
        }
    }

    void TransportThroughWormhole()
    {
        GameManager.transportingThroughWormhole = true;
        GameManager.playerShip.GetComponent<ShipMovement>().acceleration = Vector3.zero;

        Camera.main.GetComponent<Follow>().followObject = transform;

        enteredWormholeTime = Time.time;
    }

    void ShowWinText()
    {
        Color c = wormholeSinkUI.transform.Find("Wormhole_Sink_Text").GetComponent<Text>().color;
        wormholeSinkUI.transform.Find("Wormhole_Sink_Text").GetComponent<Text>().color = new Color(
            c.r, c.g, c.b, c.a + 0.5f * Time.deltaTime
        );
    }

    void WinLevel()
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

        // Load next level
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0; // Go to Main Menu
        SceneManager.LoadScene(nextSceneIndex);
    }
}
