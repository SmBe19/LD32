using UnityEngine;
using System.Collections;

public class OverviewManager : MonoBehaviour {

	public float slowScale;
	public float cameraFar;
	public float animationDuration;
	public Transform cam;
	float cameraClose;
	float origScale, origFixed;
	bool wasPressed;
	bool isAnimation;
	Vector3 start, dest;
	float timePassed;

	// Use this for initialization
	void Start () {
		cameraClose = cam.position.y;
		origScale = Time.timeScale;
		origFixed = Time.fixedDeltaTime;
		wasPressed = false;
		isAnimation = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isAnimation) {
			timePassed += Time.deltaTime / Time.timeScale;
			cam.position = Vector3.Lerp(start, dest, timePassed / animationDuration);
			if(timePassed > animationDuration){
				isAnimation = false;
			}
		}

		if (Input.GetAxis ("Overview") > 0.5 && !wasPressed) {
			Time.timeScale = origScale * slowScale;
			Time.fixedDeltaTime = origFixed * slowScale;
			start = cam.position;
			dest = cam.position;
			dest.y = cameraFar;
			timePassed = 0;
			isAnimation = true;
		} else if (Input.GetAxis ("Overview") < 0.5 && wasPressed) {
			Time.timeScale = origScale;
			Time.fixedDeltaTime = origFixed;
			start = cam.position;
			dest = cam.position;
			dest.y = cameraClose;
			timePassed = 0;
			isAnimation = true;
		}

		wasPressed = Input.GetAxis ("Overview") > 0.5;
	}
}
