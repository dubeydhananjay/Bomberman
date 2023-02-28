using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    [SerializeField]
    private Level[] pfLevels;
    private Level currentLevel;
    private int levelNo = -1;

    public Level CurrentLevel => currentLevel;
    public bool GameLost{ get; set; }
    public static GameManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
        SetCurrentLevel();
    }

    //From list of level set the next level
    public void SetCurrentLevel() {
        GameLost = false;
        levelNo++;
        int length = pfLevels.Length;
        if(levelNo >= length) {
            levelNo = 0;
        }
        ReloadTheLevel();
    }

    public void ReloadTheLevel() {
        GameLost = false;
        if(currentLevel != null) Destroy(currentLevel.gameObject);
        currentLevel = Instantiate(pfLevels[levelNo]);
    }

    public void QuitGame() {
         Application.Quit();
    }


}
