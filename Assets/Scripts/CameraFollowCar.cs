using UnityEngine;
using System.Collections;

public class CameraFollowCar : MonoBehaviour {

	public GameObject destination;
	public float minDistance;
	public float addForward;
	public float maxDelta;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos;
		Vector3 dvelo, dpos;
		dvelo = destination.transform.InverseTransformDirection(destination.GetComponent<Rigidbody> ().velocity);
		dvelo.z = Mathf.Abs (dvelo.z);
		dvelo.z += addForward;
		dvelo = destination.transform.TransformDirection (dvelo);
		dpos = destination.transform.position;

		newPos = dpos - dvelo.normalized * minDistance;

		newPos.y = transform.position.y;
		//transform.position = newPos;
		transform.position = Vector3.MoveTowards (transform.position, newPos, maxDelta);
		transform.LookAt (destination.transform.position);
	}
}
