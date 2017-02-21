using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GameController : MonoBehaviour 
{
	#region Declarations

    [Header("Game Configuration")]
    public int startCountdown = 3;

    [Space(10)]
    int currentRound = 0;
    [Space(10)]
    public GameObject[] hazard;
	public Vector3 spawnValues;
	public int hazardCountMin;
	public int hazardCountMax;
    [Space(10)]
    public GameObject[] enemyGameObjects;
    public Vector3 enemySpawnValues;
    public int intEnemyCount = 1;
    int enemyHealth = 1;
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
    [Tooltip("Check for unlimited ammo")]
    public bool unlimitedAmmo = true; 
    public float spawnWait;
    [Tooltip("Random time between 0.25 - 1 seconds")]
    public bool randomSpawnTime = false;
	public float startWait;
	public float waveWait;
	public float minZ;
	public float maxZ;
	public GameObject explosion;
	[Space(10)]

    public Text ammoText;

    public Text scoreText;
    public Text timeText;
    public Text hiScoreText;
    public Text recordTimeText;
//	public GUIText foodText;
    public Text bombText;
    public Text shieldText;
    public Text restartText;
    public Text gameOverText;
    public Text gameStartStaticText;
    public Text gameStartCountText;
	[Space(10)]
	private bool gameOver;
	private bool restart;
    private bool finishedCountDown = false;
	private int score;
    private int hiScore;
//	private int foodCount;
	private int bombCount;
	private bool boolShieldState;
    private int elapsedTime;
    private int storedTime;
	[Space(10)]
	private int asteroidScoreValue;
    private PlayerController playerController;
    private GameObject touchInput;
    public GameObject menuPanel;
    [Space(10)]
    //public bool muteAudio = false;
    public AudioClip[] audioClips;
    public Button restartButton;
    public Button quitButton;
    public Button returnButton;
    public Button submitTime;
    public Button submitScore;


	#endregion

	#region Start() Function

	void Start()
	{
//        //if (muteAudio)
//        //{
//        //    AudioSource thisASrc = GetComponent<AudioSource>();
//        //    thisASrc.volume = 0;
//        //}

//        if (Social.localUser.authenticated)
//        {
//            // unlock achievement (achievement ID "Cfjewijawiu_QA")
//            Social.ReportProgress("CgkIp4zItNoREAIQAQ", 100.0f, (bool success) =>
//            {
//                // handle success or failure
//                if (!success)
//                {
//                    Debug.Log("Achievement on Game State Failure. \n User Authentication: " + Social.localUser.authenticated);
//                }
//            });
//        }
        

//        submitScore.interactable = false;
//        submitTime.interactable = false;
//        touchInput = GameObject.Find("MobileSingleStickControl"); 
//        if(touchInput == null)
//            touchInput = GameObject.Find("DualTouchControls"); 
//        gameOver = false;
//		restart = false;
////		foodSpawned = false;
//		boolShieldState = false;
//		restartText.text = "";	// Effectively turned off at start
//		gameOverText.text = "";
//        timeText.text = "0";
//        score = 0;
//        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

//        if (randomSpawnTime)
//        {
//            spawnWait = Random.Range(0.25f, 1f);
//        }

//        if (PlayerPrefs.HasKey("Player Score"))
//        {
//            hiScore = PlayerPrefs.GetInt("Player Score");
//            hiScoreText.text = hiScore.ToString();
//        }
//        else
//            hiScore = 0;
//        if (PlayerPrefs.HasKey("Player Time"))
//        {
//            storedTime = PlayerPrefs.GetInt("Player Time");
//            recordTimeText.text = storedTime.ToString();
//        }
//        else
//            elapsedTime = 0;

//        bombCount = 0;
//		asteroidScoreValue = 10;

//		UpdateScore();
//		UpdateBombs();
//		UpdateShields();
//		UpdateAmmo();
//        StartCoroutine(StartCountdown());
	}

	#endregion

    IEnumerator StartCountdown()
    {
        int timeWait = startCountdown;
        int audioClipCount = 0;
        while (timeWait > 0)
        {
            gameStartCountText.text = timeWait.ToString();
            AudioSource.PlayClipAtPoint(audioClips[audioClipCount], Camera.main.transform.position);
            audioClipCount++;
            yield return new WaitForSeconds(1);
            timeWait--;
        }
        AudioSource.PlayClipAtPoint(audioClips[3], Camera.main.transform.position);
        gameStartCountText.text = "Go!";
        yield return new WaitForSeconds(1);
        GetComponent<AudioSource>().Play();
        gameStartCountText.gameObject.SetActive(false);
        gameStartStaticText.gameObject.SetActive(false);
        playerController.startGame = true;
        StartCoroutine(SpawnMeteors());
        StartCoroutine(SpawnEnemies());
        finishedCountDown = true;
    }

    //// Hack to account for the starting countdown.
    //// Manually must enter the number of seconds for the countdown.
    //IEnumerator WaitForCountdown()
    //{
    //    if (finishedCountDown == false) 
    //    {
    //        yield return new WaitForSeconds(3);
    //        if (finishedCountDown != true)
    //            finishedCountDown = true;
    //        else
    //            yield break;
    //    }
    //}

	#region Update Loop

	void Update() 
	{
        //// new stuff
        //if (finishedCountDown == true && playerController != null) //playercontroller if null is to check if the player has died or not since there is no health variable
        //{
        //    int time = (int)Time.timeSinceLevelLoad - startCountdown; // counter the start delay time
        //    timeText.text = time.ToString();
        //    if (elapsedTime > storedTime)
        //    {
        //        timeText.color = Color.green;
        //        submitTime.interactable = true;
        //    }
        //    if (restart)
        //    {
        //        if (Input.GetKeyDown(KeyCode.R)) // || Input.touchCount > 0)
        //        {
        //            RestartLevel();
        //        }
        //    }
        //    BombSpawns();

        //    if (Random.Range(0, 2) == 0)
        //    {
        //        bombSpawned = true;
        //        BombSpawns();
        //    }
        //    if (Random.Range(1, 10) > 8)
        //    {
        //        shieldSpawned = true;
        //        StartCoroutine(WaitForSecs(10)); // will cause a wait for a random time in seconds between 0 and 10
        //    }
        //}
        ////else if (finishedCountDown == false)
        ////    StartCoroutine(WaitForCountdown());
	}

	#endregion

    public void RestartLevel()
    {
        elapsedTime = 0;
        timeText.text = "0";
        submitScore.interactable = false;
        submitTime.interactable = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // **Deprecated 
        // Application.LoadLevel(Application.loadedLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
        // **Deprecated 
        // Application.LoadLevel(0);
    }

	#region Ammo Management

	public void UpdateAmmo()
	{
        if (unlimitedAmmo)
        {
            ammoCount = 9999;
            //ammoText.text = Mathf.Infinity.ToString();
            return;
        }
		if(ammoCount == 0)
		{
			ammoText.text = "0";
		}
		else if(ammoCount >= 20)
		{
			ammoCount = 20;
            ammoText.text = "20";
		}
		else
			ammoText.text = ammoCount.ToString();
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

	IEnumerator SpawnMeteors()
	{
		yield return new WaitForSeconds (startWait); // Calling a coroutine
		while (true)
		{

            currentRound++;

			int hazardCount = Random.Range(hazardCountMin, hazardCountMax);
			for(int i = 0; i < hazardCount; i++)
			{
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), 
                                                     0, 
                                                     spawnValues.z);
                if(i % 3 == 0)
                {
                    // every few lets designate a spawn in a limited random range
                    spawnPosition = new Vector3(Random.Range(-3, 3),
                                                     0,
                                                     spawnValues.z);
                }
				
                // Instantiate Meteors
                Quaternion spawnRotation = Quaternion.identity;
                float randomSize = Random.Range(0.4f, 1);
                GameObject goMeteor = hazard[Random.Range(0, hazard.Length-1)];
                goMeteor.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
                Instantiate(goMeteor, spawnPosition, spawnRotation);

                yield return new WaitForSeconds (spawnWait); // Calling a coroutine
			}
            
            yield return new WaitForSeconds (waveWait);

			if(gameOver)
			{
                touchInput.SetActive(false);
                menuPanel.SetActive(true);
				// restartText.text = "Press 'R' for Restart \n or \n Tap the screen";
				restart = true;
				break;
			}
		}
	}

	IEnumerator SpawnEnemies()
	{
		yield return new WaitForSeconds (startWait); // Calling a coroutine
		while (true)
		{

            currentRound++;
    
            if (intEnemyCount == 3)
                intEnemyCount = 1;
            else
                intEnemyCount++;

            for (int i = 1; i <= intEnemyCount; i++)
            {
                Vector3 enemySpawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion enemySpawnRotation = Quaternion.identity;
                Instantiate(enemyGameObjects[Random.Range(0, enemyGameObjects.Length - 1)],
                                enemySpawnPosition,
                                enemySpawnRotation);

                yield return new WaitForSeconds (spawnWait); // Calling a coroutine
			}
            
            yield return new WaitForSeconds (waveWait);

			if(gameOver)
			{
                // restartText.text = "Press 'R' for Restart \n or \n Tap the screen";
                touchInput.SetActive(false);
                menuPanel.SetActive(true);
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
        Debug.Log("attempting to spawn another bomb");
        if(Random.Range(0,1) == 1)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-bombSpawnValues.x, bombSpawnValues.x),
                                                 bombSpawnValues.y,
                                                 Random.Range(minZ, maxZ));
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(bomb, spawnPosition, spawnRotation);
        }
	}

	public void AddBombs (int bombValue)
	{
        if(bombCount <= 2)
        {
            bombCount += bombValue;
        }
        UpdateBombs();
    }

    void UpdateBombs()
	{
		bombText.text = bombCount.ToString();
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
        if(Random.Range(0,1) == 1)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-shieldSpawnValues.x, shieldSpawnValues.x),
                                                 shieldSpawnValues.y,
                                                 Random.Range(minZ, maxZ));
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(shield, spawnPosition, spawnRotation);
        }
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
		scoreText.text = score.ToString();
        if (score >= hiScore)
        {
            submitScore.interactable = true;
            hiScoreText.text = scoreText.text;
            hiScoreText.color = Color.green;
        }
    }

	#endregion

	public void GameOver()
    {
        if (score > hiScore)
            PlayerPrefs.SetInt("Player Score", score);

        elapsedTime = (int)Time.timeSinceLevelLoad - startCountdown; // counter start delay time
        if (elapsedTime > storedTime)
            PlayerPrefs.SetInt("Player Time", elapsedTime);
		gameOverText.text = "Game Over";
		gameOver = true;
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, "CgkIp4zItNoREAIQBg", (bool success) =>
            {
                if (success)
                    Debug.Log("score uploaded");
                else
                    Debug.Log("score not uploaded");
                // handle success or failure
            });
            Social.ReportScore(elapsedTime, "CgkIp4zItNoREAIQBw", (bool success) =>
            {
                // handle success or failure
                if (success)
                    Debug.Log("time uploaded");
                else
                    Debug.Log("time not uploaded");
            });
        }

        hiScoreText.text = score.ToString();
        recordTimeText.text = elapsedTime.ToString();

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
