using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    
	public Button PlayButton;
	public Button ExitButton;
	public Text Highscore;
    
	// Use this for initialization
	private void Start () 
	{
		PlayButton.onClick.AddListener (PlayHandleClick);
		ExitButton.onClick.AddListener (ExitHandleClick);
		GameManager.Instance.CurrentLevel = 999;
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