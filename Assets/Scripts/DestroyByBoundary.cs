/*
 *  DestroyByBoundary.cs
 *  Constantly running in order to destroy all objects off player screen
 */

using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {
	void OnTriggerExit (Collider other)
	{
		if(other.gameObject.layer != 8)
		{
			Destroy(other.gameObject);
		}
	}
}
