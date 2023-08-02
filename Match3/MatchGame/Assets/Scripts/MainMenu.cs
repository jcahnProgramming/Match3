using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMP_Text playerID;

    public SceneDropdown sceneListDropdown;

    Button m_playLevelButton;

    public string ActiveScene;

    public int ActiveSceneNumber;

    private void Start()
    {
        SetRequiredInfo();

        playerID.gameObject.SetActive(true);

        SetPlayerID();
    }

    private void Update()
    {
            if (sceneListDropdown == null && playerID == null)
            {
                SetRequiredInfo();
            }
    }

    public void SetRequiredInfo()
    {
        if (SceneManager.loadedSceneCount == 0)
        {
            playerID = GameObject.Find("GameInformationText").GetComponent<TMP_Text>();
            sceneListDropdown = GameObject.Find("SceneListDropdown").GetComponent<SceneDropdown>();
            m_playLevelButton = GameObject.Find("PlayLevelButton").GetComponent<Button>();

            m_playLevelButton.onClick.AddListener(PlayLevel);
        }

        
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
            int levelNumber = ActiveSceneNumber;

            PlayerPrefsSaveLoad.Instance.SaveInt("level", levelNumber);
            SceneManager.LoadScene(1);
        }
    }


}
