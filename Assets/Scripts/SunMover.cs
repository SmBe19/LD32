using UnityEngine;
using System.Collections;

public class SunMover : MonoBehaviour {

	public float velocity;
	public float UDMag, UDVelo;
	public float maxIntensity;
	float usedTime;

	// Use this for initialization
	void Start () {
		usedTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		usedTime += Time.deltaTime;
		transform.LookAt (transform.position + new Vector3 (Mathf.Sin (usedTime * velocity), Mathf.Cos (usedTime * UDVelo) * UDMag,  Mathf.Cos (usedTime * velocity)));
		GetComponent<Light> ().intensity = (-Mathf.Cos (usedTime * UDVelo) + 0.5f) / 1.5f * maxIntensity;
	}
}
