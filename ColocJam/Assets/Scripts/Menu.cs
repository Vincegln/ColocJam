using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button PlayButton;
    public Button ExitButton;
    public Text Highscore;

    // Use this for initialization
    private void Start()
    {
        PlayButton.onClick.AddListener(PlayHandleClick);
        ExitButton.onClick.AddListener(ExitHandleClick);
        Highscore.text = "Highscore :\r\n" + ReadString();
    }

    public void PlayHandleClick()
    {
        GameManager.Instance.ChangeLevel();
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