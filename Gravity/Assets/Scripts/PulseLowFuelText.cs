using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PulseLowFuelText : MonoBehaviour
{
    Text txt;

    float sineStep = 0f;
    public float sineSpeed = 0.01f;

	void Start ()
    {
        txt = GetComponent<Text>();
	}
	
	void Update ()
    {
        if (txt.enabled)
        {
            sineStep += sineSpeed;
            float sineVal = Mathf.Sin(sineStep); //-1 to 1    / 4 = -1/4 to 1/4

            Color c = txt.color;
            txt.color = new Color(c.r, c.g, c.b, (3f / 4f) + (sineVal / 4f));
        }
	}
}
