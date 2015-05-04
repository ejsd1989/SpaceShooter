using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour 
{
	public float speed;
	public float speedMin;
	public float speedMax;

void Start()
	{
		if(speedMin != 0 && speedMax != 0)
			speed = Random.Range(speedMin, speedMax);
		rigidbody.velocity = transform.forward * speed; // transform.forward relates to the Z axis
	}
}
