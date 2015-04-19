using UnityEngine;
using System.Collections;

public class ControlRandCarController : MonoBehaviour {

	public bool controlling;
	bool wasPressed;

	// Use this for initialization
	void Start () {
		controlling = false;
		wasPressed = false;
	}

	public void startControl(){
		if (GetComponent<RandCarController> () == null || GetComponent<CarController> () == null) {
			return;
		}
		GetComponent<ParticleSystem> ().Play ();
		wasPressed = true;
		controlling = true;

		GetComponent<RandCarController> ().enabled = false;
		GetComponent<CarController> ().enabled = true;
		GetComponent<CarController> ().isSecondControl = true;
		foreach (ExplosionStarter es in GetComponentsInChildren<ExplosionStarter>(true)) {
			es.enabled = true;
		}
	}

	public void stopControl(){
		if (GetComponent<RandCarController> () == null || GetComponent<CarController> () == null) {
			return;
		}
		GetComponent<ParticleSystem> ().Stop ();
		controlling = false;
		
		GetComponent<RandCarController> ().enabled = true;
		GetComponent<RandCarController> ().ImLost ();
		GetComponent<CarController> ().enabled = false;
		GetComponent<ExplosionStarter> ().enabled = false;
		foreach (ExplosionStarter es in GetComponentsInChildren<ExplosionStarter>(true)) {
			es.enabled = false;
		}
	}

	// Update is called once per frame
	void Update () {
		if (controlling) {
			if (Input.GetAxis ("ControlCar") > 0.5 && !wasPressed) {
				stopControl ();
			}

			wasPressed = Input.GetAxis ("ControlCar") > 0.5;
		}
	}
}
