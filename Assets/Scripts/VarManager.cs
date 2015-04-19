﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VarManager : MonoBehaviour {

	public float EnemyCount = 0;
	public float LiveCount = 10;
	public Text livesLabel;
	public Text enemyLabel;

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
		livesLabel.text = "lives: " + LiveCount;
		enemyLabel.text = "enemies: " + EnemyCount;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
