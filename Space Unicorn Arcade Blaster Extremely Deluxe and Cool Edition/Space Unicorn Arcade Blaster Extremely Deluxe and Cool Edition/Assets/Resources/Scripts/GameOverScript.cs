using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour {

	int score;
	int highscore;

	public Text hs;
	public Text sc;

	// Use this for initialization
	void Start () {
		score = PlayerPrefs.GetInt ("Score");
		highscore = PlayerPrefs.GetInt ("Highscore");
	}
	
	// Update is called once per frame
	void Update () {
		hs.text = ""+highscore;
		sc.text = ""+score;
	}
}
