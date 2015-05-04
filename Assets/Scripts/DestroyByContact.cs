using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour 
{
	public GameObject explosion;
	public GameObject playerExplosion;
	[Space(10)]
	public int scoreValue;
	public int bombValue;
	public int foodValue;
	public int shieldValue;
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
	}

	public int GetScoreValue()
	{
		return scoreValue;
	}

	void OnTriggerEnter(Collider other)
	{
		//Debug.Log("collider other: " + other.name);
	if(other.tag == "Boundary")
		{
			return; // Ignore Boundary Collider on Enter
		}
	else if(gameObject.tag == "FoodSphere")
		{
			if(other.tag == "Player")
			{
				Destroy(gameObject);
			}
		}
	else if(gameObject.tag == "Bomb")
		{
		if(other.tag == "Player")
			{
				gameController.AddBombs(bombValue);
				Destroy(gameObject);
			}
		}
	else if(gameObject.tag == "Shield")
		{
		if(other.tag == "Player")
			{
			if(gameController.GetShieldState() == false)
				{
					gameController.ChangeShieldState();
					gameController.shieldSpawned = false;
					Destroy(gameObject);
				}
			}
		}
	else if(gameObject.tag == "Asteroid")// Collider must be Asteroid
		{
        bool playerDead = false;
		if(other.gameObject.layer == 8 || other.tag == "Asteroid")
			{
				return; 
			}
		Instantiate(explosion, transform.position, transform.rotation); //as GameObject;
		if(other.tag == "Player")
			{	
				if(!gameController.GetShieldState())
					{
						Instantiate(playerExplosion, other.transform.position, other.transform.rotation); //as GameObject;
                        playerDead = true;
						Destroy(other.gameObject);	
					}
				else // Shield exists, so let change the state of the shield
				{
					gameController.ChangeShieldState();
				}
			}
		if(other.tag == "Bolt")
			{
				gameController.incrementAmmoCount(2);
				Destroy(other.gameObject);
			}
		gameController.AddScore(scoreValue);	// Score always incremented
        if (playerDead) gameController.GameOver();
        Destroy(gameObject);					// Gameobject (Asteroid) always destroyed on collision with Player
		}
	}	
}
