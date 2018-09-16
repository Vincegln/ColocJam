﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public int Score = 0;
    public int HighScore = 0;

    public int Lives = 0;
	private int _currentCombinaisonLength;
	private float _levelTime;
	private float _levelTotalTime;
	private float _distanceEachDecrease;
	public float _timerDecrease = 0.03f;

    public int CurrentLevel = 999;
    private List<int> _levelsAvailable = new List<int>();
    private List<int> _levelsDone = new List<int>();

    // --------

    private const int NUMBEROFKEY = 55; // nb max de touches

    private Dictionary<int, KeyCode> _keyCodesDictionnary;
    private Dictionary<KeyCode, string> _keyNamesDictionnary;

    private KeyCode[] _keyCombinaison; // A remplacer par la combinaison de touches
    private string[] _combinaisonNames; //A remplacer par le tableau des noms composant la combinaison

    private int _currentKey; //Position touche attendue
    private KeyCode _waitedKey; // code touche attendue
	
	public List<Image> scenes = new List<Image>();

	public Image fond;

	public int CurrentScene;

	private int _correctKey; // booléen de merde pour savoir si on s'est trompé ou non
    private int _countingDown; // booléen de merde 2 pour savoir si le temps est écoulé ou non

	public GameObject[] Inputs;

	public GameObject Timer;
    // ----------

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _keyCodesDictionnary = new Dictionary<int, KeyCode>();
            _keyNamesDictionnary = new Dictionary<KeyCode, string>();
            FillKeyCodesDictionnary();
            FillKeyNamesDictionnary();
            _currentCombinaisonLength = 1;
            _levelTotalTime = 10.0f;
            _levelTime = 0.0f;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        for (var i = 1; i < 6; i++) { _levelsAvailable.Add(i);}
        HighScore = int.Parse(ReadString());
        Lives = 3;
	    _distanceEachDecrease = (36.0f * _timerDecrease) / _levelTotalTime;
	    GenerateCombinaison(NUMBEROFKEY, _currentCombinaisonLength);
	    _currentKey = 0;
	    string letterDisplayed;
	    for (int i = 0; i < Inputs.Length ; i++)
	    {
		    letterDisplayed = "";
		    if (i < _keyCombinaison.Length)
		    {
			    letterDisplayed = _combinaisonNames[i];
			    Inputs[i].SetActive(true);
			    //TODO : Reset la source de l'image de chaque composant Input
			    Inputs[i].transform.Find("Text").GetComponent<Text>().text = letterDisplayed;
		    }
		    else
		    {
			    Inputs[i].SetActive(false);
		    }
	    }
	    
	    _countingDown = 1;
		StartCoroutine(CountDown(_levelTotalTime));
    }

    
	private void Update()
	{
		if (_currentKey < _currentCombinaisonLength && _countingDown == 1)
		{
			if (_waitedKey == 0)
			{
				_waitedKey = _keyCombinaison[_currentKey];
				Debug.Log(_waitedKey.ToString());
			
			}
			if (Input.anyKeyDown)
			{
				if (Input.GetKeyDown(_waitedKey))
				{
					_correctKey = 1;
					StartCoroutine(KeyPressing());
				}
				else
				{
					_correctKey = 2;
					StartCoroutine(KeyPressing());
				}
			}
		}
		else
		{
			if (_countingDown == 1)
			{
				_countingDown = 0;
				StopAllCoroutines();
				//TODO : Indicateur Succès niveau
				Debug.Log("Congrats, good end");
			}
			
			
			//TODO : gestion changement niveau + changement difficulté
		}
			
	}
	
    public void IncreaseScore(int amount)
    {
        Score += amount;

        print("New Score : " + Score.ToString());
    }

    public void DecreaseLife()
    {
        if (Lives <= 0)
        {
            CheckHighscores();
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            Lives -= 1;
        }
    }

    private void CheckHighscores()
    {
        if (HighScore < Score)
        {
            HighScore = Score;
        }

        WriteString(HighScore.ToString());

        print("New Highscore : " + Score);
    }

    public void Reset()
    {
        Score = 0;

        Lives = 3;

        ChangeLevel();
    }

    public void ChangeLevel()
    {
        _levelsDone.Add(CurrentLevel);
        var levelsToDo = _levelsAvailable.Except(_levelsDone).ToList();
        CurrentLevel = levelsToDo[Random.Range(0, levelsToDo.Count)];
        SceneManager.LoadScene("Level" + CurrentLevel);
    }

    private void FillKeyCodesDictionnary()
    {
        _keyCodesDictionnary.Add(1, KeyCode.A);
        _keyCodesDictionnary.Add(2, KeyCode.B);
        _keyCodesDictionnary.Add(3, KeyCode.C);
        _keyCodesDictionnary.Add(4, KeyCode.D);
        _keyCodesDictionnary.Add(5, KeyCode.E);
        _keyCodesDictionnary.Add(6, KeyCode.F);
        _keyCodesDictionnary.Add(7, KeyCode.G);
        _keyCodesDictionnary.Add(8, KeyCode.H);
        _keyCodesDictionnary.Add(9, KeyCode.I);
        _keyCodesDictionnary.Add(10, KeyCode.J);
        _keyCodesDictionnary.Add(11, KeyCode.K);
        _keyCodesDictionnary.Add(12, KeyCode.L);
        _keyCodesDictionnary.Add(13, KeyCode.M);
        _keyCodesDictionnary.Add(14, KeyCode.N);
        _keyCodesDictionnary.Add(15, KeyCode.O);
        _keyCodesDictionnary.Add(16, KeyCode.P);
        _keyCodesDictionnary.Add(17, KeyCode.Q);
        _keyCodesDictionnary.Add(18, KeyCode.R);
        _keyCodesDictionnary.Add(19, KeyCode.S);
        _keyCodesDictionnary.Add(20, KeyCode.T);
        _keyCodesDictionnary.Add(21, KeyCode.U);
        _keyCodesDictionnary.Add(22, KeyCode.V);
        _keyCodesDictionnary.Add(23, KeyCode.W);
        _keyCodesDictionnary.Add(24, KeyCode.X);
        _keyCodesDictionnary.Add(25, KeyCode.Y);
        _keyCodesDictionnary.Add(26, KeyCode.Z);
        _keyCodesDictionnary.Add(27, KeyCode.Alpha0);
        _keyCodesDictionnary.Add(28, KeyCode.Alpha1);
        _keyCodesDictionnary.Add(29, KeyCode.Alpha2);
        _keyCodesDictionnary.Add(30, KeyCode.Alpha3);
        _keyCodesDictionnary.Add(31, KeyCode.Alpha4);
        _keyCodesDictionnary.Add(32, KeyCode.Alpha5);
        _keyCodesDictionnary.Add(33, KeyCode.Alpha6);
        _keyCodesDictionnary.Add(34, KeyCode.Alpha7);
        _keyCodesDictionnary.Add(35, KeyCode.Alpha8);
        _keyCodesDictionnary.Add(36, KeyCode.Alpha9);
        _keyCodesDictionnary.Add(37, KeyCode.F1);
        _keyCodesDictionnary.Add(38, KeyCode.F2);
        _keyCodesDictionnary.Add(39, KeyCode.F3);
        _keyCodesDictionnary.Add(40, KeyCode.F4);
        _keyCodesDictionnary.Add(41, KeyCode.F5);
        _keyCodesDictionnary.Add(42, KeyCode.F6);
        _keyCodesDictionnary.Add(43, KeyCode.F7);
        _keyCodesDictionnary.Add(44, KeyCode.F8);
        _keyCodesDictionnary.Add(45, KeyCode.F9);
        _keyCodesDictionnary.Add(46, KeyCode.F10);
        _keyCodesDictionnary.Add(47, KeyCode.F11);
        _keyCodesDictionnary.Add(48, KeyCode.F12);
        _keyCodesDictionnary.Add(49, KeyCode.Space);
        _keyCodesDictionnary.Add(50, KeyCode.Backspace);
        _keyCodesDictionnary.Add(51, KeyCode.Return);
        _keyCodesDictionnary.Add(52, KeyCode.RightArrow);
        _keyCodesDictionnary.Add(53, KeyCode.LeftArrow);
        _keyCodesDictionnary.Add(54, KeyCode.UpArrow);
        _keyCodesDictionnary.Add(55, KeyCode.DownArrow);
    }

    private void FillKeyNamesDictionnary()
    {
        _keyNamesDictionnary.Add(KeyCode.A, "A");
        _keyNamesDictionnary.Add(KeyCode.B, "B");
        _keyNamesDictionnary.Add(KeyCode.C, "C");
        _keyNamesDictionnary.Add(KeyCode.D, "D");
        _keyNamesDictionnary.Add(KeyCode.E, "E");
        _keyNamesDictionnary.Add(KeyCode.F, "F");
        _keyNamesDictionnary.Add(KeyCode.G, "G");
        _keyNamesDictionnary.Add(KeyCode.H, "H");
        _keyNamesDictionnary.Add(KeyCode.I, "I");
        _keyNamesDictionnary.Add(KeyCode.J, "J");
        _keyNamesDictionnary.Add(KeyCode.K, "K");
        _keyNamesDictionnary.Add(KeyCode.L, "L");
        _keyNamesDictionnary.Add(KeyCode.M, "M");
        _keyNamesDictionnary.Add(KeyCode.N, "N");
        _keyNamesDictionnary.Add(KeyCode.O, "O");
        _keyNamesDictionnary.Add(KeyCode.P, "P");
        _keyNamesDictionnary.Add(KeyCode.Q, "Q");
        _keyNamesDictionnary.Add(KeyCode.R, "R");
        _keyNamesDictionnary.Add(KeyCode.S, "S");
        _keyNamesDictionnary.Add(KeyCode.T, "T");
        _keyNamesDictionnary.Add(KeyCode.U, "U");
        _keyNamesDictionnary.Add(KeyCode.V, "V");
        _keyNamesDictionnary.Add(KeyCode.W, "W");
        _keyNamesDictionnary.Add(KeyCode.X, "X");
        _keyNamesDictionnary.Add(KeyCode.Y, "Y");
        _keyNamesDictionnary.Add(KeyCode.Z, "Z");
        _keyNamesDictionnary.Add(KeyCode.Alpha0, "0");
        _keyNamesDictionnary.Add(KeyCode.Alpha1, "1");
        _keyNamesDictionnary.Add(KeyCode.Alpha2, "2");
        _keyNamesDictionnary.Add(KeyCode.Alpha3, "3");
        _keyNamesDictionnary.Add(KeyCode.Alpha4, "4");
        _keyNamesDictionnary.Add(KeyCode.Alpha5, "5");
        _keyNamesDictionnary.Add(KeyCode.Alpha6, "6");
        _keyNamesDictionnary.Add(KeyCode.Alpha7, "7");
        _keyNamesDictionnary.Add(KeyCode.Alpha8, "8");
        _keyNamesDictionnary.Add(KeyCode.Alpha9, "9");
        _keyNamesDictionnary.Add(KeyCode.F1, "F1");
        _keyNamesDictionnary.Add(KeyCode.F2, "F2");
        _keyNamesDictionnary.Add(KeyCode.F3, "F3");
        _keyNamesDictionnary.Add(KeyCode.F4, "F4");
        _keyNamesDictionnary.Add(KeyCode.F5, "F5");
        _keyNamesDictionnary.Add(KeyCode.F6, "F6");
        _keyNamesDictionnary.Add(KeyCode.F7, "F7");
        _keyNamesDictionnary.Add(KeyCode.F8, "F8");
        _keyNamesDictionnary.Add(KeyCode.F9, "F9");
        _keyNamesDictionnary.Add(KeyCode.F10, "F10");
        _keyNamesDictionnary.Add(KeyCode.F11, "F11");
        _keyNamesDictionnary.Add(KeyCode.F12, "F12");
        _keyNamesDictionnary.Add(KeyCode.Space, "␣");
        _keyNamesDictionnary.Add(KeyCode.Backspace, "Z");
        _keyNamesDictionnary.Add(KeyCode.Return, "↵");
        _keyNamesDictionnary.Add(KeyCode.RightArrow, "→");
        _keyNamesDictionnary.Add(KeyCode.LeftArrow, "←");
        _keyNamesDictionnary.Add(KeyCode.UpArrow, "↑");
        _keyNamesDictionnary.Add(KeyCode.DownArrow, "↓");
    }

    private bool GetKeyCode(int code, int element)
    {
        return _keyCodesDictionnary.TryGetValue(code, out _keyCombinaison[element]);
    }

    private bool GetKeyName(KeyCode key, int element)
    {
        return _keyNamesDictionnary.TryGetValue(key, out _combinaisonNames[element]);
    }

    private bool GenerateCombinaison(int nbChar, int combinaisonLength)
    {
        _keyCombinaison = new KeyCode[combinaisonLength];
        _combinaisonNames = new string[combinaisonLength];
        int qteGen;
        var result = true;
        for (var i = 0; i < combinaisonLength; i++)
        {
            qteGen = Random.Range(1, nbChar + 1);
            result = GetKeyCode(qteGen, i);
            result = GetKeyName(_keyCombinaison[i], i);
            if (!result)
            {
                break;
            }
        }

        return result;
    }

