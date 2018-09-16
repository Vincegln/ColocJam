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