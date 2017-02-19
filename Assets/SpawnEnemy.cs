using UnityEngine;
using System.Collections;

[System.Serializable]
public class Wave {
	public GameObject enemyPrefab;
	public float spawnInterval = 2;
	public int maxEnemies = 20;
}

public class SpawnEnemy : MonoBehaviour {

	public GameObject TestEnemyPrefab;

	public GameObject[] waypointsEven;
    public GameObject[] waypointsOdd;

	public Wave[] waves;
	public int timeBetweenWaves = 1;

	private GameManagerBehaviour gameManager;

	private float lastSpawnTime;
	private int enemiesSpawned = 0;

	// Use this for initialization
	void Start () {
		lastSpawnTime = Time.time;
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManagerBehaviour> ();
	}
	
	// Update is called once per frame
	void Update () {
		int currentWave = gameManager.Wave; 

        if (currentWave < waves.Length) {
            float timeInterval = Time.time - lastSpawnTime;
            float spawnInterval = waves [currentWave].spawnInterval;
			if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) || 
				timeInterval > spawnInterval) && enemiesSpawned < waves[currentWave].maxEnemies) {
				lastSpawnTime = Time.time;
				GameObject newEnemy = Instantiate(waves[currentWave].enemyPrefab);
                if (enemiesSpawned % 2 == 0) {
                    newEnemy.GetComponent<MoveEnemy>().waypoints = waypointsEven;
                } else {
                    newEnemy.GetComponent<MoveEnemy>().waypoints = waypointsOdd;
                }
				enemiesSpawned++;
			}
			if (enemiesSpawned == waves[currentWave].maxEnemies && GameObject.FindGameObjectWithTag("Enemy") == null) {
				gameManager.Wave++;
				gameManager.Gold = Mathf.RoundToInt(gameManager.Gold * 1.1f);
				enemiesSpawned = 0;
				lastSpawnTime = Time.time;
			} 
		} else if (gameManager.Health > 0) {
            gameManager.gameOver = true;
            GameObject gameOverText = GameObject.FindGameObjectWithTag("GameWon");
            gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
            gameManager.SavePlayer();
		}
	}
}
