using UnityEngine;
using System.Collections;

public class PlaceMonster : MonoBehaviour {

	public GameObject monsterPrefab;
	private GameObject monster;

	private GameManagerBehaviour gameManager;

	private bool canAfford(int cost) {
		return gameManager.Gold >= cost;
	}

	private bool canPlaceMonster() {
		int initialCost = monsterPrefab.GetComponent<MonsterData> ().Levels[0].Cost;
		return monster == null && canAfford(initialCost);
	}

	private bool canUpgradeMonster() {
		if (monster != null) {
			MonsterData monsterData = monster.GetComponent<MonsterData> ();
			int upgradeCost = monsterPrefab.GetComponent<MonsterData> ().GetNextLevel().Cost;
			return monsterData.GetNextLevel() != null && canAfford(upgradeCost);
		}
		return false;
	}

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp() {

		if (canPlaceMonster ()) {
			monster = (GameObject)Instantiate (monsterPrefab, transform.position, Quaternion.identity);

			AudioSource audioSource = gameObject.GetComponent<AudioSource> ();
			audioSource.PlayOneShot (audioSource.clip);

			gameManager.Gold -= monster.GetComponent<MonsterData> ().CurrentLevel.Cost;

		} else if (canUpgradeMonster ()) {

			monster.GetComponent<MonsterData> ().IncreaseLevel ();

			AudioSource audioSource = gameObject.GetComponent<AudioSource> ();
			audioSource.PlayOneShot (audioSource.clip);

			gameManager.Gold -= monster.GetComponent<MonsterData> ().CurrentLevel.Cost;

		}

	}
}
