using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{

    [SerializeField]public GameObject collectionGoalLayout;

    public int collectionGoalBaseWidth = 125;

    CollectionGoalPanel[] m_collectionGoalPanels;

    // reference to graphic that fades in and out
    public ScreenFader screenFader;

    // UI.Text that stores the level name
    public Text levelNameText;

    // UI.Text that shows how many moves are left
    public Text movesLeftText;

    // reference to three-star score meter
    public ScoreMeter scoreMeter;

    // reference to the custom UI window
    public MessageWindow messageWindow;

    public GameObject movesCounter;

    public Timer timer;

    public bool GetInfo = false;

    private void Update()
    {
        if (GetInfo)
        {
            GetData();
        }
    }

    void GetData(Scene scene, LoadSceneMode mode)
    {
        collectionGoalLayout = GameObject.Find("Canvas,Overlay/TopPanel/CollectionGoalLayout");
        screenFader = GameObject.FindWithTag("ScreenFader").GetComponent<ScreenFader>();
        levelNameText = GameObject.Find("Canvas,Overlay/TopPanel/LevelNameText").GetComponent<Text>();
        movesLeftText = GameObject.Find("Canvas,Overlay/TopPanel/MovesLeftText").GetComponent<Text>();
        scoreMeter = GameObject.Find("Canvas,ScreenSpaceCameraBackground/BottomPanel/ScoreMeter").GetComponent<ScoreMeter>();
        messageWindow = GameObject.Find("Canvas,Overlay/MessageWindow").GetComponent<MessageWindow>();
        movesCounter = GameObject.Find("Canvas,Overlay/MovesCounter");
        timer = GameObject.Find("Canvas,Overlay/TimerUI").GetComponent<Timer>();  
    }

    void GetData()
    {
        collectionGoalLayout = GameObject.Find("Canvas,Overlay/TopPanel/CollectionGoalLayout");
        screenFader = GameObject.FindWithTag("ScreenFader").GetComponent<ScreenFader>();
        levelNameText = GameObject.Find("Canvas,Overlay/TopPanel/LevelNameText").GetComponent<Text>();
        movesLeftText = GameObject.Find("Canvas,Overlay/TopPanel/MovesLeftText").GetComponent<Text>();
        scoreMeter = GameObject.Find("Canvas,ScreenSpaceCameraBackground/BottomPanel/ScoreMeter").GetComponent<ScoreMeter>();
        messageWindow = GameObject.Find("Canvas,Overlay/MessageWindow").GetComponent<MessageWindow>();
        movesCounter = GameObject.Find("Canvas,Overlay/MovesCounter");
        timer = GameObject.Find("Canvas,Overlay/TimerUI").GetComponent<Timer>();
    }



    public override void Awake()
    {
        base.Awake();

        //SceneManager.sceneLoaded += GetData;

        if (messageWindow != null)
        {
            messageWindow.gameObject.SetActive(true);
        }

        if (screenFader != null)
        {
            screenFader.gameObject.SetActive(true);
        }
    }

    public void SetupCollectionGoalLayout(CollectionGoal[] collectionGoals, GameObject goalLayout, int spacingWidth)
    {
        if (goalLayout != null && collectionGoals != null && collectionGoals.Length != 0)
        {
            RectTransform rectXform = goalLayout.GetComponent<RectTransform>();
            rectXform.sizeDelta = new Vector2(collectionGoals.Length * spacingWidth, rectXform.sizeDelta.y);

            CollectionGoalPanel[] panels = goalLayout.GetComponentsInChildren<CollectionGoalPanel>();

            for (int i = 0; i < panels.Length; i++)
            {
                if (i < collectionGoals.Length && collectionGoals[i] != null)
                {
                    panels[i].gameObject.SetActive(true);
                    panels[i].collectionGoal = collectionGoals[i];
                    panels[i].SetupPanel();
                }
                else
                {
                    panels[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void SetupCollectionGoalLayout(CollectionGoal[] collectionGoals)
    {
        SetupCollectionGoalLayout(collectionGoals, collectionGoalLayout, collectionGoalBaseWidth);
    }

    public void UpdateCollectionGoalLayout(GameObject goalLayout)
    {
        if (goalLayout != null)
        {
            CollectionGoalPanel[] panels = goalLayout.GetComponentsInChildren<CollectionGoalPanel>();

            if (panels != null && panels.Length != 0)
            {
                foreach (CollectionGoalPanel panel in panels)
                {
                    if (panel != null && panel.isActiveAndEnabled)
                    {
                        panel.UpdatePanel();
                    }
                }
            }
        }
    }

    public void UpdateCollectionGoalLayout()
    {
        UpdateCollectionGoalLayout(collectionGoalLayout);
    }

    public void EnableTimer(bool state)
    {
        if (timer != null)
        {
            timer.gameObject.SetActive(state);
        }
    }

    public void EnableMovesCounter(bool state)
    {
        if (movesCounter != null)
        {
            movesCounter.SetActive(state);
        }
    }

    public void EnableCollectionGoalLayout(bool state)
    {
        if (collectionGoalLayout != null)
        {
            collectionGoalLayout.SetActive(state);
        }
    }

}
