using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float Xmin, Xmax, Zmin, Zmax;
}

public class PlayerController : MonoBehaviour 
{
	public float speed;
	public float tilt;
	public Boundary boundary;
	[Space(10)]
	public GameObject shot;
	public Transform shotSpawn;
	private Quaternion shotSpawnRotation;
	public float fireRate = 0.5f;
	[Space(10)]
	private float nextFire;
	[Space(10)]
	private GameController gameController;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null)
		{
			Debug.Log("Cannot find 'GameController' script");
		}

		shotSpawnRotation = shotSpawn.rotation;
	}

	void Update()
	{
		if(Input.GetButton("Fire1") && Time.time > nextFire && gameController.ammoCount > 0)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawnRotation); //as GameObject;
			audio.Play ();
			gameController.incrementAmmoCount(-1);
		}
		if(Input.GetButton("Fire2") && gameController.GetBombCount() > 0)
		{
			gameController.IncrementBombCount(-1);
			DestroyAll();
			// Fire bomb
			// Destroy all enemy & obstacle gameobjects
		}
	}

	void DestroyAll()
	{
//		Debug.Log("TESTING1");
		GameObject[] layerObjects = FindObjectsOfType<GameObject>();
		foreach (GameObject go in layerObjects)
		{
			if(go.layer == 9 || go.layer == 10) // 9 - Obstacles, 10 - Enemies
			{
				gameController.DestroyGameObjectWithExplosion(go);
			}
		}
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rigidbody.velocity = movement * speed;

		rigidbody.position = new Vector3
		(
			Mathf.Clamp(rigidbody.position.x, boundary.Xmin, boundary.Xmax),
         	0.0f,
			Mathf.Clamp(rigidbody.position.z, boundary.Zmin, boundary.Zmax)
		);
	
		// for tilting in all 4 directions
		// rigidbody.rotation = Quaternion.Euler(rigidbody.velocity.z * tilt, 0.0f, rigidbody.velocity.x * -tilt);
		rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt);
	}

}
