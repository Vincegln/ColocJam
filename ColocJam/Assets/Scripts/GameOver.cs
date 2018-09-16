using System.IO;
using UnityEngine;
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
        Score.text = "Score : " + GameManager.Instance.Score;
        Highscore.text = "Highscore :\r\n" + ReadString();
    }
    
    public void RetryHandleClick()
    {
        GameManager.Instance.Score = 0;
        GameManager.Instance.ChangeLevel();
    }

    public void MenuHandleClick()
    {
        Application.Quit();
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