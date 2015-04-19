using UnityEngine;
using System.Collections;

public class ActivationController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void DeactivateChildren(GameObject g, bool a, bool amParent) {
		if (!amParent) {
			g.SetActive (a);
		}

		if (g.GetComponent<MeshRenderer> () != null) {
			g.GetComponent<MeshRenderer> ().enabled = a;
		}
		
		foreach (Transform child in g.transform) {
			DeactivateChildren(child.gameObject, a, false);
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Enemy" || other.tag == "Car") {
			DeactivateChildren(other.gameObject, true, true);
			//Debug.Log ("Activate");
		}
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Enemy" || other.tag == "Car") {
			DeactivateChildren(other.gameObject, false, true);
			//Debug.Log ("Deactivate");
		}
	}
}
