using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {
	float maxHealth;
	float currentHealth;
	PlayerController player;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent <PlayerController>();
		maxHealth = player.health;
	}
	
	// Update is called once per frame
	void Update () {
		currentHealth = player.health;
		float newWidth = currentHealth/maxHealth;
		gameObject.transform.localScale = new Vector3 (newWidth, 1, 1);
	
	}
}
