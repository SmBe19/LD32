using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {

	public float acceleration;
	public float maxVelocity;
	public float torque;
	public bool isSecondControl = false;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (rb.velocity.magnitude < maxVelocity) {
			rb.AddRelativeForce (Vector3.forward * Input.GetAxis (isSecondControl ? "Vertical2" : "Vertical") * acceleration * Time.deltaTime);
		}
		if (Input.GetAxis (isSecondControl ? "Horizontal2" : "Horizontal") != 0 && Mathf.Abs(transform.InverseTransformDirection(rb.velocity).z) >= 0.0f) {
			transform.Rotate(Vector3.up, Input.GetAxis(isSecondControl ? "Horizontal2" : "Horizontal") * torque * (transform.InverseTransformDirection(rb.velocity).z < -0.1f ? -1 : 1) * Time.deltaTime);

			Vector3 newVelocity = rb.velocity;
			newVelocity.y = 0;
			newVelocity = newVelocity.magnitude * transform.TransformDirection(Vector3.forward).normalized * (transform.InverseTransformDirection (rb.velocity).z < 0 ? -1 : 1);
			newVelocity.y = rb.velocity.y;
			rb.velocity = newVelocity;
		}
	}
}
