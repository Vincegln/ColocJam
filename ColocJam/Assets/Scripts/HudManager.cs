using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
	public Text ScoreLabel;
	
	// Use this for initialization
	private void Start ()
	{
		Refresh();
	}

	public void Refresh()
	{
		ScoreLabel.text = "Score : " + GameManager.Instance.Score;
	}
}
