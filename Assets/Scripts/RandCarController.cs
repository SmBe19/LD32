using UnityEngine;
using System.Collections;

public class RandCarController : MonoBehaviour {

	public generateCity generator;
	protected Vector2 start;
	protected Vector2 size;
	protected Vector2 blockSize;
	protected Vector2 completeBlockSize;
	protected Vector2 houseSize;
	protected float streetWidth;

	public float maxVelocity;
	public float acceleration;
	public float torque;
	public float minVelocityRatio;
	public float breakDist;

	protected float aVelocity;
	protected Vector3 aDest, aStart, tmpDest;
	protected bool lastDirchange;
	protected Rigidbody rb;

	// Use this for initialization
	protected virtual void Start () {
		start = generator.start;
		size = generator.size;
		blockSize = generator.blockSize;
		houseSize = generator.houseSize;
		streetWidth = generator.streetWidth;

		completeBlockSize = new Vector2 ((blockSize.x + streetWidth) * houseSize.x, (blockSize.y + streetWidth) * houseSize.y);

		rb = GetComponent<Rigidbody> ();
		tmpDest = aDest = transform.position;

		lastDirchange = Random.value > 0.5f;
		aVelocity = 0;
	}
	protected void setNewDestPos(int row, int col, int newRow, int newCol){
		aDest = new Vector3(newCol * completeBlockSize.x + start.x,
	                         0,
	                         newRow * completeBlockSize.y + start.y);
		aStart = new Vector3(col * completeBlockSize.x + start.x,
		                     0,
		                     row * completeBlockSize.y + start.y);
		
		Vector3 Dest, Start;
		Dest = Start = Vector3.zero;
		
		Color debCol = Color.white;
		
		if(newRow > row){
			Start.x = (streetWidth * 0.25f) * houseSize.x;
			Start.z = (streetWidth * -0.25f) * houseSize.y;
			Dest.x = (streetWidth * 0.25f) * houseSize.x;
			Dest.z = (streetWidth * 1.25f) * houseSize.y;
			debCol = Color.red;
		} else if(newRow < row){
			Start.x = (streetWidth * 0.75f) * houseSize.x;
			Start.z = (streetWidth * 1.25f) * houseSize.y;
			Dest.x = (streetWidth * 0.75f) * houseSize.x;
			Dest.z = (streetWidth * -0.25f) * houseSize.y;
			debCol = Color.yellow;
		} else if(newCol > col){
			Start.x = (streetWidth * -0.25f) * houseSize.x;
			Start.z = (streetWidth * 0.75f) * houseSize.y;
			Dest.x = (streetWidth * 1.25f) * houseSize.x;
			Dest.z = (streetWidth * 0.75f) * houseSize.y;
			debCol = Color.green;
		} else if(newCol < col){
			Start.x = (streetWidth * 1.25f) * houseSize.x;
			Start.z = (streetWidth * 0.25f) * houseSize.y;
			Dest.x = (streetWidth * -0.25f) * houseSize.x;
			Dest.z = (streetWidth * 0.25f) * houseSize.y;
			debCol = Color.blue;
		}
		
		aStart -= Start;
		aDest -= Dest;
		
		//Debug.DrawLine(aStart, aDest, debCol, 1000f);
	}

	protected void getColRow(Vector3 pos, out int row, out int col){
		col = Mathf.RoundToInt((pos.x - start.x) / completeBlockSize.x);
		row = Mathf.RoundToInt((pos.z - start.y) / completeBlockSize.y);
	}

	protected virtual void setNewDest(){
		if (tmpDest == aDest) {
			int row, col, newRow, newCol;
			getColRow(transform.position, out row, out col);

			newRow = row;
			newCol = col;

			if (lastDirchange) {
				// horizontal
				newCol = Random.Range(1, (int)size.x);
			} else {
				// vertical
				newRow = Random.Range(1, (int)size.y);
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

	public void ImLost(){
		tmpDest = aDest = transform.position;
	}

	// Update is called once per frame
	protected virtual void Update () {
		Vector3 diff = tmpDest - transform.position;
		diff.y = 0;
		if (diff.magnitude > 1f) {
			aVelocity = Mathf.MoveTowards(aVelocity, maxVelocity, acceleration * Time.deltaTime);

			Quaternion oldRotation = transform.rotation;
			transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.LookRotation (diff, Vector3.up), torque * Time.deltaTime);
			if(Quaternion.Angle(oldRotation, transform.rotation) > torque * Time.deltaTime * 0.25f){
				aVelocity = maxVelocity * minVelocityRatio;
			}
			else if(diff.magnitude < breakDist){
				aVelocity = Mathf.Min(maxVelocity * (Mathf.Sin ((diff.magnitude / breakDist) * Mathf.PI / 2) + minVelocityRatio), aVelocity);
			}
			/*if(Quaternion.Angle(transform.rotation, Quaternion.LookRotation (diff, Vector3.up)) > 90){
				aVelocity *= -1;
				Debug.DrawRay(transform.position, Vector3.up * 100, Color.red, 10f);
				Debug.Log (diff);
				Debug.Log (Quaternion.Angle(transform.rotation, Quaternion.LookRotation (diff, Vector3.up)));
			}*/
			Vector3 dir = transform.TransformDirection(Vector3.forward);
			Vector3 newVelo = rb.velocity;
			newVelo.x = dir.x * aVelocity;
			newVelo.z = dir.z * aVelocity;
			rb.velocity = newVelo;
		} else {
			setNewDest();
		}

		//Debug.DrawLine (transform.position, tmpDest, Color.white, 0.1f);
	}
	
	void OnTriggerStay(Collider other){
		if (other.tag == "Enemy" || other.tag == "Car" || (other.tag == "Player" && tag != "Enemy")) {
			aVelocity = Mathf.Min (aVelocity, other.GetComponent<Rigidbody> ().velocity.magnitude);
			//Debug.Log (aVelocity);
		} else if (other.tag == "TrafficLight") {
			if((other.transform.parent.GetComponent<TrafficLightControl>().aStatus & 1) > 0){
				aVelocity = 0;
			}
			//Debug.Log("Traffic Light");
		}
	}

	void OnCollisionEnter(Collision col){
		if (col.transform.tag == "Car" || col.transform.tag == "Enemy") {
			aVelocity = maxVelocity;
		}
	}
}