// -------- Coroutines --------

    private IEnumerator KeyPressing()
    {
        if (_correctKey == 1)
        {
            _correctKey = 0;
            //TODO : Mettre à jour la source image avec touche validée
            yield return new WaitForSeconds(0.01f);
            _waitedKey = 0;
            _currentKey++;
        }
        else if (_correctKey == 2)
        {
            _correctKey = 0;
            //TODO : Mettre à jour la source image avec touche Fail
            //TODO : Mettre en place le lancement d'un son d'échec de frappe
	        GetComponent<AudioSource>().Play(); //joue le son associé à l’objet
            yield return new WaitForSeconds(0.1f);
            _waitedKey = 0;
//			_currentKey++; TODO : Adapter au mode de jeu sélectionné
		}
	}
	
	private IEnumerator CountDown(float timeToWaitIntoScene)
	{
		if (_countingDown == 1)
		{
			_levelTime = timeToWaitIntoScene;
			while (_levelTime > 0.0f)
			{
				_levelTime -= _timerDecrease;
				Timer.transform.position += new Vector3(_distanceEachDecrease,0,0);
				Debug.Log(_levelTime);
				yield return new WaitForSeconds(_timerDecrease);
			}
			_countingDown = 2;
			_levelTime = 0.0f;
			//TODO : Indicateur d'échec de niveau
			Debug.Log("You noob, time's up end");
			yield return new WaitForSeconds(0.01f);
			_correctKey = 0;
			_waitedKey = 0;
		}
	}
	
	void WriteString(string lHighscore)
	{
		const string path = "Assets/Resources/highscore.txt";

		//Write some text to the test.txt file
		var writer = new StreamWriter(path, true);
		writer.WriteLine(lHighscore);
		writer.Close();

		//Re-import the file to update the reference in the editor
		AssetDatabase.ImportAsset(path); 
		var asset = (TextAsset) Resources.Load("highscore");

		//Print the text from the file
		Debug.Log(asset.text);
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