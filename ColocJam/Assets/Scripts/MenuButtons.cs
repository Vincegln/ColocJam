using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour {
    
	public Button PlayButton;
	public Button HighscoreButton;
	public Button CreditsButton;
	public Button ExitButton;
    
	// Use this for initialization
	private void Start () 
	{
		PlayButton.onClick.AddListener (PlayHandleClick);
		HighscoreButton.onClick.AddListener (HighscoreHandleClick);
		CreditsButton.onClick.AddListener (CreditsHandleClick);
		ExitButton.onClick.AddListener (ExitHandleClick);
		GameManager.Instance.CurrentLevel = 999;
	}

	public void PlayHandleClick()
	{
		GameManager.Instance.ChangeLevel();
	}
	
	public void HighscoreHandleClick()
	{
		//TODO : Things
	}

	public void CreditsHandleClick()
	{
		//TODO : Things
	}
	
	public void ExitHandleClick()
	{
		Application.Quit();
	}
}