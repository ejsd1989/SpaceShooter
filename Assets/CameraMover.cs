using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {

    public int cameraMovementSpeed = 5;

    GameObject mObj;

	// Use this for initialization
	void Start () {
        mObj = this.gameObject;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void LateUpdate()
    {
        mObj.transform.Translate(Vector3.up * cameraMovementSpeed * Time.deltaTime);
    }
}
