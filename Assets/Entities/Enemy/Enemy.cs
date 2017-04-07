using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public GameObject projectile;
	public float projectilSpeed = 10f;
	public float firingRate = 0.2f;
	public float health = 200f;
	public float shotsPerSeconds = 0.5f;

	void OnTriggerEnter2D (Collider2D collider)
	{
		Projectile missile = collider.gameObject.GetComponent<Projectile> ();
		if (missile) {
			health -= missile.GetDamage();
			missile.Hit ();
			if (health <= 0) {
				Destroy (gameObject);
			}
		}
	}

	void Update () 
	{
		float probability = Time.deltaTime * shotsPerSeconds;
		if (Random.value < probability) {
			Fire ();
		}
	}

	void Fire() 
	{
		Vector3 startPosition = transform.position + new Vector3 (0, -1, 0);
		GameObject beam = Instantiate (projectile, startPosition, Quaternion.identity);
		beam.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -projectilSpeed);
	}
}
