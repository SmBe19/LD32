using UnityEngine;
using System.Collections;

public class PlaySirene : MonoBehaviour {

	public AudioSource sirene;
	public float possibility;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!sirene.isPlaying) {
			if (Random.value < possibility) {
				sirene.Play();
			}
		}
	}
}
