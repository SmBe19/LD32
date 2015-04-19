using UnityEngine;
using System.Collections;

public class AddTut : MonoBehaviour {

	public Transform TutE, TutQ, TutBad;
	public Transform cam;

	bool generated;

	// Use this for initialization
	void Start () {
		generated = false;
	}

	// Update is called once per frame
	void Update () {
		if (!generated) {
			Transform tut;
			foreach (GameObject go in GameObject.FindGameObjectsWithTag ("Car")) {
				tut = Instantiate(TutE) as Transform;
				tut.SetParent(go.transform, false);
				tut.localPosition = Vector3.up * 1;
				tut.GetComponent<LookAtController>().dest = cam;
			}

			foreach (GameObject go in GameObject.FindGameObjectsWithTag ("TrafficLight")) {
				tut = Instantiate(TutQ) as Transform;
				tut.SetParent(go.transform, false);
				tut.localPosition = Vector3.up * 1;
				tut.GetComponent<LookAtController>().dest = cam;
			}

			foreach (GameObject go in GameObject.FindGameObjectsWithTag ("Enemy")) {
				tut = Instantiate(TutBad) as Transform;
				tut.SetParent(go.transform, false);
				tut.localPosition = Vector3.up * 1;
				tut.GetComponent<LookAtController>().dest = cam;
			}

			generated = true;
		}
	}
}
