using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {
	public static int score = 0;
	public static bool beatBoss = false;
	private Text myText;
	
	void Start(){
		myText = GetComponent<Text>();
	}
	
	public void addPoints(int points){
		score += points;
		myText.text = score.ToString();
	}
	
	public static void Reset(){
		score = 0;
		beatBoss = false;
	}
}
