using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text Score;
    public Text Highscore;
    
    public Button RetryButton;
    public Button MenuButton;

    private const int COMBINAISONINIT = 1;
    private const float LEVELTIMEINIT = 10.0f;
    
    private void Start()
    {
        RetryButton.onClick.AddListener(RetryHandleClick);
        MenuButton.onClick.AddListener(MenuHandleClick);
        Score.text = "Score : " + Database.Score;
        Highscore.text = "Highscore :\r\n" + ReadString();
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
    
    public string ReadString()
    {
        const string path = "Assets/Resources/highscore.txt";

        //Read the text from directly from the test.txt file
        var reader = new StreamReader(path);
        var highscore = reader.ReadToEnd();
        Debug.Log(highscore);
        reader.Close();
        return highscore;
    }
}