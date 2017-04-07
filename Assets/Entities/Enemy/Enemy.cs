using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public GameObject projectile;
	public float projectilSpeed = 5f;
	public float firingRate = 0.2f;

	private float health = 200f;

	void OnTriggerEnter2D (Collider2D collider)
	{
		Projectile missile = collider.gameObject.GetComponent<Projectile> ();
		if (missile) {
			health -= missile.GetDamage();
			missile.Hit ();
			if (health <= 0) {
//				Destroy (gameObject);
			}
		}
	}

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating ("Fire", 0.000001f, firingRate);
		} else if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke("Fire");
		}
	}

	void Fire() 
	{
		GameObject beam = Instantiate (projectile, transform.position, Quaternion.identity);
		beam.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, projectilSpeed, 0);
	}
}
