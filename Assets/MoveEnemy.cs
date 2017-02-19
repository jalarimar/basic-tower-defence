using UnityEngine;

public class MoveEnemy : MonoBehaviour {

	[HideInInspector]
	public GameObject[] waypoints;

	private int currentWaypoint = 0;

	private float lastWaypointSwitchTime;

	public float speed = 10.0f;

	// Use this for initialization
	void Start () {
		lastWaypointSwitchTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 startPosition = waypoints [currentWaypoint].transform.position;
		Vector3 endPosition = waypoints [currentWaypoint + 1].transform.position;

		float pathLength = Vector3.Distance (startPosition, endPosition);
		float totalTimeForPath = pathLength / speed;

		float currentTimeOnPath = Time.time - lastWaypointSwitchTime;

		gameObject.transform.position = Vector3.Lerp (startPosition, endPosition, currentTimeOnPath / totalTimeForPath);

		if (gameObject.transform.position.Equals (endPosition)) {
			int lastWaypointIndex = waypoints.Length - 1;
			if (currentWaypoint < lastWaypointIndex - 1) {
				currentWaypoint++;
				lastWaypointSwitchTime = Time.time;

			} else {
				Destroy (gameObject);

				AudioSource audioSource = gameObject.GetComponent<AudioSource> ();
				AudioSource.PlayClipAtPoint (audioSource.clip, transform.position);

				GameManagerBehaviour gameManager = GameObject.Find ("GameManager").GetComponent<GameManagerBehaviour> ();
				gameManager.Health -= 1;
			}
		}
		RotateIntoMoveDirection();
	}

	private void RotateIntoMoveDirection() {
		Vector3 newStartPosition = waypoints [currentWaypoint].transform.position;
		Vector3 newEndPosition = waypoints [currentWaypoint + 1].transform.position;
		Vector3 newDirection = (newEndPosition - newStartPosition);
	
		float x = newDirection.x;
		float y = newDirection.y;
		float rotationAngle = Mathf.Atan2 (y, x) * 180 / Mathf.PI;

		GameObject sprite = (GameObject)gameObject.transform.FindChild ("Sprite").gameObject;
		sprite.transform.rotation = Quaternion.AngleAxis (rotationAngle, Vector3.forward);

	}

	public float distanceToGoal() {
		float distance = 0;
		distance += Vector3.Distance(
			gameObject.transform.position, 
			waypoints [currentWaypoint + 1].transform.position);
		for (int i = currentWaypoint + 1; i < waypoints.Length - 1; i++) {
			Vector3 startPosition = waypoints [i].transform.position;
			Vector3 endPosition = waypoints [i + 1].transform.position;
			distance += Vector3.Distance(startPosition, endPosition);
		}
		return distance;
	}
}
