using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class UIManager : MonoBehaviour {

    public Text textHiScore;
    public Text textTopTime;

    public Text usernameText;
    public Button signInButton;
    public Button signOutButton;
    public Button leaderboardButton;
    public Button achievementButton;

    public void Start()
    {
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();

        Social.localUser.Authenticate((bool success) => {
            // handle success or failure
            if (success)
            {
                Debug.Log("successful login");
            }
            else
            {
                Debug.Log("login failed");
            }
        });

        int highscore = 0;
        int toptime = 0;
        if (PlayerPrefs.HasKey("Player Score")) 
             highscore = PlayerPrefs.GetInt("Player Score");
        textHiScore.text = highscore.ToString();

        if (PlayerPrefs.HasKey("Player Time"))
            toptime = PlayerPrefs.GetInt("Player Time");
        textTopTime.text = toptime.ToString();
    }

    void Update()
    {
        if(UserLoggedIn())
        {
            signInButton.interactable = false;
            leaderboardButton.interactable = true;
            achievementButton.interactable = true;
        }
        else
        {
            signInButton.interactable = true;
            leaderboardButton.interactable = false;
            achievementButton.interactable = false;
        }

    }

    public void StartGame()
    {
        Application.LoadLevel("Main");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Login()
    {
        // authenticate user:
        Social.localUser.Authenticate((bool success) => {
            // handle success or failure
            if(success)
            {
                Debug.Log("successful login");
            }
            else
            {
                Debug.Log("login failed");
            }
        });
    }

    public void DisplayLoggedInUser()
    {
        if(!UserLoggedIn())
        {
            signOutButton.interactable = false;
            usernameText.text = "Not signed in";
        }
        else
        {
            usernameText.text = ((PlayGamesLocalUser)Social.localUser).Email;
            signOutButton.interactable = true;
        }
    }

    bool UserLoggedIn()
    {
        if (!PlayGamesPlatform.Instance.IsAuthenticated())
        {
            Debug.Log("Not Logged In");
            return false;
        }
        Debug.Log("Logged In");
        return true;
    }

    public void Logout()
    {
        // sign out
        PlayGamesPlatform.Instance.SignOut();
    }

    public void ShowAchievements()
    {
        Social.ShowAchievementsUI();
    }

    public void ShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }
}

