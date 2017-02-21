using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

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
    public bool startGame = false;
	[Space(10)]
	private float nextFire;
	[Space(10)]
	private GameController gameController;
    private bool fireGun;
    private bool fireBomb;
    private bool autoFire = false;

	void Start()
	{
        CheckEnableAutoFire();
        fireGun = fireBomb = false;

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
        // fire weapons on multiple touches
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.position.y > Screen.height / 2 || touch.position.x > Screen.width/2 && Time.time > nextFire)
            {
                fireGun = true;
            }
        }
        else if (Input.GetButton("Fire1") && Time.time > nextFire) fireGun = true;
        if (Input.GetButton("Fire2") && Time.time > nextFire) fireBomb = true;

        //if (fireGun && Time.time > nextFire && gameController.ammoCount > 0)
        //{
        //    fireGun = false;
        //    nextFire = Time.time + fireRate;
        //    FireBolt();
        //}
        if (Time.time > nextFire && fireGun)// && gameController.ammoCount > 0)
        {
            fireGun = false;
            nextFire = Time.time + fireRate;
            FireBolt();
        }
        if (Time.time > nextFire && fireBomb)// && gameController.ammoCount > 0)
        {
            fireGun = false;
            nextFire = Time.time + fireRate;
            FireBolt();
        }
        //if (startGame && Time.time > nextFire)// && gameController.ammoCount > 0)
        //{
        //    fireGun = false;
        //    nextFire = Time.time + fireRate;
        //    FireBolt();
        //}
        //if (startGame && fireBomb)
        //{
        //    FireBomb();
        //}
    }

    void CheckEnableAutoFire()
    {
#if MOBILE_INPUT
        autoFire = true;
#else
        autoFire = false;
#endif

    }

    void FireBolt()
    {
        Instantiate(shot, shotSpawn.position, shotSpawnRotation); //as GameObject;
        GetComponent<AudioSource>().Play();
        gameController.incrementAmmoCount(-1);
    }

    public void FireBomb()
    {
        //new stuff
        int bombs = gameController.GetBombCount();

        if (bombs > 0)
        {
            gameController.IncrementBombCount(-1);
            DestroyAll();
        }
        fireBomb = false;
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
}
