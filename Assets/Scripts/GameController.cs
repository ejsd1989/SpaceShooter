using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	#region Declarations

	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCountMin;
	public int hazardCountMax;
	[Space(10)]
//	public GameObject food;
//	public Vector3 foodSpawnValues;
//	public int simFoodCount;
//	[HideInInspector]
//	public bool foodSpawned;
	[Space(10)]
	public GameObject bomb;
	public Vector3 bombSpawnValues;
	public int simBombCount;
	[HideInInspector]
	public bool bombSpawned;
	[Space(10)]
	public GameObject shield;
	public Vector3 shieldSpawnValues;
	[HideInInspector]
	public bool shieldSpawned;
	[Space(10)]
	public int ammoCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public float minZ;
	public float maxZ;
	public GameObject explosion;
	[Space(10)]
    
	public GUIText ammoText;
    public GUIText scoreText; 
    public GUIText hiScoreText;
//	public GUIText foodText;
	public GUIText bombText;
	public GUIText shieldText;
	public GUIText restartText;
	public GUIText gameOverText;
	[Space(10)]
	private bool gameOver;
	private bool restart;
	private int score;
    private int hiScore;
//	private int foodCount;
	private int bombCount;
	private bool boolShieldState;
	[Space(10)]
	private int asteroidScoreValue;

	#endregion

	#region Start() Function

	void Start()
	{
		gameOver = false;
		restart = false;
//		foodSpawned = false;
		boolShieldState = false;
		restartText.text = "";	// Effectively turned off at start
		gameOverText.text = "";
        score = 0;
        if (PlayerPrefs.HasKey("Player Score")) {
            hiScore = PlayerPrefs.GetInt("Player Score");
            hiScoreText.text = "High Score: " + hiScore;
        }
        else
            hiScore = 0;
        bombCount = 0;
		asteroidScoreValue = 10;

		UpdateScore();
		UpdateBombs();
		UpdateShields();
		UpdateAmmo();
		StartCoroutine (SpawnWaves());
	}

	#endregion

	#region Update Loop

	void Update() 
	{
		if(restart)
		{
			if(Input.GetKeyDown(KeyCode.R))
			{
				Application.LoadLevel(Application.loadedLevel);
			}
		}
//		if(!foodSpawned)
//		{
//			foodSpawned = true;
//			FoodSpawns();
//		}
		if(!bombSpawned)
		{
			bombSpawned = true;
			BombSpawns();
		}
		if(!shieldSpawned)
		{
			shieldSpawned = true;
			int spawnRandom = Random.Range(0,100);
			if(spawnRandom > 80) // %20 chance to spawn
			{
				ShieldSpawns();
			}
			else
			{
				StartCoroutine(WaitForSecs(10)); // will cause a wait for a random time in seconds between 0 and 10
			}
		}
//		else
//		{
//			if(shieldObjTime)
//		}
	}

	#endregion

	#region Ammo Management

	public void UpdateAmmo()
	{
		if(ammoCount == 0)
		{
			ammoText.text = "Out of Ammo";
		}
		else if(ammoCount >= 20)
		{
			ammoCount = 20;
            ammoText.text = "Ammo: 20";
		}
		else
			ammoText.text = "Ammo: " + ammoCount;
	}

	public void incrementAmmoCount(int i)
	{
		ammoCount += i;
		UpdateAmmo();
	}

    public float getAmmoCount()
    {
        return (float)ammoCount;
    }
	#endregion

	#region Instantiation of Waves

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait); // Calling a coroutine
		while (true)
		{
			int hazardCount = Random.Range(hazardCountMin, hazardCountMax);
			for(int i = 0; i < hazardCount; i++)
			{
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
                float randomSize = Random.Range(0.6f, 1);
                GameObject goMeteor = hazard;
                goMeteor.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
                Instantiate(goMeteor, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait); // Calling a coroutine
			}
			yield return new WaitForSeconds (waveWait);

			if(gameOver)
			{
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}

	#endregion

	#region Food Instantiation & Management

//	void FoodSpawns()
//	{
//		Vector3 spawnPosition = new Vector3 (Random.Range (-foodSpawnValues.x, foodSpawnValues.x), 
//		                                     foodSpawnValues.y, 
//		                                     Random.Range(minZ, maxZ));
//		Quaternion spawnRotation = Quaternion.identity;
//		Instantiate (food, spawnPosition, spawnRotation);
//	}
//
//	public void AddFood (int foodValue)
//	{
//		foodCount += foodValue;
//		UpdateFood();
//	}
//	
//	void UpdateFood()
//	{
////		bombText.text = "Bombs: " + bombCount;
//	}
	
	#endregion

	#region Bomb Instantiation & Management
	
	void BombSpawns()
	{
		Vector3 spawnPosition = new Vector3 (Random.Range (-bombSpawnValues.x, bombSpawnValues.x), 
		                                     bombSpawnValues.y, 
		                                     Random.Range(minZ, maxZ));
		Quaternion spawnRotation = Quaternion.identity;
		Instantiate (bomb, spawnPosition, spawnRotation);
	}

	public void AddBombs (int bombValue)
	{
		bombCount += bombValue;
		UpdateBombs();
	}
	
	void UpdateBombs()
	{
		bombText.text = "Bombs: " + bombCount;
	}

	public int GetBombCount()
	{
		return bombCount;
	}

	public void IncrementBombCount(int i)
	{
		bombCount += i;
		UpdateBombs();
	}
	#endregion

	#region Shield Instantiation & Management
	
	void ShieldSpawns()
	{
		Vector3 spawnPosition = new Vector3 (Random.Range (-shieldSpawnValues.x, shieldSpawnValues.x), 
		                                     shieldSpawnValues.y, 
		                                     Random.Range(minZ, maxZ));
		Quaternion spawnRotation = Quaternion.identity;
		Instantiate (shield, spawnPosition, spawnRotation);
	}
	
	public void ChangeShieldState ()
	{
		boolShieldState = !boolShieldState;
		UpdateShields();
	}
	
	void UpdateShields()
	{
		if(boolShieldState)
		{
			shieldText.text = "Shields Enabled";
		}
		else
			shieldText.text = "Shields Disabled";
	}

	public bool GetShieldState()
	{
		return boolShieldState;
	}
	
	#endregion

	#region Score Management

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore();
	}
	
	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
        if (score >= hiScore)
            hiScoreText.text = scoreText.text;
	}

	#endregion

	public void GameOver()
    {
        if(score > hiScore)
            PlayerPrefs.SetInt("Player Score", score);
		gameOverText.text = "Game Over";
		gameOver = true;
	}

	public void DestroyGameObjectWithExplosion(GameObject go)
	{
		if(go.tag == "Asteroid")
		{
		incrementAmmoCount(2);
		Instantiate(explosion, go.transform.position, go.transform.rotation); //as GameObject;
		AddScore(asteroidScoreValue);	// Score always incremented
		Destroy(go);					// Gameobject (Asteroid) always destroyed on collision with Player
		}
	}

	IEnumerator WaitForSecs(int i)
	{
		yield return new WaitForSeconds(Random.Range(0, i));
		shieldSpawned = false;
	}
}
