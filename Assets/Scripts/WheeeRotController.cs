using UnityEngine;
using System.Collections;

public class WheeeRotController : MonoBehaviour {
	public float rotLength;
	public Transform[] lights;
	float timePassed;

	// Use this for initialization
	void Start () {
		setRot (0);
		timePassed = Random.value * 10;
	}

	void setRot(float tp){
		float diff = rotLength / lights.Length;
		for(int i = 0; i < lights.Length; i++){
			lights[i].LookAt(lights[i].position + new Vector3(Mathf.Sin((tp + diff) / rotLength * Mathf.PI * 2), 0, Mathf.Cos((tp + diff) / rotLength * Mathf.PI * 2)));
		}
	}

	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;

		if (timePassed > rotLength) {
			timePassed -= rotLength;
		}
		setRot (timePassed);
	}
}
