using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour {
	public GameObject[] formations;
	public float waveDuration = 30f;
	public float wavetimer;
		
	private int currentWave = 0;
	
	// Use this for initialization
	void Start () {
		currentWave = 0;
		wavetimer = waveDuration;
		foreach(GameObject formation in formations){
			formation.SetActive (false);
		}
		formations[0].SetActive (true);
	
	}
	
	// Update is called once per frame
	void Update () {
		wavetimer -= Time.deltaTime;
	
		if(wavetimer <= 0){
			if(formations[currentWave].GetComponent<FormationController>().isBoss && !Score.beatBoss){
		}
			else{
				wavetimer = waveDuration;
				formations[currentWave].SetActive (false);
				print (formations.Length + "= formations.length" + currentWave + "= current wave");
				if(formations.Length > currentWave+1){
					currentWave ++;
				} else currentWave = 0;
				formations[currentWave].SetActive (true);
				print("Spawning wave " + currentWave);
			}
		}
		if(Score.beatBoss){
		LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel ("Win Screen");
		}
	}
}

