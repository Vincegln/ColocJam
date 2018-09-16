using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text Score;
    public Text Highscore;
    public Text NewHighScore;
    
    public Button RetryButton;
    public Button MenuButton;

    private const int COMBINAISONINIT = 1;
    private const float LEVELTIMEINIT = 10.0f;
    
    private void Start()
    {
        RetryButton.onClick.AddListener(RetryHandleClick);
        MenuButton.onClick.AddListener(MenuHandleClick);
        Score.text = "Score : " + Database.Score;
        CheckHighscores();
        Highscore.text = "Highscore :\r\n" + PlayerPrefs.GetInt ("highscore",0);
    }
    
    public void RetryHandleClick()
    {
        Database.Score = 0;
        Database.CombinaisonLength = COMBINAISONINIT;
        Database.LevelTime = LEVELTIMEINIT;
        Database.Lives = 3;
        SceneManager.LoadScene("Level1");
    }

    public void MenuHandleClick()
    {
        SceneManager.LoadScene("Menu");
    }
    
    private void CheckHighscores()
    {
        if (PlayerPrefs.GetInt ("highscore",0) < Database.Score)
        {
            NewHighScore.gameObject.SetActive(true);
            PlayerPrefs.SetInt ("highscore",Database.Score);
        }
    }
}