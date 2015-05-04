using UnityEngine;
using System.Collections;


/*
 * 	Script to ensure that the shot spawn does not tilt up/down due to tilt of player
 */
public class ShotSpawnFixedPosition : MonoBehaviour {
	
	private GameObject go;

	void Start()
	{
		go = gameObject;
	}

	void Update()
	{
		go.transform.localPosition = new Vector3(0,
		                                         0,
		                                         go.transform.localPosition.z);
	}
}
