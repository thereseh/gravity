using UnityEngine;
using System.Collections;

public class WormholeScript : MonoBehaviour
{
    public float distToCollide = 2f;

    float enteredWormholeTime = 0f;

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

            if (Time.time - enteredWormholeTime >= 3f)
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

    void WinLevel()
    {
        print("Go to next level");
    }
}
