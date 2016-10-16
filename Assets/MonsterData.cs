using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class MonsterLevel {
	public int Cost;
	public GameObject visualization;
}


public class MonsterData : MonoBehaviour {

	private MonsterLevel currentLevel;
	public MonsterLevel CurrentLevel {
		get {
			return currentLevel;
		}
		set {
			currentLevel = value;

			int currentLevelIndex = Levels.IndexOf (currentLevel);
			GameObject levelVisualization = Levels [currentLevelIndex].visualization;
			for (int i = 0; i < Levels.Count; i++) {
				if (levelVisualization != null) {
					if (i == currentLevelIndex) {
						Levels [i].visualization.SetActive (true);
					} else {
						Levels [i].visualization.SetActive (false);
					}
				}
			}
		}
	}

	public List<MonsterLevel> Levels;

	public MonsterLevel GetNextLevel() {
		int currentLevelIndex = Levels.IndexOf (currentLevel);
		int maxLevelIndex = Levels.Count - 1;
		if (currentLevelIndex < maxLevelIndex) {
			return Levels [currentLevelIndex + 1];
		} else {
			return null;
		}
	}

	public void IncreaseLevel() {
		CurrentLevel = GetNextLevel () ?? CurrentLevel;
	}



	// Runs once, even in the editor
	void OnEnable() {
		CurrentLevel = Levels [0];
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
