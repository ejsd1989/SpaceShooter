using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour {

    private GameObject mObj;
    private GameObject player;

	// Use this for initialization
	void Start () {
        mObj = this.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        mObj.transform.position = player.transform.position;
	}
}
