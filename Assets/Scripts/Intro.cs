using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Intro : MonoBehaviour {
	public Sprite[] images;
	private int current;
	
	// Use this for initialization
	void Start () {
	current = 0;
	gameObject.GetComponent<Image>().sprite = images[current];
	
	}
	
	// Update is called once per frame
	void Update () {
	if(Input.anyKeyDown){
		if(images.Length > current+1){
			current++;
			gameObject.GetComponent<Image>().sprite = images[current];
		} 
		else {
			LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
			man.LoadLevel ("Start Menu");
		}
	}
	
	}
}
