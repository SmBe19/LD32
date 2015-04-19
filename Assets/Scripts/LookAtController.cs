using UnityEngine;
using System.Collections;

public class LookAtController : MonoBehaviour {

	public Transform dest;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (2*transform.position - dest.position);
	}
}
