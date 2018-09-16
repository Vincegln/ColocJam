using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text Score;
    public Text Highscore;

    private void Start()
    {
        Score.text = "Score : " + GameManager.Instance.Score;
        Highscore.text = "Highscore :\r\n" + GameManager.Instance.HighScore;
    }
}