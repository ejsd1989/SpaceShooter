using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class UX_handler : MonoBehaviour
{
    //This for the main menu to the login panel 
    //once the player presses the play button
    //from there the player can sigin or play as a guest
    public GameObject mainMenu;
    public GameObject LoginPanel;

    public void OnClickGoToLoginPanel()
    {
        LoginPanel.SetActive(true);
        mainMenu.SetActive(false);
    }
}
