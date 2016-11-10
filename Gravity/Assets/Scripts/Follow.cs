using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour
{
	public Transform followObject;
	public float damping;
	public Vector3 offset;

	void Update ()
	{
		transform.Translate(((offset + followObject.position) - transform.position) * damping);
	}
}
