using UnityEngine;
using System.Collections;

public class LoseController : MonoBehaviour {
	public VarManager varManager;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other){
		if (other.tag == "Enemy") {
			varManager.heroHit();
		}
	}
}
