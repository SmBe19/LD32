using UnityEngine;
using System.Collections;

public class EnemyCarController : RandCarController {

	public Transform hero;
	public float checkEveryXs;
	public float followPos;
	float timePassed;
	
	// Use this for initialization
	protected override void Start () {
		base.Start ();
		timePassed = 0;
	}
	
	protected override void setNewDest(){
		if (tmpDest == aDest) {
			int row, col, hRow, hCol, newRow, newCol;
			getColRow(aDest, out row, out col);
			getColRow(hero.position, out hRow, out hCol);

			newRow = row;
			newCol = col;
			
			if (lastDirchange) {
				// horizontal
				newCol = hCol;
				if(newCol == col){
					newCol = Random.Range(1, (int)size.x);
				}
			} else {
				// vertical
				newRow = hRow;
				if(newRow == row){
					newRow = Random.Range(1, (int)size.y);
				}
			}
			lastDirchange = !lastDirchange;
			
			setNewDestPos(row, col, newRow, newCol);
			
			tmpDest = aStart;
		} else if (tmpDest == aStart){
			tmpDest = aDest;
		} else {
			tmpDest = aStart;
		}
	}

	void checkHeroSight(){
		int layerMask = Physics.DefaultRaycastLayers;
		layerMask ^= 1 << 8;
		layerMask ^= 1 << 9;
		layerMask ^= 1 << 10;
		RaycastHit hit;
		if (!Physics.Linecast(transform.position, hero.position, out hit, layerMask)) {
			if(Random.value < followPos){
				tmpDest = hero.position;
			}
			//Debug.Log ("Found him");
		}
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();

		timePassed += Time.deltaTime;
		if (timePassed > checkEveryXs) {
			timePassed -= checkEveryXs;
			checkHeroSight ();
		}
	}
}
