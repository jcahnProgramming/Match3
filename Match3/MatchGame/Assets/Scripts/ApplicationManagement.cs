using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationManagement : Singleton<ApplicationManagement>
{
    public GameObject ParticleManager;
    public GameObject ScoreManager;
    public GameObject GameManager;
    public GameObject SoundManager;
    public GameObject UIManager;
    public GameObject CollectionGoalPrefab;
    public GameObject MainMenuManager;

    [SerializeField]bool m_isPlayingLevel = false;
    [SerializeField]bool m_isMainMenu = true;

    void DetectScene()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            m_isMainMenu = true;
            m_isPlayingLevel = false;
        }
        else
        {
            m_isMainMenu = false;
            m_isPlayingLevel = true;
        }
    }


    private void Start()
    {
        DetectScene();

        DestroyManagers();
        
        if (m_isMainMenu)
        {
            CreateMainMenuManagers();
        }
        else if (m_isPlayingLevel)
        {
            //CreateLevelManagers();
            //UpdateLevelManagers();
        }
        else
        {
            Debug.Log("APPLICATION_MANAGER: How did you get here?");        
        }
        

    }

    void DestroyManagers()
    {
        //if (ParticleManager.activeInHierarchy)
        //{
        //    Destroy(ParticleManager);
        //}
        if (ScoreManager.activeInHierarchy)
        {
            Destroy(ScoreManager);
        }
        if (GameManager.activeInHierarchy)
        {
            Destroy(GameManager);
        }
        if (SoundManager.activeInHierarchy)
        {
            Destroy(SoundManager);
        }
        if (UIManager.activeInHierarchy)
        {
            Destroy(UIManager);
        }
        if (MainMenuManager.activeInHierarchy)
        {
            Destroy(MainMenuManager);
        }
    }

    void CreateMainMenuManagers()
    {
        GameObject m_mainMenuManager = Instantiate(MainMenuManager, gameObject.transform, true);
        m_mainMenuManager.name = "MainMenuManager";
    }

    void UpdateLevelManagers()
    {
        UIManager.transform.parent = gameObject.transform;
        SoundManager.transform.parent = gameObject.transform;
        GameManager.transform.parent = gameObject.transform;
        ScoreManager.transform.parent = gameObject.transform;
    }

    void CreateLevelManagers()
    {        
        //GameObject m_particleManager = Instantiate(ParticleManager, gameObject.transform, true);
        //m_particleManager.name = "ParticleManager";
        //m_particleManager.tag = "ParticleManager";

        GameObject m_GameManager = Instantiate(GameManager, gameObject.transform, true);
        m_GameManager.name = "GameManager";

        GameObject m_scoreManager = Instantiate(ScoreManager, gameObject.transform, true);
        m_scoreManager.name = "ScoreManager";

        GameObject m_soundManager = Instantiate(SoundManager, gameObject.transform, true);
        m_soundManager.name = "SoundManager";

        GameObject m_UIManager = Instantiate(UIManager, gameObject.transform, true);
        m_UIManager.name = "UIManager";
        m_UIManager.transform.parent = gameObject.transform;
    }
}
