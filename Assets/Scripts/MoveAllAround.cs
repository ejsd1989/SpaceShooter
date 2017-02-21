using UnityEngine;
using System.Collections;

public class MoveAllAround : MonoBehaviour {

	public float minX, maxX, minZ, maxZ;
	[Space(10)]
	public float speed;
	public float speedMin;
	public float speedMax;

    private Vector3 goPos;
	
	void Start()
	{
		if(speedMin != 0 && speedMax != 0)
			speed = Random.Range(speedMin, speedMax);
        Vector3 direction;
        if (Random.Range(0, 1) == 1)
        {
            direction = transform.right;
        }
        else
            direction = new Vector3 (-1, 0, 0);
        GetComponent<Rigidbody>().velocity = (transform.forward + direction) * speed; // transform.forward relates to the Z axis
//		rigidbody.velocity = transform.right * speed;
	}

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Boundary")
        {
    //        rigidbody.velocity = rigidbody.velocity * (-1);
//			rigidbody.velocity = transform.right * (-1)*speed;
        }
    }

    void FixedUpdate()
    {
        goPos = gameObject.transform.position;
        if (goPos.x <= minX || goPos.x >= maxX)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(-GetComponent<Rigidbody>().velocity.x, 0.0f, GetComponent<Rigidbody>().velocity.z);
        } 
        if (goPos.z <= minZ || goPos.z >= maxZ)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 0.0f, -GetComponent<Rigidbody>().velocity.z);
        }
    }

//	void RandomMovementDirection()
//	{
//		rigidbody.velocity = R
//	}
}
