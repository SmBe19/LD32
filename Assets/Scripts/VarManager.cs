﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VarManager : MonoBehaviour {

	public string nextLevel;
	public float timeUntilNextLevel;
	public float EnemyCount = 0;
	public float LiveCount = 10;
	public Text livesLabel;
	public Text enemyLabel;
	public Text winLoseLabel;

	// Use this for initialization
	void Start () {
		updateText ();
	}

	public void heroHit(){
		LiveCount--;
		livesLabel.text = "lives: " + LiveCount;
		updateText ();
	}

	public void enemyDestroyed(){
		EnemyCount--;
		updateText ();
	}

	public void updateText(){
		if (LiveCount <= 0) {
			livesLabel.text = "you lose";
			winLoseLabel.text = "you lose";
		} else {
			livesLabel.text = "lives: " + LiveCount;
		}
		if (EnemyCount <= 0) {
			enemyLabel.text = "you win";
			winLoseLabel.text = "you win";
		} else {
			enemyLabel.text = "enemies: " + EnemyCount;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (LiveCount <= 0) {
			timeUntilNextLevel -= Time.deltaTime;
			if(timeUntilNextLevel <= 0){
				Application.LoadLevel(Application.loadedLevel);
			}
		}
		if (EnemyCount <= 0) {
			timeUntilNextLevel -= Time.deltaTime;
			if(timeUntilNextLevel <= 0){
				Application.LoadLevel(nextLevel);
			}
		}
	}
}
