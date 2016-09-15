using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public float health = 2;
	public GameObject laserPrefab;
	public float projectileSpeed = 4f;
	public float shotsPerSecond = 0.5f;
	public int scoreValue = 150;
	public AudioClip fireSound;
	public AudioClip damageSound;
	public AudioClip hitSound;
	public GameObject explosion;
	public int dieSoundPercent = 20;
	public bool isBoss;
	
	private Score scoreKeeper;
	
	// Use this for initialization
	void Start () {
		scoreKeeper = GameObject.FindObjectOfType<Score>();
	}
	
	// Update is called once per frame
	void Update () {
		float probability = shotsPerSecond * Time.deltaTime;
		if(Random.value < probability){
		Fire();
		}
	}
	void OnTriggerEnter2D(Collider2D col){
		if(hitSound != null) {
			AudioSource.PlayClipAtPoint (hitSound, transform.position);
			if(explosion != null){
				Instantiate (explosion, transform.position, Quaternion.identity);
			}
		}
		Projectile missile = col.gameObject.GetComponent<Projectile>();
		if(missile){
			health -= missile.getDamage();
			missile.Hit ();
			if (health<= 0){
				Die ();
			}
		}
		//Debug.Log (col);
	}
	
	void Die(){
		if(damageSound != null){
			if(Random.Range (0,100)<=dieSoundPercent){
				print(Random.Range (0,100));
				AudioSource.PlayClipAtPoint(damageSound, transform.position);
			}
		}
		scoreKeeper.addPoints(scoreValue);
		if(explosion != null){
			Instantiate (explosion, transform.position, Quaternion.identity);
		}
		if (isBoss) Score.beatBoss = true;
		Destroy(gameObject);
	}
	
	void Fire(){
		Vector3 startPosition =  new Vector3(transform.position.x,transform.position.y-.1f,transform.position.z);
		GameObject beam = Instantiate (laserPrefab, startPosition, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, -projectileSpeed, 0);
		if (fireSound != null) AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}
}
