using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    [SerializeField]
    private GameOverPanel gameOverPanel;    
    [SerializeField]
    private WinGamePanel winGamePanel;
    [SerializeField]
    private TMPro.TextMeshProUGUI scoreText;
    private int score = -1;

    public static UIManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
        IncrementScore();
    }

    private void SetScoreText() {
        scoreText.text = string.Format("Score:\n{0}",score);
    }

    public void ActivateWinGamePanel() {
        if(GameManager.Instance.GameLost) return;
        winGamePanel.Activation(true);
    }

     public void ActivateGameOverPanel() {
        gameOverPanel.Activation(true);
        gameOverPanel.SetScoreText(score);
    }

    public void IncrementScore() {
        score += 1;
        SetScoreText();
    }

    public void ResetScore() {
        score = 0;
        SetScoreText();
    }
}
