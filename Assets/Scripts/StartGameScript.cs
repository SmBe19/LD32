﻿using UnityEngine;
using System.Collections;

public class StartGameScript : MonoBehaviour {

	public string nextLevel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClick(){
		Application.LoadLevel (nextLevel);
	}
}
