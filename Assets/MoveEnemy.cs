using UnityEngine;
using System.Collections;

public class MoveEnemy : MonoBehaviour {

	[HideInInspector]
	public GameObject[] waypoints;

	private int currentWaypoint = 0;

	private float lastWaypointSwitchTime;

	public float speed = 1.0f;

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
			}
		}
	
	}
}
