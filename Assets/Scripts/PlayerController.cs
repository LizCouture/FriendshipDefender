using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed = 12;
	public GameObject laserPrefab;
	public float projectileSpeed = 4f;
	public float projectileRepeatRate = 0.2f;
	float xmin;
	float xmax;
	public float padding = 1f;
	public float health = 5f;
	
	public AudioClip fireSound;
	public AudioClip damageSound;
	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint (new Vector3(0,0,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint (new Vector3(1,0,distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
	}
	
	// Update is called once per frame
	void Update () {
	if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)){
		transform.position += Vector3.left * speed * Time.deltaTime;
		}
	if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey (KeyCode.D)){
		transform.position += Vector3.right * speed * Time.deltaTime;
		}
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
		
	if(Input.GetKeyDown(KeyCode.Space)){
		InvokeRepeating ("Fire", 0.0001f, projectileRepeatRate);
		}
	if(Input.GetKeyUp(KeyCode.Space)){
		CancelInvoke("Fire");
		}
	}
	
	void Fire(){
		Vector3 startPosition =  new Vector3(transform.position.x,transform.position.y+.1f,transform.position.z);
		GameObject beam = Instantiate (laserPrefab, startPosition, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, projectileSpeed, 0);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}
	void OnTriggerEnter2D(Collider2D col){
		Projectile missile = col.gameObject.GetComponent<Projectile>();
		if(missile){
			health -= missile.getDamage();
			missile.Hit ();
			AudioSource.PlayClipAtPoint(damageSound, transform.position);
			if (health<= 0){
				Die ();
			}
		}
	}
	
	void Die(){
		LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		if (Score.beatBoss) man.LoadLevel ("Win Screen");
		else man.LoadLevel ("Lose Screen");
		Destroy(gameObject);
	}
}
