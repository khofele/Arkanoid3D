using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int lives = 3;
    [SerializeField] private int score = 0;
    [SerializeField] private int highscore = 0;
    [SerializeField] private Ball ball = null;
    [SerializeField] private Paddle paddle = null;

    private bool isRunning = false;

    private static GameManager instance = null;

    public static GameManager Instance
    {
        get => instance;
    }

    public int Lives
    {
        get => lives;
        set
        { 
            lives = value; 
        }
    }

    public int Score
    {
        get => score;
        set
        {
            score = value;
        }
    }

    public int Highscore
    {
        get => highscore;
        set
        {
            highscore = value;
        }
    }

    public bool IsRunning
    {
        get => isRunning;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            Application.Quit();
        }

        if(isRunning == false && Input.GetKeyDown(KeyCode.Space))
        {
            isRunning = true;
            UIManager.Instance.DisableGameWon();
            UIManager.Instance.DisableGameOver();
            ResetGame();
        }

        if (isRunning == true && BoxManager.Instance.CheckBoxes() == true)
        {
            UIManager.Instance.DisplayGameWon();
            ball.ResetBall();
            paddle.ResetPaddle();
            BoxManager.Instance.ResetBoxes();
            ItemManager.Instance.ResetItems();
            isRunning = false;
        }
    }

    public bool IsDead()
    {
        if (lives <= 0)
        {
            isRunning = false;
            UIManager.Instance.DisplayGameOver();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ResetGame()
    {
        lives = 3;
        score = 0;
    }

    public void CheckHighscore()
    {
        if(score > highscore)
        {
            highscore = score;
        }
    }
}
