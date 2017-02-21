using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {

	public float tumble;

	void Start()
	{
		// Static Vector3 insideUnitSphere; - Returns a random point inside a sphere with radius 1; 
		GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
	}
}
