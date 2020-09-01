using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class Missile : weapon {
	public GameObject ExplosionRange;
	private int moveSpeed=3;
	public override void OnStartServer ()
	{
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 1) * moveSpeed;
	}
	protected override void OnTriggerEnter2D (Collider2D other)
	{
		if(other.gameObject.CompareTag("Enemy")){
			other.gameObject.GetComponent<EnemyObject> ().hitDamage (damage);
			if (isServer) {
				GameObject ins = Instantiate (ExplosionRange, transform.position, transform.rotation);
				NetworkServer.Spawn (ins);
			}
			Destroy (gameObject);
		}
	}
}
