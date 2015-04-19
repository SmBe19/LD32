using UnityEngine;
using System.Collections;

public class ChangeTrafficLightController : MonoBehaviour {

	bool wasPressed;

	// Use this for initialization
	void Start () {
		wasPressed = false;
	}

	void findAndAttackTrafficLight(){
		RaycastHit hit;
		int layerMask = Physics.DefaultRaycastLayers;
		layerMask ^= 1 << 9;
		layerMask ^= 1 << 10;
		if (Physics.Raycast (transform.position + transform.TransformDirection(Vector3.forward) * 1, transform.TransformDirection (Vector3.forward), out hit, Mathf.Infinity, layerMask)) {
			if (hit.transform.tag == "TrafficLight") {
				hit.transform.parent.GetComponent<TrafficLightControl> ().setNextStatus ();
				hit.transform.parent.GetComponent<TrafficLightControl> ().playParticleSystem ();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("TrafficLight") > 0.5 && !wasPressed) {
			findAndAttackTrafficLight();
		}

		wasPressed = Input.GetAxis ("TrafficLight") > 0.5;
	}
}
