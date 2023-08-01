using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGoalInfinite : LevelGoal
{
    public override void Start()
    {
        //levelCounter = LevelCounter.Infinite;
        base.Start();
        SetLevelGoals();
    }
    public override bool IsGameOver()
    {
        while (!playerEndingGame)
        {
            return false;
        }
        return true;
    }
    public override bool IsWinner()
    {
        while (!playerEndingGame)
        {
            return false;
        }
        return true;
    }

    public void PlayerWantsToEndGame(bool value, Sprite image)
    {
        playerEndingGame = value;

        if (playerEndingGame)
        {
            HighscoreSync();
            UIManager.Instance.messageWindow.ShowMessage(image, "Ending Game", "Ok");
        }
    }

    void SetLevelGoals()
    {
        if (PlayerPrefs.HasKey("highscore_1"))
        {
            if (scoreGoals.Length > 0)
            {
                scoreGoals[0] = PlayerPrefs.GetInt("highscore_1");
            }
            else
            {
                scoreGoals[0] = 1000;
            }
        }
        if (PlayerPrefs.HasKey("highscore_2"))
        {
            if (scoreGoals.Length > 0)
            {
                scoreGoals[1] = PlayerPrefs.GetInt("highscore_2");
            }
            else
            {
                scoreGoals[1] = 3000;
            }
        }
        if (PlayerPrefs.HasKey("highscore_3"))
        {
            if (scoreGoals.Length > 0)
            {
                scoreGoals[2] = PlayerPrefs.GetInt("highscore_3");
            }
            else
            {
                scoreGoals[2] = 10000;
            }
        }
    }

    void HighscoreSync()
    {
        if (ScoreManager.Instance != null)
        {
            if (PlayerPrefs.HasKey("highscore_3"))
            {
                if (ScoreManager.Instance.CurrentScore > PlayerPrefs.GetInt("highscore_3"))
                {
                    PlayerPrefs.SetInt("highscore_3", ScoreManager.Instance.CurrentScore);
                }
                else if (ScoreManager.Instance.CurrentScore > PlayerPrefs.GetInt("highscore_2"))
                {
                    PlayerPrefs.SetInt("highscore_2", ScoreManager.Instance.CurrentScore);
                }
                else if (ScoreManager.Instance.CurrentScore > PlayerPrefs.GetInt("highscore_1"))
                {
                    PlayerPrefs.SetInt("highscore_1", ScoreManager.Instance.CurrentScore);
                }
            }
            else if (PlayerPrefs.HasKey("highscore_2"))
            {
                if (ScoreManager.Instance.CurrentScore > PlayerPrefs.GetInt("highscore_2"))
                {
                    PlayerPrefs.SetInt("highscore_2", ScoreManager.Instance.CurrentScore);
                }
                else if (ScoreManager.Instance.CurrentScore > PlayerPrefs.GetInt("highscore_1"))
                {
                    PlayerPrefs.SetInt("highscore_1", ScoreManager.Instance.CurrentScore);
                }
            }
            else if (PlayerPrefs.HasKey("highscore_1"))
            {
                if (ScoreManager.Instance.CurrentScore > PlayerPrefs.GetInt("highscore_1"))
                {
                    PlayerPrefs.SetInt("highscore_1", ScoreManager.Instance.CurrentScore);
                }
            }
            else
            {
                PlayerPrefs.SetInt("highscore_1", ScoreManager.Instance.CurrentScore);
            }

        }
        if (PlayerPrefs.HasKey("highscore_2"))
        {
            if (scoreGoals.Length > 0)
            {
                scoreGoals[1] = PlayerPrefs.GetInt("highscore_2");
            }
        }
        if (PlayerPrefs.HasKey("highscore_3"))
        {
            if (scoreGoals.Length > 0)
            {
                scoreGoals[2] = PlayerPrefs.GetInt("highscore_3");
            }
        }
    }

    
}
