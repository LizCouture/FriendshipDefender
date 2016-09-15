using UnityEngine;
using System.Collections;

public class WinText : MonoBehaviour {
	public static bool won;
	private float counter;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(won) counter -= Time.deltaTime;
		if (counter <= 0) gameObject.SetActive (false);
	
	}
}
