using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMover : MonoBehaviour {

    public int playerMovementSpeed = 5;
    public float speed;
    public float tilt;
    public Boundary boundary;

    GameObject camObj;
    GameObject mObj;
    Rigidbody mRb;

    float bound_zmin, bound_zmax;

    CameraMover camMover;

    // Use this for initialization
    void Start()
    {
        mObj = this.gameObject;
        mRb = mObj.GetComponent<Rigidbody>();
        camObj = Camera.main.gameObject;
        camMover = camObj.GetComponent<CameraMover>();
        bound_zmin = boundary.Zmin;
        bound_zmax = boundary.Zmax;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        mObj.transform.Translate(Vector3.forward * playerMovementSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        float zmin = bound_zmin + camObj.transform.position.z;
        float zmax = bound_zmax + camObj.transform.position.z;

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        mRb.velocity = movement * speed;
        mRb.position = new Vector3
        (
            Mathf.Clamp(mRb.position.x, boundary.Xmin, boundary.Xmax),
             0.0f,
            Mathf.Clamp(mRb.position.z, zmin, zmax)
        );

        // for tilting in all 4 directions
        // rigidbody.rotation = Quaternion.Euler(rigidbody.velocity.z * tilt, 0.0f, rigidbody.velocity.x * -tilt);
        mRb.rotation = Quaternion.Euler(0.0f, 0.0f, mRb.velocity.x * -tilt);
    }
}
