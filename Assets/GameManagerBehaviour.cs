using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManagerBehaviour : MonoBehaviour {

	public Text GoldLabel;
	public Text waveLabel;
	public GameObject[] nextWaveLabels;
	public bool gameOver = false;
	public Text healthLabel;
	public GameObject[] healthIndicator;

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

	private int wave;
	public int Wave {
		get {
			return wave;
		}
		set {
			wave = value;
			if (!gameOver) {
                if (nextWaveLabels != null)
                {
                    for (int i = 0; i < nextWaveLabels.Length; i++)
                    {
                        nextWaveLabels[i].GetComponent<Animator>().SetTrigger("nextWave");
                    }
                }
			}
			waveLabel.text = "WAVE: " + (wave + 1);
		}
	}

	private int health;
	public int Health {
		get { 
			return health; 
		}
		set {
			if (value < health) {
				Camera.main.GetComponent<CameraShake>().Shake();
			}
			health = value;
			healthLabel.text = "HEALTH: " + health;

			if (health <= 0 && !gameOver) {
				gameOver = true;
				GameObject gameOverText = GameObject.FindGameObjectWithTag ("GameOver");
				gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
			}
			 
			for (int i = 0; i < healthIndicator.Length; i++) {
				if (i < Health) {
					healthIndicator[i].SetActive(true);
				} else {
					healthIndicator[i].SetActive(false);
				}
			}
		}
	}

	// Use this for initialization
	void Start () {
        Gold = GlobalControl.Instance.Gold;
		Wave = 0;
		Health = 5;
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void SavePlayer()
    {
        GlobalControl.Instance.Gold = Gold;
    }
}
