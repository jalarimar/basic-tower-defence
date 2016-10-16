using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManagerBehaviour : MonoBehaviour {

	public Text GoldLabel;

	private int gold;
	public int Gold {
		get {
			return gold;
		}
		set {
			gold = value;
			GoldLabel.GetComponent<Text> ().text = "GOLD: " + gold;
		}
	}

	// Use this for initialization
	void Start () {
		Gold = 1997;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
