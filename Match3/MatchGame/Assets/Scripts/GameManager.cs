using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



[RequireComponent(typeof(LevelGoal))]
public class GameManager : Singleton<GameManager>
{
    Board m_board;

    bool m_isReadyToBegin = false;
    bool m_isGameOver = false;

    public bool IsGameOver
    {
        get { return m_isGameOver; }
        set { m_isGameOver = value; }
    }
    
    bool m_isWinner = false;
    bool m_isReadyForMainMenu = false;

    LevelGoal m_levelGoal;

    [SerializeField] public LevelType levelType = LevelType.Infinite;

    //LevelGoalTimed m_levelGoalTimed;

    LevelGoalCollected m_levelGoalCollected;

    public LevelGoal LevelGoal { get { return m_levelGoal; } }


    public override void Awake()
    {
        base.Awake();

        m_levelGoal = GetComponent<LevelGoal>();
        //m_levelGoalTimed = GetComponent<LevelGoalTimed>();
        m_levelGoalCollected = GetComponent<LevelGoalCollected>();
        m_board = GameObject.FindObjectOfType<Board>().GetComponent<Board>();
    }


    // Start is called before the first frame update
    void Start()
    {
        if (UIManager.Instance != null)
        {
            if (UIManager.Instance.scoreMeter != null)
            {
                UIManager.Instance.scoreMeter.SetupStars(m_levelGoal);
            }
            if (UIManager.Instance.levelNameText != null)
            {
                Scene scene = SceneManager.GetActiveScene();
                UIManager.Instance.levelNameText.text = scene.name;
            }
            if (m_levelGoalCollected != null)
            {
                UIManager.Instance.EnableCollectionGoalLayout(true);
                UIManager.Instance.SetupCollectionGoalLayout(m_levelGoalCollected.collectionGoals);
            }
            else
            {
                UIManager.Instance.EnableCollectionGoalLayout(false);
            }

            bool useTimer = (m_levelGoal.levelCounter == LevelCounter.Timer);

            UIManager.Instance.EnableTimer(useTimer);
            UIManager.Instance.EnableMovesCounter(!useTimer);
        }

        m_levelGoal.movesLeft++;
        UpdateMoves();

        StartCoroutine("ExecuteGameLoop");
    }

    public void UpdateMoves()
    {
        if (m_levelGoal.levelCounter == LevelCounter.Moves)
        {
            m_levelGoal.movesLeft--;
            if (UIManager.Instance != null && UIManager.Instance.movesLeftText != null)
            {
                UIManager.Instance.movesLeftText.text = m_levelGoal.movesLeft.ToString();
            }
        }
    }

    IEnumerator ExecuteGameLoop()
    {
        yield return StartCoroutine("StartGameRoutine");
        yield return StartCoroutine("PlayGameRoutine");
        yield return StartCoroutine("WaitForBoardRoutine", 0.5f);
        yield return StartCoroutine("EndGameRoutine");
    }

    public void BeginGame()
    {
        m_isReadyToBegin = true;
    }

    IEnumerator StartGameRoutine()
    {
        if (UIManager.Instance != null)
        {
            if (UIManager.Instance.messageWindow != null)
            {
                UIManager.Instance.messageWindow.GetComponent<RectXformMover>().MoveOn();
                int maxGoal = m_levelGoal.scoreGoals.Length - 1;
                UIManager.Instance.messageWindow.ShowScoreMessage(m_levelGoal.scoreGoals[maxGoal]);

                if (m_levelGoal.levelCounter == LevelCounter.Timer)
                {
                    UIManager.Instance.messageWindow.ShowTimedGoal(m_levelGoal.timeLeft);
                }
                else
                {
                    UIManager.Instance.messageWindow.ShowMovesGoal(m_levelGoal.movesLeft);
                }
                if (m_levelGoalCollected != null)
                {
                    UIManager.Instance.messageWindow.ShowCollectionGoal(true);

                    GameObject goalLayout = UIManager.Instance.messageWindow.collectionGoalLayout;

                    if (goalLayout != null)
                    {
                        UIManager.Instance.SetupCollectionGoalLayout(m_levelGoalCollected.collectionGoals, goalLayout, 100);
                    }
                }
            }
        }

        while (!m_isReadyToBegin)
        {
            yield return null;
        }

        if (UIManager.Instance != null && UIManager.Instance.screenFader != null)
        {
            UIManager.Instance.screenFader.FadeOff();
        }

        yield return new WaitForSeconds(0.5f);

        if (m_board != null)
        {
            m_board.SetupBoard();
        }
    }

