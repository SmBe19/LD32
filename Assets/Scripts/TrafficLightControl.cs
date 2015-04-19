using UnityEngine;
using System.Collections;

public class TrafficLightControl : MonoBehaviour {

	public int aStatus;
	public float randomStart;
	public float startAfter;
	public GameObject LRed, LOrange, LGreen;
	public float[] timeUntilNext;

	float timeRemaining;

	// Use this for initialization
	void Start () {
		setStatus (aStatus);
		timeRemaining = Random.value * randomStart;
		timeRemaining += startAfter;
	}

	public void setStatus(int status){
		aStatus = status;
		LRed.SetActive((status & (1 << 0)) > 0); // 1
		LOrange.SetActive((status & (1 << 1)) > 0); // 2
		LGreen.SetActive((status & (1 << 2)) > 0); // 4

		timeRemaining = timeUntilNext [status];

		//Debug.Log (status);
	}

	public void setNextStatus(){
		aStatus &= 7;
		switch (aStatus) {
		case 0: // undef
		case 2: // O
		case 5: // RG
		case 6: // OG
		case 7: // ORG
			setStatus(1); // R
			break;
		case 1: // R
			setStatus (3); // RO
			break;
		case 3: // RO
			setStatus (4); // G
			break;
		case 4: // G
			setStatus(2); // O
			break;
		}
	}

	public void playParticleSystem(){
		GetComponent<ParticleSystem> ().Stop ();
		GetComponent<ParticleSystem> ().Play ();
	}
	
	// Update is called once per frame
	void Update () {
		timeRemaining -= Time.deltaTime;
		if (timeRemaining <= 0) {
			setNextStatus();
		}
	}
}
