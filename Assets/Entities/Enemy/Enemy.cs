using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public GameObject projectile;
	public float projectilSpeed = 10f;
	public float firingRate = 0.2f;
	public float health = 200f;
	public float shotsPerSeconds = 0.5f;
	public int points = 50;
	public AudioClip fireSound;
	public AudioClip destroySound;

	private ScoreKeeper scoreKeeper;

	void OnTriggerEnter2D (Collider2D collider)
	{
		Projectile missile = collider.gameObject.GetComponent<Projectile> ();
		if (missile) {
			health -= missile.GetDamage();
			missile.Hit ();
			if (health <= 0) {
				scoreKeeper.Score (points);
				AudioSource.PlayClipAtPoint(destroySound, transform.position);
				Destroy (gameObject);
			}
		}
	}

	void Start() 
	{
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
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
		GameObject beam = Instantiate (projectile, transform.position, Quaternion.identity);
		beam.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -projectilSpeed);
		AudioSource.PlayClipAtPoint (fireSound, transform.position);
	}


}
