using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
	public Text ScoreLabel;
	public Button ExitButton;
	
	// Use this for initialization
	private void Start ()
	{
		Refresh();
		ExitButton.onClick.AddListener(ExitHandleClick);
	}

	public void Refresh()
	{
		ScoreLabel.text = "Score : " + Database.Score;
	}
	
	public void ExitHandleClick()
	{
		SceneManager.LoadScene("Menu");
	}
}
