using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour
{
    Transform[] backgrounds = new Transform[4];
    Transform[] starLayer1 = new Transform[4];
    Transform[] starLayer2 = new Transform[4];

	void Start ()
    {
        backgrounds[0] = transform.Find("Background1"); // A
        backgrounds[1] = transform.Find("Background2"); // B
        backgrounds[2] = transform.Find("Background3"); // C
        backgrounds[3] = transform.Find("Background4"); // D
        starLayer1[0] = transform.Find("Stars1"); // A
        starLayer1[1] = transform.Find("Stars2"); // B
        starLayer1[2] = transform.Find("Stars3"); // C
        starLayer1[3] = transform.Find("Stars4"); // D
        starLayer2[0] = transform.Find("StarsCloser1"); // A
        starLayer2[1] = transform.Find("StarsCloser2"); // B
        starLayer2[2] = transform.Find("StarsCloser3"); // C
        starLayer2[3] = transform.Find("StarsCloser4"); // D
	}

    void Update()
    {
        MainBackgroundLoop(backgrounds);
        MainBackgroundLoop(starLayer1);
        MainBackgroundLoop(starLayer2);
    }

    void MainBackgroundLoop(Transform[] bgs)
    {
        Vector3 PLAYER = GameManager.playerShip.transform.position;
        Vector3 A = bgs[0].position;
        Vector3 B = bgs[1].position;
        Vector3 C = bgs[2].position;
        Vector3 D = bgs[3].position;

        Vector2 bgSize = new Vector2(
            bgs[0].GetComponent<SpriteRenderer>().sprite.bounds.extents.x * bgs[0].localScale.x * 2,
            bgs[0].GetComponent<SpriteRenderer>().sprite.bounds.extents.y * bgs[0].localScale.y * 2
        );

        if (PLAYER.y > A.y)
        {
            // Move C & D above A & B
            bgs[2].position = new Vector3(C.x, A.y + bgSize.y, C.z);
            bgs[3].position = new Vector3(D.x, A.y + bgSize.y, D.z);
        }
        else
        {
            // Move C & D below A & B
            bgs[2].position = new Vector3(C.x, A.y - bgSize.y, C.z);
            bgs[3].position = new Vector3(D.x, A.y - bgSize.y, D.z);
        }

        A = bgs[0].position;
        B = bgs[1].position;
        C = bgs[2].position;
        D = bgs[3].position;

        if (PLAYER.y > C.y)
        {
            // Move A & B above C & D
            bgs[0].position = new Vector3(A.x, C.y + bgSize.y, A.z);
            bgs[1].position = new Vector3(B.x, C.y + bgSize.y, B.z);
        }
        else
        {
            // Move A & B below C & D
            bgs[0].position = new Vector3(A.x, C.y - bgSize.y, A.z);
            bgs[1].position = new Vector3(B.x, C.y - bgSize.y, B.z);
        }

        A = bgs[0].position;
        B = bgs[1].position;
        C = bgs[2].position;
        D = bgs[3].position;

        if (PLAYER.x > A.x)
        {
            // Move B & D to the right of A & C
            bgs[1].position = new Vector3(A.x + bgSize.x, B.y, B.z);
            bgs[3].position = new Vector3(A.x + bgSize.x, D.y, D.z);
        }
        else
        {
            // Move B & D to the left of A & C
            bgs[1].position = new Vector3(A.x - bgSize.x, B.y, B.z);
            bgs[3].position = new Vector3(A.x - bgSize.x, D.y, D.z);
        }

        A = bgs[0].position;
        B = bgs[1].position;
        C = bgs[2].position;
        D = bgs[3].position;

        if (PLAYER.x > B.x)
        {
            // Move A & C to the right of B & D
            bgs[0].position = new Vector3(B.x + bgSize.x, A.y, A.z);
            bgs[2].position = new Vector3(B.x + bgSize.x, C.y, C.z);
        }
        else
        {
            // Move A & C to the left of B & D
            bgs[0].position = new Vector3(B.x - bgSize.x, A.y, A.z);
            bgs[2].position = new Vector3(B.x - bgSize.x, C.y, C.z);
        }
    }
}
