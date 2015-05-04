using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text textHiScore;

    public void Start()
    {
        int highscore = 0;
        if (PlayerPrefs.HasKey("Player Score")) 
             highscore = PlayerPrefs.GetInt("Player Score");
        textHiScore.text = "Highest Score: " + highscore;
    }

    public void StartGame()
    {
        Application.LoadLevel("Main");
    }
}
