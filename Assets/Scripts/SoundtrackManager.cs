using UnityEngine;
using System.Collections;

public class SoundtrackManager : MonoBehaviour {

	private static SoundtrackManager instance = null;
	public static SoundtrackManager Instance  { get { return instance; } }

	// Use this for initialization
	void Start () {
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
