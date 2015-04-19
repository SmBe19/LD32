using UnityEngine;
using System.Collections;

public class PauseController : MonoBehaviour {

	public Transform pauseMenu;
	float oldTimeScale;
	bool oldCursor;
	bool isPaused;
	bool wasPressed;

	// Use this for initialization
	void Start () {
		oldTimeScale = Time.timeScale;
		oldCursor = Cursor.visible;
		endPause ();
	}

	public void startPause(){
		oldTimeScale = Time.timeScale;
		oldCursor = Cursor.visible;
		Time.timeScale = 0f;
		foreach (Transform child in pauseMenu) {
			child.gameObject.SetActive(true);
		}
		Cursor.visible = true;
		isPaused = true;
	}

	public void endPause(){
		Time.timeScale = oldTimeScale;
		Cursor.visible = oldCursor;
		foreach (Transform child in pauseMenu) {
			child.gameObject.SetActive(false);
		}
		isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown ("escape") && !wasPressed){
			if(isPaused){
				endPause();
			} else {
				startPause();
			}
		}
		wasPressed = Input.GetKeyDown ("escape");
	}
}
