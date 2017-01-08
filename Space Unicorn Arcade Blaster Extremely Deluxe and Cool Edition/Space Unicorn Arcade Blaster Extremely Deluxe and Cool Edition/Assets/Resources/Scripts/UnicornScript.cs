using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UnicornScript : MonoBehaviour {

	public float UnicornSpeed = 0.5f;

	public Text Score;
	public Text PowerMeter;
	int score;
	int highscore;

	bool isInvincible = false;
	bool isSpeedy = false;

	public int invTime = 5;
	public int speedTime = 3;

	float countdown;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		score = 0;
		highscore = PlayerPrefs.GetInt ("Highscore");
	}
	
	void Update()
	{
		float hor = 0, ver = 0;
		float moveHorizontal = Input.acceleration.x;
		float moveVertical = Input.acceleration.z;
		if (moveHorizontal < 0.0f && transform.position.x > 0) {
			hor = -1.0f;
		} else if (moveHorizontal > 0.0f && transform.position.x < 100) {
			hor = 1.0f;
		} else {
			hor = 0;
		}

		if (moveVertical < 0.0f && transform.position.y > 0) {
			ver = -1.0f;
		} else if (moveVertical > 0.0f && transform.position.y < 80) {
			ver = 1.0f;
		} else {
			ver = 0;
		}

		transform.Translate (new Vector3 (hor*UnicornSpeed, ver*UnicornSpeed, 0));

		Score.text = "Score: "+ score +"m";
		score++;

		if (isInvincible) {
			rb.isKinematic = true;
			countdown -= Time.deltaTime;
			PowerMeter.text = "Invincibility: " + countdown + "s";
			if (countdown <= 0.0f) {
				PowerMeter.text = "";
				isInvincible = false;
			}
		} else {
			rb.isKinematic = false;
		}

		if (isSpeedy) {
			UnicornSpeed = 1.5f;
			countdown -= Time.deltaTime;
			PowerMeter.text = "Speed Boost: " + countdown + "s";
			if (countdown <= 0.0f) {
				PowerMeter.text = "";
				isSpeedy = false;
			}
		} else {
			UnicornSpeed = 0.5f;
		}
	}

	void OnCollisionEnter(Collision col){
		if (!isInvincible) {
			if (col.collider.name.Contains ("Ast")) {
				if (score > highscore) {
					highscore = score;
					PlayerPrefs.SetInt ("Highscore", highscore);
				}
				PlayerPrefs.SetInt ("Score", score);
				SceneManager.LoadScene ("GameOverScene", LoadSceneMode.Single);
			}
		}

		if (col.collider.name.Contains ("Sh")) {
			isInvincible = true;
			countdown = (float)invTime;
			col.gameObject.active = false;
		}

		if (col.collider.name.Contains ("Bo")) {
			isSpeedy = true;
			countdown = (float)speedTime;
			col.gameObject.active = false;
		}
	}
}
