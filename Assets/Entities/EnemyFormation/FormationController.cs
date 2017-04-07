using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 2f;
	public float spawnDelay = 0.5f;

	private bool movingRight;
	private float xmin;
	private float xmax;

	void Start () 
	{
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint (new Vector3(0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint (new Vector3(1, 0, distance));
		xmin = leftmost.x;
		xmax = rightmost.x;

		SpawnUntilFull ();
	}
		
	void OnDrawGizmos ()
	{
		Gizmos.DrawWireCube (transform.position, new Vector3 (width, height));
	}

	void Update () 
	{
		if (movingRight) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		if (transform.position.x + width / 2 > xmax) {
			movingRight = false;
		} else if (transform.position.x - width / 2 < xmin) {
			movingRight = true;
		}

		if (AllMembersDead ()) {
			SpawnUntilFull ();
		}
	}

	void SpawnEnemies () 
	{
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity);
			enemy.transform.parent = child;
		}
	}

	void SpawnUntilFull () 
	{
		Transform nextPosition = NextFreePosition ();
		if (nextPosition) {
			GameObject enemy = Instantiate (enemyPrefab, nextPosition.position, Quaternion.identity);
			enemy.transform.parent = nextPosition;	
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}

	Transform NextFreePosition()
	{
		foreach (Transform position in transform) {
			if (position.childCount == 0) {
				return position;
			}
		}
		return null;
	}

	bool AllMembersDead() 
	{
		foreach (Transform position in transform) {
			if (position.childCount > 0) {
				return false;
			}
		}
		return true;
	}
}
