using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score = null;
    [SerializeField] private TextMeshProUGUI highscore = null;
    [SerializeField] private TextMeshProUGUI lives = null;
    [SerializeField] private GameObject gameOver = null;
    [SerializeField] private GameObject gameWon = null;

    private static UIManager instance = null;

    public static UIManager Instance
    {
        get => instance;
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
        score.text = "Score: " + GameManager.Instance.Score;
        lives.text = "Lives: " + GameManager.Instance.Lives;
        highscore.text = "Highscore: " + GameManager.Instance.Highscore;
    }

    public void DisplayGameOver()
    {
        gameOver.SetActive(true);
    }

    public void DisableGameOver()
    {
        gameOver.SetActive(false);
    }

    public void DisplayGameWon()
    {
        gameWon.SetActive(true);
    }

    public void DisableGameWon()
    {
        gameWon.SetActive(false);
    }
}