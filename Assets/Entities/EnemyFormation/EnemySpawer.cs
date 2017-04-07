﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawer : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 2f;

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

		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity);
			enemy.transform.parent = child;
		}
	}

	void OnDrawGizmos()
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
	}

}
