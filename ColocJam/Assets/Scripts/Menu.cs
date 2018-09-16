using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private const int COMBINAISONINIT = 1;
    private const float LEVELTIMEINIT = 10.0f;
    public Button PlayButton;
    public Button ExitButton;
    public Text Highscore;

    // Use this for initialization
    private void Start()
    {
        Database.CombinaisonLength = COMBINAISONINIT;
        Database.LevelTime = LEVELTIMEINIT;
        Database.Lives = 3;
        Database.SceneNumber = Random.Range(0,5);
        PlayButton.onClick.AddListener(PlayHandleClick);
        ExitButton.onClick.AddListener(ExitHandleClick);
        Highscore.text = "Highscore :\r\n" + ReadString();
    }

    public void PlayHandleClick()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ExitHandleClick()
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