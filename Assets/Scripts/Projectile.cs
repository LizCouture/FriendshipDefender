using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float damage = 1f;
	public bool destructible = false;
	public int health = 0;
	public Explosion explosion = null;
	public AudioClip hitSound = null;
	
	public float getDamage(){
		return damage;
	}
	
	public void Hit(){
		if(explosion != null){
			Instantiate (explosion, transform.position, Quaternion.identity);
		}
		Destroy (gameObject);
	}
	
}
