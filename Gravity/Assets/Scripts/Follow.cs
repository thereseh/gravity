using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour
{
	public Transform followObject;
	public float damping;
	public Vector3 offset;
    public Vector3 overviewPosition;

	void Update()
	{
        if (Input.GetKey(KeyCode.Space))
        {
            GameManager.overviewCam = true;
            transform.Translate((overviewPosition - transform.position) * damping);
        }
        else
        {
            GameManager.overviewCam = false;
            transform.Translate(((offset + followObject.position) - transform.position) * damping);
        }
	}
}