    IEnumerator PlayGameRoutine()
    {
        if (m_levelGoal.levelCounter == LevelCounter.Timer)
        {
            m_levelGoal.StartCountdown();
        }

        while (!m_isGameOver)
        {
            m_isGameOver = m_levelGoal.IsGameOver();

            m_isWinner = m_levelGoal.IsWinner();

            yield return null;
        }
    }
    
    IEnumerator WaitForBoardRoutine(float delay = 0f)
    {
        if (m_levelGoal.levelCounter == LevelCounter.Timer && UIManager.Instance != null && UIManager.Instance.timer != null)
        {
                UIManager.Instance.timer.FadeOff();
                UIManager.Instance.timer.paused = true;
        }

        if (m_board != null)
        {
            yield return new WaitForSeconds(m_board.swapTime);

            while (m_board.isRefilling)
            {
                yield return null;
            }
        }
        yield return new WaitForSeconds(delay);
    }

    IEnumerator EndGameRoutine()
    {
        m_isReadyForMainMenu = false;

        if (m_isWinner)
        {
            ShowWinScreen();
        }
        else
        {
            ShowLoseScreen();
        }

        yield return new WaitForSeconds(1f);

        if (UIManager.Instance != null && UIManager.Instance.screenFader != null)
        {
            UIManager.Instance.screenFader.FadeOn();
        }

        while (!m_isReadyForMainMenu)
        {
            yield return null;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reload scene (disable this when main menu is complete)
        //SceneManager.LoadScene("MainMenu"); //sends player to main menu (enable this when main menu is complete)
    }

    public void ToMainMenu()
    {
        m_isReadyForMainMenu = true;
    }

    public void ScorePoints(GamePiece piece, int multiplier = 1, int bonus = 0)
    {
        if (piece != null)
        {

            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(piece.scoreValue * multiplier + bonus);
                m_levelGoal.UpdateScoreStars(ScoreManager.Instance.CurrentScore);

                if (UIManager.Instance != null && UIManager.Instance.scoreMeter != null)
                {
                    UIManager.Instance.scoreMeter.UpdateScoreMeter(ScoreManager.Instance.CurrentScore, m_levelGoal.scoreStars);
                }
            }

            if (SoundManager.Instance != null && piece.clearSound != null)
            {
                SoundManager.Instance.PlayClipAtPoint(piece.clearSound, Vector3.zero, SoundManager.Instance.fxVolume);
            }
        }
    }

    public void AddTime(int timeValue)
    {
        if(m_levelGoal.levelCounter == LevelCounter.Timer)
        {
            m_levelGoal.AddTime(timeValue);
        }
    }

    public void UpdateCollectionGoals(GamePiece pieceToCheck)
    {
        if (m_levelGoalCollected != null)
        {
            m_levelGoalCollected.UpdateGoals(pieceToCheck);
        }
    }

    void ShowWinScreen()
    {
        if (UIManager.Instance != null && UIManager.Instance.messageWindow != null)
        {
            UIManager.Instance.messageWindow.GetComponent<RectXformMover>().MoveOn();
            UIManager.Instance.messageWindow.ShowWinMessage();
            UIManager.Instance.messageWindow.ShowCollectionGoal(false);

            if (ScoreManager.Instance != null)
            {
                string scoreStr = "you scored\n" + ScoreManager.Instance.CurrentScore.ToString() + " points!";
                UIManager.Instance.messageWindow.ShowGoalCaption(scoreStr, 0, 70);
            }

            if (UIManager.Instance.messageWindow.goalCompleteIcon != null)
            {
                UIManager.Instance.messageWindow.ShowGoalImage(UIManager.Instance.messageWindow.goalCompleteIcon);
            }
        }
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayWinSound();
        }
    }

    void ShowLoseScreen()
    {
        if (UIManager.Instance != null && UIManager.Instance.messageWindow != null)
        {
            UIManager.Instance.messageWindow.GetComponent<RectXformMover>().MoveOn();
            UIManager.Instance.messageWindow.ShowLoseMessage();
            UIManager.Instance.messageWindow.ShowCollectionGoal(false);


            string caption = "";
            if (m_levelGoal.levelCounter == LevelCounter.Timer)
            {
                caption = "Out of time!";
            }
            else
            {
                caption = "Out of moves!";
            }

            UIManager.Instance.messageWindow.ShowGoalCaption(caption, 0, 70);

            if (UIManager.Instance.messageWindow.goalFailedIcon != null)
            {
                UIManager.Instance.messageWindow.ShowGoalImage(UIManager.Instance.messageWindow.goalFailedIcon);
            }
        }
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayLoseSound();
        }
    }
}
