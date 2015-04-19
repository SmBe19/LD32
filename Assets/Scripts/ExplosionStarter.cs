using UnityEngine;
using System.Collections;

public class ExplosionStarter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision col){
		if (!this.enabled) {
			return;
		}
		if (col.transform.tag == "Enemy") {
			if(!col.transform.GetComponent<ExplosionController> ().exploding){
				col.transform.GetComponent<ExplosionController> ().Explode ();
				if(GetComponent<ExplosionController>() == null){
					transform.parent.GetComponent<ExplosionController>().Explode();
				} else {
					GetComponent<ExplosionController> ().Explode ();
				}
			}
		}
	}
}
