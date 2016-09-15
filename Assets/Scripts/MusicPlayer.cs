using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	
	public AudioClip introClip;
	public AudioClip startClip;
	public AudioClip gameClip;
	
	private AudioSource music;
	
	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>();
			music.clip = introClip;
			music.loop = true;
			music.Play ();
		}
		
	}
	
	void OnLevelWasLoaded(int level){
		Debug.Log ("Loaded Level: " + level);
		if(level == 1){
			music.Stop ();
			//music.volume = .25f;
			music.clip = startClip;
			music.loop = true;
			music.Play ();
		} else if(level == 2){
			music.Stop ();
			music.clip = gameClip;
			//music.volume = .5f;
			Debug.Log ("playing music" + music.volume);
			music.Play();
		}
	}
}
