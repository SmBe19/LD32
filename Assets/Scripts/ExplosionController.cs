using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour {

	public VarManager varManager;
	public float ExplosionDuration;
	public bool exploding;
	public AudioSource explosionSound;
	float explosionProgress;
	Vector3 startScale;

	// Use this for initialization
	void Start () {
	
	}

	public void Explode(){
		if (exploding) {
			return;
		}
		Destroy (gameObject, ExplosionDuration);
		exploding = true;
		explosionProgress = 0;
		startScale = transform.localScale;
		if (tag == "Enemy") {
			varManager.enemyDestroyed ();
		}

		if (explosionSound != null) {
			explosionSound.Play ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (exploding) {
			explosionProgress += Time.deltaTime;
			transform.localScale = Vector3.Lerp(startScale, Vector3.zero, explosionProgress / ExplosionDuration);
		}
	}
}
