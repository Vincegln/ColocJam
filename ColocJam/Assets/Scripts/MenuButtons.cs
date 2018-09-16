using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour {
    
	public Button PlayButton;
	public Button ExitButton;
    
	// Use this for initialization
	private void Start () 
	{
		PlayButton.onClick.AddListener (PlayHandleClick);
		ExitButton.onClick.AddListener (ExitHandleClick);
		GameManager.Instance.CurrentLevel = 999;
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