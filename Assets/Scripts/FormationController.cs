using UnityEngine;
using System.Collections;

public class FormationController : MonoBehaviour {
	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 1;
	public float padding = 0;
	public float spawnDelaySeconds = 1;
	public bool isBoss;
	public AudioClip arrivalSound;
	
	private float xmin,xmax,ymin,ymax;
	private int direction = 1;
	private float boundaryRightEdge, boundaryLeftEdge;
	
	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		boundaryLeftEdge = Camera.main.ViewportToWorldPoint (new Vector3(0,0,distance)).x + padding;
		boundaryRightEdge = Camera.main.ViewportToWorldPoint (new Vector3(1,0,distance)).x - padding;
		
		if (arrivalSound != null){
			AudioSource.PlayClipAtPoint (arrivalSound, transform.position);
		}
		
		foreach(Transform child in transform){
		    if (child.tag == "spawnPosition"){
		    GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
		    enemy.transform.parent = child;
		    }
		}
	}
	
	void OnDrawGizmos(){
		xmin = transform.position.x - .5f * width;
		xmax = transform.position.x + .5f * width;
		ymin = transform.position.y - .5f * height;
		ymax = transform.position.y + .5f * height;
		Gizmos.DrawLine (new Vector3(xmin,ymin,0), new Vector3(xmin,ymax,0));
		Gizmos.DrawLine (new Vector3(xmin,ymax,0), new Vector3(xmax,ymax,0));
		Gizmos.DrawLine (new Vector3(xmax,ymax,0), new Vector3(xmax,ymin,0));
		Gizmos.DrawLine (new Vector3(xmax,ymin,0), new Vector3(xmin,ymin,0));
	}
	
	// Update is called once per frame
	void Update () {
		float formationRightEdge = transform.position.x + .5f * width;
		float formationLeftEdge = transform.position.x - .5f * width;
		if(formationRightEdge > boundaryRightEdge){
			direction = -1;
		}
		if (formationLeftEdge < boundaryLeftEdge){
			direction = 1;
		}
		transform.position += new Vector3(direction * speed * Time.deltaTime,0,0);
		if(AllMembersAreDead () && !isBoss){
			SpawnUntilFull();
		}
	}
	
	void SpawnEnemies(){
		foreach(Transform child in transform){
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}
	
	bool FreePositionExists(){
		foreach(Transform position in transform){
			if(position.childCount > 0){
				return true;
			}
		}
		return false;
	}
	
	void SpawnUntilFull(){
		Transform freePos = NextFreePosition();
		if(freePos != null){
		GameObject enemy = Instantiate (enemyPrefab, freePos.position, Quaternion.identity) as GameObject;
		enemy.transform.parent = freePos;
		}
		if(FreePositionExists ()){
			Invoke ("SpawnUntilFull",spawnDelaySeconds);
		}
	}
	
	Transform NextFreePosition(){
		foreach(Transform position in transform){
			if(position.childCount == 0){
				return position;
			}
		}
		return null;
	}
	
	bool AllMembersAreDead(){
		foreach(Transform position in transform){
			if(position.childCount > 0){
				return false;
			}
		}
		return true;
	}
}
