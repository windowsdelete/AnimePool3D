using UnityEngine;
using System.Collections;

public class PocketsController : MonoBehaviour {
	public GameObject otherBalls;
	public GameObject mainBall;

	private Vector3 originalmainBallPosition;

	void Start() {
		originalmainBallPosition = mainBall.transform.position;
	}

	void OnCollisionEnter(Collision collision) {
		foreach (var transform in otherBalls.GetComponentsInChildren<Transform>()) {
			if (transform.name == collision.gameObject.name) {
				var objectName = collision.gameObject.name;
				GameObject.Destroy(collision.gameObject);

				var ballNumber = int.Parse(objectName.Replace("Ball", ""));
				PoolGameController.GameInstance.BallPocketed(ballNumber);
			}
		}

		if (mainBall.transform.name == collision.gameObject.name) {
			mainBall.transform.position = originalmainBallPosition;
		}
	}
}
