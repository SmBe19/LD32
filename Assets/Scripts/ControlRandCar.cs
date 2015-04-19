using UnityEngine;
using System.Collections;

public class ControlRandCar : MonoBehaviour {

	bool wasPressed;
	ControlRandCarController aControl;

	// Use this for initialization
	void Start () {
		wasPressed = false;
	}
	void findAndAttackRandCar(){
		RaycastHit hit;
		int layerMask = Physics.DefaultRaycastLayers;
		layerMask ^= 1 << 8;
		layerMask ^= 1 << 10;
		if (Physics.Raycast (transform.position + transform.TransformDirection(Vector3.forward) * 1, transform.TransformDirection (Vector3.forward), out hit, Mathf.Infinity, layerMask)) {
			if (hit.transform.tag == "Car") {
				aControl = hit.transform.GetComponent<ControlRandCarController> ();
				aControl.startControl ();
			}
			//Debug.Log(hit.transform.tag);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if ((aControl == null || !aControl.controlling) && Input.GetAxis ("ControlCar") > 0.5 && !wasPressed) {
			findAndAttackRandCar();
		}
		
		wasPressed = Input.GetAxis ("ControlCar") > 0.5;
	}
}
