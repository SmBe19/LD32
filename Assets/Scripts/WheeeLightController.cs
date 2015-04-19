using UnityEngine;
using System.Collections;

public class WheeeLightController : MonoBehaviour {
	public float flashLength;
	public Light[] lights;
	public Color[] colors;
	float timePassed;
	int aColor;

	// Use this for initialization
	void Start () {
		aColor = 0;
		setColors(aColor);
		timePassed = Random.value * 10;
	}

	void setColors(int col){
		for(int i = 0; i < lights.Length; i++){
			lights[i].color = colors[(i + col) % colors.Length];
		}
	}

	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;

		if (timePassed > flashLength) {
			timePassed -= flashLength;
			aColor++;
			aColor %= lights.Length * colors.Length;
			setColors(aColor);
		}
	}
}
