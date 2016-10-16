using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

	public GameObject TestEnemyPrefab;

	public GameObject[] waypoints;

	// Use this for initialization
	void Start () {
		Instantiate (TestEnemyPrefab).GetComponent<MoveEnemy> ().waypoints = waypoints;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
