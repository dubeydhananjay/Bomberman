using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour {

    [SerializeField]
    private TMPro.TextMeshProUGUI scoreText;
    
    public void Activation(bool activate) {
        gameObject.SetActive(activate);
    }

    public void SetScoreText(int score) {
        scoreText.text = string.Format("Score: {0}",score);
    }
}
