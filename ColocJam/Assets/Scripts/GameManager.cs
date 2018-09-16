using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance = null;
    
    public int Score = 0;
    public int[] HighScores = new int[3];
    public int Lives = 0;
    
    public int CurrentLevel = 1;
    private List<int> _levelsAvailable = new List<int>();
    private List<int> _levelsDone = new List<int>();
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void OnStart()
    {
        for (var i = 1; i < 6; i++) { _levelsAvailable.Add(i);}
        for (var i = 1; i < 3; i++) { HighScores[i]=0;}

        Lives = 3;
    }
    
    public void IncreaseScore(int amount)
    {
        Score += amount;

        print("New Score : " + Score.ToString());
    }

    public void DecreaseLife()
    {
        if (Lives <= 0)
        {
            CheckHighscores();
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            Lives -= 1;
        }
    }

    private void CheckHighscores()
    {
        for (var i = 0; i < 3; i++)
        {
            if (Score <= HighScores[i]) continue;
            HighScores[i] = Score;
            break;
        }

        HighScores = HighScores.OrderByDescending(c => c).ToArray();
        
        print("New Highscore : " + Score);
    }

    public void Reset()
    {
        Score = 0;

        Lives = 3;
        
        ChangeLevel();
    }

    public void ChangeLevel()
    {
        _levelsDone.Add(CurrentLevel);
        var levelsToDo = _levelsAvailable.Except(_levelsDone).ToList();
        SceneManager.LoadScene("Level" + levelsToDo[Random.Range(0, levelsToDo.Count)]);
    }
}
