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
        Highscore.text = "Highscore :\r\n" + GameManager.Instance.ReadString();
    }

    public void PlayHandleClick()
    {
        GameManager.Instance.ChangeLevel();
    }

    public void ExitHandleClick()
    {
        Application.Quit();
    }
}