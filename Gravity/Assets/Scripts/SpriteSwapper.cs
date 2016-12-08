using UnityEngine;
using System.Collections;

public class SpriteSwapper : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;
    public float swapDelay;

    SpriteRenderer rend;
    float lastChangeTime;
    int currentSprite = 1;
    
	void Start ()
    {
        rend = GetComponent<SpriteRenderer>();
	}
	
	void Update ()
    {
        if (Time.time - lastChangeTime >= swapDelay)
        {
            lastChangeTime = Time.time;
            if (currentSprite == 1)
            {
                currentSprite = 2;
                rend.sprite = sprite2;
            }
            else
            {
                currentSprite = 1;
                rend.sprite = sprite1;
            }
        }
	}
}
