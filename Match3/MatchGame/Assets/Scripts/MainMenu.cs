using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : Singleton<MainMenu>
{
    public TMP_Text playerID;

    public SceneDropdown sceneListDropdown;

    public string ActiveScene;

    private void Start()
    {
        playerID.gameObject.SetActive(true);

        SetPlayerID();
    }

    public void MoveToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void SetPlayerID()
    {
        if (DevTools.Instance.isUsingMultiplayer)
        {
            playerID.text = "PlayerID: ";

            //do logic here to sign into leaderboard system and grab player id

            //take playerid from leaderboard and add to playerID.text
        }
        else if(DevTools.Instance.isDevelopmentBuild)
        {
            playerID.text = "Development Build";
        }
        else
        {
            playerID.gameObject.SetActive(false);
        }
    }

    public void PlayLevel()
    {
        if (ActiveScene != "")
        {
            SceneManager.LoadScene(ActiveScene);
        }
    }


}
