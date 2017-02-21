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
    AudioSource audioSource;

    public AudioClip bombAudioClip;
    public AudioClip shieldAudioClip;


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
                    GetComponent<AudioSource>().PlayOneShot(bombAudioClip);
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
                        GetComponent<AudioSource>().PlayOneShot(shieldAudioClip);
                        gameController.ChangeShieldState();
					    gameController.shieldSpawned = false;
					    Destroy(gameObject);
				    }
			    }
		    }
	    else if(gameObject.tag == "Asteroid")// Collider must be Asteroid
		    {
            bool playerDead = false;
            bool destroyObject = false;
		    if(other.gameObject.layer == 8 || other.gameObject.layer == 10 || other.tag == "Asteroid")
			    {
                    // ignore the collision
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
                    destroyObject = true;
			    }
		    if(other.tag == "Bolt")
			    {
				    gameController.incrementAmmoCount(2);
				    Destroy(other.gameObject);
                    destroyObject = true;
                }
		    gameController.AddScore(scoreValue);	// Score always incremented
            if (playerDead) gameController.GameOver();
            if (destroyObject) Destroy(gameObject);					// Gameobject (Asteroid) always destroyed on collision with Player
		    }
        else if (gameObject.tag == "Enemy")
        {
            bool playerDead = false;
            bool destroyObject = false;
            if (other.gameObject.layer == 10 || other.gameObject.layer == 9 || other.gameObject.layer == 8)
            {
                // ignore the collision
                return;
            }
            if (other.tag == "Player")
            {
                if (!gameController.GetShieldState())
                {
                    Instantiate(playerExplosion, other.transform.position, other.transform.rotation); //as GameObject;
                    playerDead = true;
                    Destroy(other.gameObject);
                }
                else // Shield exists, so let change the state of the shield
                {
                    gameController.ChangeShieldState();
                }
                destroyObject = true;
            }
            if (other.tag == "Bolt")
            {
                Instantiate(explosion, transform.position, transform.rotation); //as GameObject;
                gameController.AddScore(scoreValue);	// Score always incremented
                gameController.incrementAmmoCount(3);
                Destroy(other.gameObject);
                destroyObject = true;
            }
            if (playerDead) gameController.GameOver();
            if(destroyObject) Destroy(gameObject);					// Gameobject (Asteroid) always destroyed on collision with Player
        }
        else if (gameObject.tag == "EnemyBolt")
        {
            bool playerDead = false;
            bool destroyObject = false;
            if (other.tag == "Player")
            {
                if (!gameController.GetShieldState())
                {
                    Instantiate(playerExplosion, other.transform.position, other.transform.rotation); //as GameObject;
                    playerDead = true;
                    Destroy(other.gameObject);
                }
                else // Shield exists, so let change the state of the shield
                {
                    gameController.ChangeShieldState();
                }
                destroyObject = true;
            }
            if (playerDead) gameController.GameOver();
            if (destroyObject) Destroy(gameObject);					// Gameobject (Asteroid) always destroyed on collision with Player
        }
	}
}
