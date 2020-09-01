using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : weapon {

	protected override void OnTriggerEnter2D (Collider2D other)
	{
		if(other.gameObject.CompareTag("Player")){
			other.gameObject.GetComponent<GamePlayer> ().hitDamage (damage);
			Destroy (this.gameObject);
		}
	}
}
