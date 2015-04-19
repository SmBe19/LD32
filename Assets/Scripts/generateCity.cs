using UnityEngine;
using System.Collections;

public class generateCity : MonoBehaviour {

	public VarManager varManager;
	public Transform house;
	public Transform randCar;
	public Transform enemyCar;
	public Transform streetHorizontal, streetVertical, streetCross;
	public Transform trafficLight;
	public Transform hero;
	public Transform cam;
	public Vector2 heroStart;
	public Vector2 start;
	public Vector2 size;
	public Vector2 blockSize;
	public Vector2 houseSize;
	public Vector2 houseYOffset;
	public float streetWidth;
	public int carCount;
	public int enemyCount;
	public float streetHeight;
	public float trafficLightHeight;
	public float trafficLightCycle;

	Vector2 completeBlockSize;

	// Use this for initialization
	void Start () {
		completeBlockSize = new Vector2 ((blockSize.x + streetWidth) * houseSize.x, (blockSize.y + streetWidth) * houseSize.y);

		for (int y = 0; y < size.y; y++) {
			for(int x = 0; x < size.x; x++){
				for(int by = 0; by < blockSize.y; by++){
					for(int bx = 0; bx < blockSize.x; bx++){
						Instantiate (house, new Vector3(start.x + x * (completeBlockSize.x) + (bx + 0.5f) * houseSize.x,
						                                Random.value * (houseYOffset.y - houseYOffset.x) + houseYOffset.x,
						                                start.y + y * (completeBlockSize.y) + (by + 0.5f) * houseSize.y), Quaternion.identity);

						if(by == 0 && y > 0){
							Instantiate(streetHorizontal, new Vector3(start.x + x * (completeBlockSize.x) + (bx + 0.5f) * houseSize.x,
							                                          streetHeight,
							                                          start.y + y * (completeBlockSize.y) - 0.5f * streetWidth * houseSize.y), Quaternion.identity);
						}
					}
					if(x > 0){
						Instantiate(streetVertical, new Vector3(start.x + x * (completeBlockSize.x) - 0.5f * streetWidth * houseSize.x,
						                                        streetHeight,
						                                          start.y + y * (completeBlockSize.y) + (by + 0.5f) * houseSize.y), Quaternion.identity);
					}
				}
				if(x > 0 && y > 0){
					Instantiate(streetCross, new Vector3(start.x + x * (completeBlockSize.x) - 0.5f * streetWidth * houseSize.x,
					                                     streetHeight,
					                                     start.y + y * (completeBlockSize.y) - 0.5f * streetWidth * houseSize.y), Quaternion.identity);

					Vector3[] dir = {Vector3.forward, Vector3.left, Vector3.back, Vector3.right};
					float[] offX = {-0.25f, 0.75f, 1.25f, 0.25f};
					float[] offY = {0.25f, -0.25f, 0.75f, 1.25f};
					int startTraffic = Random.Range(0, 4);
					for(int i = 0; i < 4; i++){
						Transform go = Instantiate(trafficLight, new Vector3(start.x + x * (completeBlockSize.x) - offX[i] * streetWidth * houseSize.x,
						                                      trafficLightHeight,
						                                      start.y + y * (completeBlockSize.y) - offY[i] * streetWidth * houseSize.y),
						            Quaternion.LookRotation(dir[i], Vector3.up)) as Transform;
						go.GetComponent<TrafficLightControl>().startAfter = ((i + startTraffic) % 4) * trafficLightCycle;
					}
				}
			}
		}

		for (int i = 0; i < carCount; i++) {
			int col, row;
			col = Random.Range(1, (int)size.x);
			row = Random.Range(1, (int)size.y);

			Transform go = Instantiate(randCar, new Vector3(col * completeBlockSize.x - (streetWidth / 2) * houseSize.x + start.x,
			                                 0,
			                                 row * completeBlockSize.y - (streetWidth / 2) * houseSize.y + start.y), Quaternion.identity) as Transform;

			go.GetComponent<RandCarController>().generator = this;
			go.GetComponent<ExplosionController>().varManager = varManager;
			ActivationController.DeactivateChildren(go.gameObject, false, true);
		}
		
		for (int i = 0; i < enemyCount; i++) {
			int col, row;
			col = Random.Range(1, (int)size.x);
			row = Random.Range(1, (int)size.y);
			
			Transform go = Instantiate(enemyCar, new Vector3(col * completeBlockSize.x - (streetWidth / 2) * houseSize.x + start.x,
			                                 0,
			                                 row * completeBlockSize.y - (streetWidth / 2) * houseSize.y + start.y), Quaternion.identity) as Transform;

			go.GetComponent<EnemyCarController>().hero = hero;
			go.GetComponent<EnemyCarController>().generator = this;
			go.GetComponent<ExplosionController>().varManager = varManager;
			ActivationController.DeactivateChildren(go.gameObject, false, true);
		}

		hero.position = new Vector3 (start.x + heroStart.x * (completeBlockSize.x) - (streetWidth / 2) * houseSize.x,
		                            0,
		                             start.y + heroStart.y * (completeBlockSize.y) - (streetWidth / 2) * houseSize.y);

		if (cam != null) {
			cam.position = hero.position;
		}
		
		if (varManager != null) {
			varManager.EnemyCount += enemyCount;
			varManager.updateText ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
