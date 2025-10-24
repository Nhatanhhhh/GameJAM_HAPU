using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public TextMeshProUGUI scoreText;
    public GameObject WinPanel;

    public TextMeshProUGUI FinalScoreAndTimeText;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        if (WinPanel != null)
        {
            WinPanel.SetActive(false);
        }
    }

    public void UpdateScoreUI(int currentScore)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString();
        }
    }

    public void ShowWinPanle()
    {
        if (WinPanel != null)
        {
            WinPanel.SetActive(true);
        }
    }

    public void UpdateFinalScore()
    {
        if (FinalScoreAndTimeText != null)
        {
            FinalScoreAndTimeText.text = "Final Score: " + PointManager.Instance.currentScore + "Final Time : " + PointManager.Instance.playTime.ToString("F2") + " seconds";
        }
    }

}
