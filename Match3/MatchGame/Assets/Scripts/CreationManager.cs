using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelType 
{ 
    Collected,
    Scored,
    Timed,
    Infinite
}


public class CreationManager : Singleton<CreationManager>
{
    public LevelType levelType = LevelType.Infinite;

    public int[] scoreGoals;
    public int movesLeft;
    public int timeLeft;
    public LevelCounter levelCounter = LevelCounter.Moves;
    public CollectionGoal[] collectionGoals;


    public override void Awake()
    {
        base.Awake();
        //CreateGameManager();
        //SetupLevel();
    }

    void CreateGameManager()
    {
        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance);

            Instantiate(GameManager.Instance, Vector3.zero, Quaternion.identity, GameObject.Find("Managers").transform);
            
            switch (levelType)
            {
                case LevelType.Collected:
                    {
                        GameManager.Instance.gameObject.AddComponent<LevelGoalCollected>();
                    }
                    break;
            }
        }
    }
    public void SetupLevel()
    {
        switch (levelType)
        {
            case LevelType.Collected:
                {
                    GameManager.Instance.gameObject.GetComponent<LevelGoalCollected>().enabled = true;
                    GameManager.Instance.gameObject.GetComponent<LevelGoalScored>().enabled = false;
                    GameManager.Instance.gameObject.GetComponent<LevelGoalTimed>().enabled = false;
                    GameManager.Instance.gameObject.GetComponent<LevelGoalInfinite>().enabled = false;
                }
                break;
            case LevelType.Scored:
                {
                    GameManager.Instance.gameObject.GetComponent<LevelGoalCollected>().enabled = false;
                    GameManager.Instance.gameObject.GetComponent<LevelGoalScored>().enabled = true;
                    GameManager.Instance.gameObject.GetComponent<LevelGoalTimed>().enabled = false;
                    GameManager.Instance.gameObject.GetComponent<LevelGoalInfinite>().enabled = false;
                    Debug.LogFormat("CREATION_MANAGER: Infinite Level Installing...", Color.green);
                    //Debug.Log("CREATION_MANAGER: Infinite Level Installing...");
                }
                break;
            case LevelType.Timed:
                {
                    GameManager.Instance.gameObject.GetComponent<LevelGoalCollected>().enabled = false;
                    GameManager.Instance.gameObject.GetComponent<LevelGoalScored>().enabled = false;
                    GameManager.Instance.gameObject.GetComponent<LevelGoalTimed>().enabled = true;
                    GameManager.Instance.gameObject.GetComponent<LevelGoalInfinite>().enabled = false;
                    Debug.Log("CREATION_MANAGER: Timed Level Installing...");
                }
                break;
            case LevelType.Infinite:
                {
                    GameManager.Instance.gameObject.GetComponent<LevelGoalCollected>().enabled = false;
                    GameManager.Instance.gameObject.GetComponent<LevelGoalScored>().enabled = false;
                    GameManager.Instance.gameObject.GetComponent<LevelGoalTimed>().enabled = false;
                    GameManager.Instance.gameObject.GetComponent<LevelGoalInfinite>().enabled = true;
                    Debug.Log("CREATION_MANAGER: Infinite Level Installing...");
                }
                break;
            default:
                {
                    Debug.Log("CREATION_MANAGER: Not sure how you got here? Congrats I guess? You must have played yourself... To get here you would have really had to have screwed up somewhere. ggs :) -Path");
                }
                break;
        }
    }
}
